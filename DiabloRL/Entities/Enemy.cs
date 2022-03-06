using System;
using DiabloRL.AI;
using DiabloRL.Behaviors;
using GoRogue.Components;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace DiabloRL.Entities
{
    public class Enemy : GameEntity
    {
        public Enemy(Color foreground, Color background, int glyph) : base(foreground, background, glyph, walkable:false, transparent: false, (int)GameMap.Layer.Monsters)
        {
            
        }
    }
}