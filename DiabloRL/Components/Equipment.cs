using System.Collections.Generic;
using DiabloRL.Enums;
using DiabloRL.Models.Equipment;
using SadConsole.Components.GoRogue;

namespace DiabloRL.Components
{
    public class Equipment : ComponentBase<Player>
    {

        public readonly Dictionary<EquipSlots, Equippable> Equipped;

        public void EquipItem(Equippable item)
        {
            
        }

        public void UnequipItem(Equippable item)
        {
            if (!Equipped.ContainsKey(item.EquipSlot))
                Equipped[item.EquipSlot] = null;
            
            
        }
    }
}