using System;
using DiabloRL.Actors;
using DiabloRL.Enums;
using DiabloRL.Models;
using Microsoft.Xna.Framework;
using SadConsole;

namespace DiabloRL.Components
{
    public class HealthPotion : Potion
    {
        public HealthPotion(int amount) : base(StatTypes.LIFE, amount)
        {
            Glyph = new ColoredGlyph('p', Color.Crimson, Color.Black);
            Name = "Health Potion";
            IsConsumedOnUse = true;
        }

        public override bool Use(Player actor)
        {
            var stats = actor.GetGoRogueComponent<Stats>();

            if (stats == null) return false;
            
            if (stats[StatTypes.LIFE] == stats[StatTypes.MAX_LIFE])
            {
                System.Console.WriteLine("You are already at max life.");
                return false;
            }

            stats[StatTypes.LIFE] = Math.Min(stats[StatTypes.LIFE] + AffectedStats[StatTypes.LIFE],
                stats[StatTypes.MAX_LIFE]);

            return true;
        }
    }
}