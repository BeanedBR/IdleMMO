using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;

namespace Assets.Data.Items
{
    public sealed class Bank
    {
        private static Bank instance;
        
        public static Bank Instance => instance ??= new Bank();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatistics()
        {
            instance = null;
        }

        // How many items the player owns. If they own 0 of the item, that item is removed from the list entirely.
        // Only shows the items that the player actually owns:
        private readonly Dictionary<ItemData, int> items = new Dictionary<ItemData, int>();
        public IReadOnlyDictionary<ItemData, int> Items => items;

        private Bank() { }

        // How many of a specific item does this player have? (0 if none)
        public int GetQuantity(ItemData item)
        {
            if (item == null) return 0;
            return items.TryGetValue(item, out int quantity) ? quantity : 0;
        }

        // Checks if the player has atleast this many items in their bank:
        public bool Has(ItemData item, int quantity = 1)
        {
            return GetQuantity(item) >= quantity;
        }

        // Adds items to the player's bank:
        public void Add(ItemData item, int quantity = 1)
        {
            if (item == null)
            {
                Debug.LogWarning("Bank.Add was given no item. ItemData may be missing in the Inspector.");
                return;
            }
            if (quantity <= 0) return;

            items[item] = GetQuantity(item) + quantity;
        }

        // Removes items if the player has enough items to remove. Returns true if they were removed, removes nothing if the player didn't have enough:
        public bool TryRemove(ItemData item, int quantity = 1)
        {
            if (item == null || quantity <= 0) return false;

            int current = GetQuantity(item);
            if (current < quantity) return false;

            int remaining = current - quantity;
            if (remaining == 0)
            {
                items.Remove(item);
            }
            else
            {
                items[item] = remaining;
            }
            return true;
        }
    }
}