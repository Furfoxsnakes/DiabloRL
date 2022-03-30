using DiabloRL.Resources;
using SadConsole;

namespace DiabloRL
{
    public enum GameState
    {
        Playing,
        Over
    }
    
    internal static class Game
    {
        // Window width/height
        public const int Width = 80;
        public const int Height = 25;

        // Map width/height
        private const int MapWidth = 100;
        private const int MapHeight = 100;

        public static MapScreen GameScreen;
        public static GameState GameState;
        public static Content Content;

        private static void Main()
        {
            SadConsole.Game.Create(Width, Height);
            SadConsole.Game.Instance.OnStart = Init;
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Init()
        {
            Content = new Content();
            ExperienceData.Load(Content);
            
            // Generate a dungeon map
            var map = MapFactory.GenerateDungeonMap(MapWidth, MapHeight);

            // Create a MapScreen and set it as the active screen so that it processes input and renders itself.
            GameScreen = new MapScreen(map);
            GameHost.Instance.Screen = GameScreen;

            // Destroy the default starting console that SadConsole created automatically because we're not using it.
            GameHost.Instance.DestroyDefaultStartingConsole();
        }
    }
}