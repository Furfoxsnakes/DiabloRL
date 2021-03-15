using System;
using DiabloRL.Containers;
using DiabloRL.Systems;
using SadConsole;

namespace DiabloRL
{
    internal class Game
    {
        private const int StartingWidth = 80;
        private const int StartingHeight = 60;

        public static readonly int GameWidth = 80;
        public static readonly int GameHeight = 60;

        public static readonly int GameplayAreaHeight = 40;
        public static readonly int GameUIHeight = StartingHeight - GameplayAreaHeight;
        public static InputManager InputManager { get; private set; }
        public static Random Random { get; private set; }

        public static PlayingScreen PlayingScreen => Global.CurrentScreen as PlayingScreen;

        private static void Main()
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create("Fonts/Buddy.font", StartingWidth, StartingHeight);
            SadConsole.Game.Instance.Window.AllowUserResizing = true;

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Init()
        {
            Random = new Random((int)DateTime.UtcNow.Ticks);
            InputManager = new InputManager();
            
            Global.CurrentScreen = new PlayingScreen();
        }
    }
}