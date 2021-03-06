using System;
using DiabloRL.Actors;
using DiabloRL.Enums;
using GoRogue;
using GoRogue.GameFramework;
using GoRogue.MapGeneration;
using GoRogue.MapViews;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Actions;
using XnaRect = Microsoft.Xna.Framework.Rectangle;

namespace DiabloRL
{
    internal class MapScreen : ContainerConsole
    {
        public DungeonMap Map { get; }
        public GameFrameManager GameFrameManager { get; private set; }
        public ActionStack Actions { get; private set; }

        public ScrollingConsole MapRenderer { get; }

        //Generate a map and display it.  Could just as easily pass it into
        public MapScreen(int mapWidth, int mapHeight, int viewportWidth, int viewportHeight)
        {
            Map = GenerateDungeon(mapWidth, mapHeight);

            GameFrameManager = new GameFrameManager(Map);
            GameFrameManager.LogicFrameCompleted += OnLogicCompleted;
            Actions = new ActionStack();

            // Get a console that's set up to render the map, and add it as a child of this container so it renders
            MapRenderer = Map.CreateRenderer(new XnaRect(0, 0, viewportWidth, viewportHeight),
                SadConsole.Global.FontDefault);
            Children.Add(MapRenderer);
            Map.ControlledGameObject.IsFocused =
                true; // Set player to receive input, since in this example the player handles movement

            // Set up to recalculate FOV and set camera position appropriately when the player moves.  Also make sure we hook the new
            // Player if that object is reassigned.
            Map.ControlledGameObject.Moved += Player_Moved;
            Map.ControlledGameObjectChanged += ControlledGameObjectChanged;

            // Calculate initial FOV and center camera
            Map.CalculateFOV(Map.ControlledGameObject.Position, Map.ControlledGameObject.FOVRadius, Radius.DIAMOND);
            MapRenderer.CenterViewPortOnPoint(Map.ControlledGameObject.Position);
        }

        private void OnLogicCompleted(object? sender, EventArgs e)
        {
            Game.InputManager.IsPlayerTurn = true;
        }

        private void ControlledGameObjectChanged(object s, ControlledGameObjectChangedArgs e)
        {
            if (e.OldObject != null)
                e.OldObject.Moved -= Player_Moved;

            ((BasicMap) s).ControlledGameObject.Moved += Player_Moved;
        }

        private static DungeonMap GenerateDungeon(int width, int height)
        {
            // Same size as screen, but we set up to center the camera on the player so expanding beyond this should work fine with no other changes.
            var map = new DungeonMap(width, height);

            // Generate map via GoRogue, and update the real map with appropriate terrain.
            var tempMap = new ArrayMap<bool>(map.Width, map.Height);
            // QuickGenerators.GenerateDungeonMazeMap(tempMap, minRooms: 10, maxRooms: 20, roomMinSize: 5,
            //     roomMaxSize: 11);
            QuickGenerators.GenerateRectangleMap(tempMap);
            map.ApplyTerrainOverlay(tempMap, SpawnTerrain);

            Coord posToSpawn;
            // Spawn a few mock enemies
            for (int i = 0; i < 1; i++)
            {
                posToSpawn = map.WalkabilityView.RandomPosition(true); // Get a location that is walkable
                var zombie = Zombie.Create(Difficulties.NORMAL, posToSpawn);
                map.AddMonster(zombie);
            }

            // Spawn player
            posToSpawn = map.WalkabilityView.RandomPosition(true);
            map.ControlledGameObject = new Player(posToSpawn);
            map.AddEntity(map.ControlledGameObject);

            return map;
        }

        private void Player_Moved(object sender, ItemMovedEventArgs<IGameObject> e)
        {
            Map.CalculateFOV(Map.ControlledGameObject.Position, Map.ControlledGameObject.FOVRadius, Radius.DIAMOND);
            MapRenderer.CenterViewPortOnPoint(Map.ControlledGameObject.Position);
        }

        private static IGameObject SpawnTerrain(Coord position, bool mapGenValue)
        {
            // Floor or wall.  This could use the Factory system, or instantiate Floor and Wall classes, or something else if you prefer;
            // this simplistic if-else is just used for example
            if (mapGenValue) // Floor
                return new BasicTerrain(Color.White, Color.Black, '.', position, isWalkable: true, isTransparent: true);
            else // Wall
                return new BasicTerrain(Color.White, Color.Black, '#', position, isWalkable: false,
                    isTransparent: false);
        }

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);

            GameFrameManager.Update(MapRenderer, timeElapsed);
            Actions.Run(timeElapsed);
            // Game.InputManager.Actions.Run(timeElapsed);
        }
    }
}