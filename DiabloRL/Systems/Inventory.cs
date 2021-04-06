using System;
using System.Collections.Generic;
using DiabloRL.Actors;
using DiabloRL.Components;
using DiabloRL.Enums;
using DiabloRL.Models;
using DiabloRL.Models.Equipment;
using SadConsole.Components.GoRogue;

namespace DiabloRL.Systems
{
    public class Inventory : ComponentBase<Player>
    {
        public readonly List<Item> Items;
        public readonly Dictionary<EquipSlots, Equippable> EquippedItems;

        public static readonly string DidChangeNotification = "Inventory.EquippedItemsDidChange";

        public Inventory()
        {
            Items = new List<Item>();
            EquippedItems = new Dictionary<EquipSlots, Equippable>();
        }

        /// <summary>
        ///  Uses an item from the inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns>True if the item was consumed</returns>
        public bool UseItem(Item item)
        {
            if (!item.Use(Parent)) return false;
            
            // System.Console.WriteLine($"{Parent.Name} used {item.Name}");
            
            if (item.IsConsumedOnUse)
                Items.Remove(item);

            
            // notify that stats have been changed
            foreach (var (key,value) in item.AffectedStats)
                this.PostNotification(Stats.DidChangeNotification(key), Parent);


            return true;
        }

        public void AddItem(Item item)
        {
            if (Items.Contains(item)) return;
            
            System.Console.WriteLine($"{item.Name} was added to {Parent.Name}'s inventory.");
            item.Owner = Parent;
            Items.Add(item);
        }
    }
}