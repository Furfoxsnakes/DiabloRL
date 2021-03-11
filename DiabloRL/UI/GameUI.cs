using Microsoft.Xna.Framework;
using SadConsole;

namespace DiabloRL.UI
{
    public class GameUI : ControlsConsole
    {
        public GameUI(int width, int height) : base(width, height)
        {
            Position = new Point(0, Game.GameHeight - height);
        }
    }
}