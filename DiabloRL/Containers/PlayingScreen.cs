using DiabloRL.UI;
using GoRogue;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Actions;

namespace DiabloRL.Containers
{
    public class PlayingScreen : ContainerConsole
    {
        public MapConsole MapConsole;
        public GameUI GameUI;
        public MenuWindow MenuWindow;
        public InventoryWindow InventoryWindow;

        public PlayingScreen()
        {
            Game.Player = new Player(Coord.NONE);
            
            MapConsole = new MapConsole(100, 100, Game.GameWidth, Game.GameplayAreaHeight);
            Children.Add(MapConsole);
            
            GameUI = new GameUI(Game.GameWidth, Game.GameUIHeight);
            Children.Add(GameUI);

            MenuWindow = new MenuWindow();
            Children.Add(MenuWindow);

            InventoryWindow =
                new InventoryWindow(Game.GameWidth / 2, Game.GameHeight - Game.GameUIHeight, new Point(Game.GameWidth / 2, 0));
            Children.Add(InventoryWindow);
        }
    }
}