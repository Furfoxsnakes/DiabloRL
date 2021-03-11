using DiabloRL.UI;
using SadConsole;
using SadConsole.Actions;

namespace DiabloRL.Containers
{
    public class PlayingScreen : ContainerConsole
    {
        public MapConsole MapConsole;
        public GameUI GameUI;

        public PlayingScreen()
        {
            MapConsole = new MapConsole(100, 100, Game.GameWidth, Game.GameplayAreaHeight);
            Children.Add(MapConsole);

            GameUI = new GameUI(Game.GameWidth, Game.GameUIHeight);
            Children.Add(GameUI);
        }
    }
}