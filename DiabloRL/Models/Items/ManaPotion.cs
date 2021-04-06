using System;
using DiabloRL.Actors;
using DiabloRL.Components;
using DiabloRL.Enums;
using Microsoft.Xna.Framework;
using SadConsole;

namespace DiabloRL.Models.Items
{
    public class ManaPotion : Potion
    {
        public ManaPotion(int amount) : base(StatTypes.MANA, amount)
        {
            Glyph = new ColoredGlyph('p', Color.Crimson, Color.Black);
            Name = "Mana Potion";
        }
        
        public override bool Use(Player actor)
        {
            var stats = actor.GetGoRogueComponent<Stats>();

            if (stats == null) return false;
            
            if (stats[StatTypes.MANA] == stats[StatTypes.MAX_MANA])
            {
                System.Console.WriteLine("You are already at max mana.");
                return false;
            }

            stats[StatTypes.MANA] = Math.Min(stats[StatTypes.MANA] + AffectedStats[StatTypes.MANA],
                stats[StatTypes.MAX_MANA]);

            return true;
        }
    }
}