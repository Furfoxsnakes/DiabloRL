using System;
using DiabloRL.Components;
using GoRogue.Components;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace DiabloRL.Entities
{
    public class Goblin : Enemy
    {
        public Goblin() : base(Color.White, Color.Black, 'g')
        {
            Name = "Goblin";
        }
    }
}