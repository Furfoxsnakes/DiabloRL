using System.Collections.Generic;
using DiabloRL.Actors;
using DiabloRL.Enums;
using DiabloRL.Models;

namespace DiabloRL.Components
{
    public class Potion : Item
    {
        public Potion(StatTypes statType, int amount) : base()
        {
            AffectedStats = new Dictionary<StatTypes, int>()
            {
                {statType, amount}
            };
            IsConsumedOnUse = true;
        }
        
        public override bool Use(Player actor)
        {
            return true;
        }
    }
}