using System;
using System.Collections.Generic;
using System.Linq;
using DiabloRL.Actions;
using DiabloRL.Behaviors;
using DiabloRL.Entities;
using DiabloRL.Processing;
using DiabloRL.Things;
using SadConsole;
using SadConsole.Entities;
using SadRogue.Integration;
using SadRogue.Primitives.GridViews;
using Action = DiabloRL.Actions.Action;
using Console = System.Console;

namespace DiabloRL
{
    public class MapScreen : ScreenObject
    {
        public readonly GameMap Map;
        public readonly Player Player;
        public readonly MessageLogConsole MessageLog;

        const int MessageLogHeight = 5;

        public MapScreen(GameMap map)
        {
            // Record the map we're rendering
            Map = map;

            // Create a renderer for the map, specifying viewport size.  The value in DefaultRenderer is automatically
            // managed by the map, and renders whenever the map is the active screen.
            //
            // CUSTOMIZATION: Pass in custom fonts/viewport sizes here.
            //
            // CUSTOMIZATION: If you want multiple renderers to render the same map, you can call CreateRenderer and
            // manage them yourself; but you must call the map's RemoveRenderer when you're done with these renderers,
            // and you must add any non-default renderers to the SadConsole screen object hierarchy, IN ADDITION
            // to the map itself.
            
            // load custom font/sprites for the dungeon
            var font = SadConsole.Game.Instance.LoadFont("Resources/Fonts/Buddy.font");
            // 16 & 12 are arbitrary based on trial and error. Should come up with a better system to calculate
            Map.DefaultRenderer = Map.CreateRenderer((Game.Width - 16, Game.Height + 12 - MessageLogHeight), font);

            // Make the Map (which is also a screen object) a child of this screen.  You MUST have the map as a child
            // of the active screen, even if you are using entirely custom renderers.
            Map.Parent = this;

            // Make sure the map is focused so that it and the entities can receive keyboard input
            Map.IsFocused = true;

            // Generate player, add to map at a random walkable position, and calculate initial FOV
            Player = MapObjectFactory.Player();
            Player.Position = Map.WalkabilityView.RandomPosition(true);
            Map.AddEntity(Player);
            Player.AllComponents.GetFirst<PlayerFOVController>().CalculateFOV();

            // Center view on player as they move
            Map.DefaultRenderer?.SadComponents.Add(new SadConsole.Components.SurfaceComponentFollowTarget
                {Target = Player});

            // Create message log
            MessageLog = new MessageLogConsole(Game.Width, MessageLogHeight);
            MessageLog.Parent = this;
            MessageLog.Position = new(0, Game.Height - MessageLogHeight);
        }

        public GameResult Process()
        {
            
            // ActionStack.Run(default);
            
            // create enumerator if not already done so
            if (_processEnumerator == null)
                _processEnumerator = CreateProcessEnumerable().GetEnumerator();
            
            if (!_processEnumerator.MoveNext())
                // if there's no more results then the game must be over
                return new GameResult(GameResultFlags.GameOver | GameResultFlags.NeedsPause);
            
            // otherwise send the GameResult of the current turn
            return _processEnumerator.Current;
        }

        private IEnumerable<GameResult> CreateProcessEnumerable()
        {
            // while the game is playing
            while (true)
            {
                foreach (GameEntity entity in Map.Entities.Items)
                // for (var entityIndex = 0; entityIndex < Map.Entities.Count; entityIndex++)
                {
                    var energy = entity.AllComponents.GetFirstOrDefault<Energy>();
                    while (energy.HasEnergy)
                    {
                        // Don't continue if the entity's (most likely the player) behavior requires an action
                        while (entity.Behavior.NeedsUserInput)
                            yield return new GameResult(GameResultFlags.NeedsUserInput, entity);
        
                        foreach (var action in entity.TakeTurn())
                        {
                            foreach (var result in ProcessAction(action))
                                yield return result;
                        }
                    }
                    
                    // round has finished so give everything some energy
                    energy.Gain();
                }
            }
        }

        private IEnumerable<GameResult> ProcessAction(Action actionToProcess)
        {
            var actions = new Queue<Action>();
            actions.Enqueue(actionToProcess);
        
            while (actions.Count > 0)
            {
                var action = actions.Peek();

                var result = action.Process(actions);
                
                // cascade through alternate actions if possible
                while (result.Alternate != null)
                    result = result.Alternate.Process(actions);
                
                if (result.IsDone)
                    actions.Dequeue();
                
                // Bandaid fix
                // monster should be deciding what to do if an action failed
                if (result.IsFlagSet(ActionResultFlags.Failed))
                    action.AfterSuccess();
                
                if (result.Success)
                    action.AfterSuccess();

                if (result.NeedsPause)
                    yield return new GameResult(GameResultFlags.NeedsPause);
                else if (result.NeedsCheckForCancel)
                    yield return new GameResult(GameResultFlags.CheckForCancel);
            }
        }

        private IEnumerator<GameResult> _processEnumerator;
        private GameEntity _actingGameEntity;

    }
}