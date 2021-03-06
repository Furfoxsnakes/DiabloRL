using DiabloRL.Components;
using GoRogue;
using Microsoft.Xna.Framework;

namespace DiabloRL.Actors
{
    public class Zombie : Monster
    {
        public Zombie(Coord position) : base(Color.DarkKhaki, Color.Black, 'Z', name: "Zombie", position)
        {
            
        }

        public static Zombie Create(int level, Coord position)
        {
            var stats = new Stats(0, 0, 0, 0, 2, 0);
            var zombie = new Zombie(position);
            zombie.AddGoRogueComponent(stats);
            return zombie;
        }
    }
}