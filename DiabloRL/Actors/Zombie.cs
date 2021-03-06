using System.Collections.Generic;
using DiabloRL.Components;
using DiabloRL.Enums;
using GoRogue;
using Microsoft.Xna.Framework;

namespace DiabloRL.Actors
{
    public class Zombie : Monster
    {
        public MonsterTypes Type;
        

        public Zombie(Coord position) : base(Color.DarkKhaki, Color.Black, 'Z', name: "Zombie", position)
        {
            Type = MonsterTypes.UNDEAD;
            DungeonLevels = new Dictionary<Difficulties, int[]>()
            {
                {Difficulties.NORMAL, new int[] {1, 2}}
            };
            MonsterLevel = new Dictionary<Difficulties, int>()
            {
                {Difficulties.NORMAL, 1}
            };
        }

        public static Zombie Create(Difficulties difficulty, Coord position)
        {
            var stats = new Stats(0, 0, 0, 0, 2, 0);
            var zombie = new Zombie(position);
            zombie.AddGoRogueComponent(stats);
            return zombie;
        }
    }
}