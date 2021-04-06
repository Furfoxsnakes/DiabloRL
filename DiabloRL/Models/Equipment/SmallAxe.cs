using System.Collections.Generic;
using DiabloRL.Enums;

namespace DiabloRL.Models.Equipment
{
    public class SmallAxe : WeaponItem
    {
        public SmallAxe() : base(EquipSlots.MAIN_HAND, 24, 2, 10, false, null)
        {
            Name = "Small Axe";
        }
    }
}