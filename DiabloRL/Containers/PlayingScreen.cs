using DiabloRL.UI;
using SadConsole;
using SadConsole.Actions;

namespace DiabloRL.Containers
{
    public class PlayingScreen : ContainerConsole
    {
        public MapConsole MapConsole;
        public GameUI GameUI;
        public MenuWindow MenuWindow;

        public PlayingScreen()
        {
            GameUI = new GameUI(Game.GameWidth, Game.GameUIHeight);
            Children.Add(GameUI);
            
            MapConsole = new MapConsole(100, 100, Game.GameWidth, Game.GameplayAreaHeight);
            Children.Add(MapConsole);
            
            MenuWindow = new MenuWindow();
            Children.Add(MenuWindow);
            MenuWindow.Show();
        }
    }
}