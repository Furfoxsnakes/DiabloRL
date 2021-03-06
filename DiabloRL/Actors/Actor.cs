using GoRogue;
using Microsoft.Xna.Framework;
using SadConsole;

namespace DiabloRL.Actors
{
    public class Actor : BasicEntity
    {
        public Actor(Color foreground, Color background, int glyph, Coord position, int layer, bool isWalkable, bool isTransparent) : base(foreground, background, glyph, position, layer, isWalkable, isTransparent)
        {
            
        }
    }
}