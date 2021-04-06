using System;
using System.Collections.Generic;
using DiabloRL.Enums;

namespace DiabloRL.Models.Equipment
{
    public class ArmourItem : Equippable
    {
        public ArmourItem(int minAC, int maxAC, int maxDurability) : base(EquipSlots.HEAD, maxDurability)
        {
            var randomValue = Game.Random.Next(minAC, maxAC + 1);
            AffectedStats.Add(StatTypes.ARMOUR, randomValue);
        }
    }
}