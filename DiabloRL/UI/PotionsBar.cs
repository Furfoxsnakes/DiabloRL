using Microsoft.Xna.Framework;
using SadConsole;

namespace DiabloRL.UI
{
    public class PotionsBar : Console
    {
        public PotionsBar(Point pos) : base(15, 2)
        {
            Position = pos;
            
            // print the headers of the bar
            Print(0,0,"1 2 3 4 5 6 7 8");

            for (var i = 0; i < 8; i++)
            {
                Print(i * 2, 1, new ColoredGlyph('p', Color.Crimson, Color.Black));
            }
        }
    }
}