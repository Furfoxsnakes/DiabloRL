using System;
using DiabloRL.Components;
using DiabloRL.Things;
using GoRogue.Components;
using GoRogue.DiceNotation;
using SadRogue.Integration;
using SadRogue.Primitives;

namespace DiabloRL.Entities
{
    public class Goblin : Enemy
    {
        public Goblin() : base(Color.Red, Color.Black, 'g')
        {
            Name = "Goblin";
        }

        public override Attack GetAttack(GameEntity defender)
        {
            var damage = Dice.Parse("1d3");
            var element = Element.None;
            var effectType = EffectType.Melee;
            
            // get modifiers based on equipped weapon(s)
            
            // build the attack
            var attack = new Attack(damage, 1, element, effectType);
            
            // allow the defender to modify the attack
            
            // send it back
            return attack;
        }
    }
}