using System.Collections.Generic;
using DiabloRL.Actors;
using DiabloRL.Components;
using DiabloRL.Enums;
using SadConsole;

namespace DiabloRL.Models
{
    public abstract class Item
    {
        public Dictionary<StatTypes, int> AffectedStats;
        public static ColoredGlyph Glyph;
        public bool IsEquipped;
        public bool IsConsumedOnUse;
        public string Name;
        public int StackSize = 1;
        public int Cost;
        public Player Owner;

        public Item()
        {
            AffectedStats = new Dictionary<StatTypes, int>();
        }

        /// <summary>
        /// Uses the item such as quaff a potion or equip a piece of gear
        /// </summary>
        /// <returns>True if item was successfully used</returns>
        public abstract bool Use(Player actor);
    }
}