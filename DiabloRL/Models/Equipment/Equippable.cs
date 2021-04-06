using System.Collections.Generic;
using DiabloRL.Actors;
using DiabloRL.Components;
using DiabloRL.Enums;
using DiabloRL.Systems;

namespace DiabloRL.Models.Equipment
{
    public class Equippable : Item
    {
        public EquipSlots EquipSlot;
        public int Durability;
        public int MaxDurability;
        public Dictionary<StatTypes, int> Requirements;
        public int QualityLevel;
        public static readonly string EquipNotification = "Item.Equipped";
        public static readonly string UnequipNotfication = "Item.Unequipped";

        public Equippable(EquipSlots slot, int maxDurability, Dictionary<StatTypes, int>? requirements = null)
        {
            EquipSlot = slot;
            IsEquipped = false;
            MaxDurability = maxDurability;
            Durability = maxDurability;
            Requirements = requirements;
        }
        
        public override bool Use(Player player)
        {
            return IsEquipped ? Unequip() : Equip();
        }

        public bool Equip()
        {
            var inventory = Owner.GetGoRogueComponent<Inventory>();
            if (inventory == null) return false;
            
            // unequip the previous item using this equip slot
            if (inventory.EquippedItems.ContainsKey(EquipSlot))
                inventory.EquippedItems[EquipSlot]?.Unequip();

            inventory.EquippedItems[EquipSlot] = this;
            IsEquipped = true;
            this.PostNotification(EquipNotification);
            return true;

        }

        public bool Unequip()
        {
            var inventory = Owner.GetGoRogueComponent<Inventory>();
            if (inventory == null) return false;

            if (!inventory.EquippedItems.ContainsKey(EquipSlot)) return false;

            inventory.EquippedItems[EquipSlot] = null;
            IsEquipped = false;
            this.PostNotification(EquipNotification);
            return true;
        }
    }
}