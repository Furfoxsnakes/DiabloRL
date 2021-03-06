using DiabloRL.Actions;
using DiabloRL.Components;
using GoRogue;
using Microsoft.Xna.Framework;
using SadConsole.Actions;

namespace DiabloRL.Actors
{
    public class Monster : Actor
    {
        public ActionBase PlayerBumpAction;
        
        public Monster(Color foreground, Color background, int glyph, string name, Coord position) : base(foreground, background, glyph, name, position, (int)MapLayer.MONSTERS, false, true)
        {
            // PlayerBumpAction = new BumpActor(this, Game.MapScreen.Map.ControlledGameObject);
            AddGoRogueComponent(new MoveToPlayer());
        }
    }
}