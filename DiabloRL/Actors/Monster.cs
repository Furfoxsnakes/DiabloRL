using DiabloRL.Components;
using GoRogue;
using Microsoft.Xna.Framework;

namespace DiabloRL.Actors
{
    public class Monster : Actor
    {
        public Monster(Color foreground, Color background, int glyph, string name, Coord position) : base(foreground, background, glyph, name, position, (int)MapLayer.MONSTERS, false, true)
        {
            
        }
    }
}