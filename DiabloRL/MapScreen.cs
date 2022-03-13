using System;
using System.Collections.Generic;
using System.Linq;
using DiabloRL.Behaviors;
using DiabloRL.Entities;
using DiabloRL.Processing;
using SadConsole;
using SadConsole.Actions;
using SadRogue.Integration;
using SadRogue.Primitives.GridViews;
using Action = DiabloRL.Actions.Action;
using Console = System.Console;

namespace DiabloRL
{
    public class MapScreen : ScreenObject
    {
        public readonly GameMap Map;
        public readonly GameEntity Player;
        public readonly MessageLogConsole MessageLog;
        public readonly DiabloRL.Actions.ActionStack ActionStack;
        public readonly DiabloRL.Processing.GameFrameManager GameFrameManager;

        const int MessageLogHeight = 5;

        public MapScreen(GameMap map)
        {
            // Record the map we're rendering
            Map = map;

            // spin up the action stack
            ActionStack = new DiabloRL.Actions.ActionStack();
            GameFrameManager = new Processing.GameFrameManager(Map, this);

            // Create a renderer for the map, specifying viewport size.  The value in DefaultRenderer is automatically
            // managed by the map, and renders whenever the map is the active screen.
            //
            // CUSTOMIZATION: Pass in custom fonts/viewport sizes here.
            //
            // CUSTOMIZATION: If you want multiple renderers to render the same map, you can call CreateRenderer and
            // manage them yourself; but you must call the map's RemoveRenderer when you're done with these renderers,
            // and you must add any non-default renderers to the SadConsole screen object hierarchy, IN ADDITION
            // to the map itself.
            Map.DefaultRenderer = Map.CreateRenderer((Game.Width, Game.Height - MessageLogHeight));

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
            while (true)
            {
                foreach (GameEntity entity in Map.Entities.Items)
                {
                    // Don't continue if the entity's (most likely the player) behavior requires an action
                    while (entity.Behavior.NeedsUserInput)
                        yield return new GameResult(GameResultFlags.NeedsUserInput, entity);
        
                    foreach (var action in entity.TakeTurn())
                    {
                        foreach (var result in ProcessAction(action))
                        {
                            Game.GameScreen.MessageLog.AddMessage(result.Entity.Name);
                            yield return result;
                        }
                    }
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
        
                if (result.NeedsPause)
                    yield return new GameResult(GameResultFlags.NeedsPause);
                else if (result.NeedsCheckForCancel)
                    yield return new GameResult(GameResultFlags.CheckForCancel);
            }
        }

        public override void Update(TimeSpan delta)
        {
            base.Update(delta);
            GameFrameManager.Update(this, delta);
            ActionStack.Run(delta);
        }

        private IEnumerator<GameResult> _processEnumerator;
        private GameEntity _actingGameEntity;

    }
}