using System.Collections.Generic;
using DiabloRL.Enums;

namespace DiabloRL.Models.Equipment
{
    public class WeaponItem : Equippable
    {
        public bool IsRanged;
        
        public WeaponItem(EquipSlots slot, int maxDurability, int minDamage, int maxDamage, bool isRanged = false, Dictionary<StatTypes, int>? requirements = null) : base(slot, maxDurability, requirements)
        {
            AffectedStats.Add(StatTypes.MINDMG, minDamage);
            AffectedStats.Add(StatTypes.MAXDMG, maxDamage);
            IsRanged = isRanged;
        }
    }
}