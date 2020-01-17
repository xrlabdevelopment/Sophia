using System;
using System.Collections.Generic;
using System.Linq;

namespace Sophia.Core
{
    public class InventoryController
    {
        //--------------------------------------------------------------------------------------
        // Delegate
        public delegate void Added(IInventoryItem item);
        public delegate void Removed(IInventoryItem item);

        public Added OnItemAdded;
        public Removed OnItemRemoved;

        //--------------------------------------------------------------------------------------
        // Fields
        private readonly List<IInventoryItem> inventory_items = null;

        //--------------------------------------------------------------------------------------
        public InventoryController(IInventoryItem[] items)
        {
            inventory_items = items != null
                ? new List<IInventoryItem>(items)
                : new List<IInventoryItem>();
        }

        //--------------------------------------------------------------------------------------
        public bool add(IInventoryItem item)
        {
            if (inventory_items.Find(i => i.ID == item.ID) == null)
                return false;

            inventory_items.Add(item);
            if (OnItemAdded != null)
                OnItemAdded(item);
            return true;
        }
        //--------------------------------------------------------------------------------------
        public bool remove(IInventoryItem item)
        {
            return inventory_items.Remove(item);
        }
        //--------------------------------------------------------------------------------------
        public bool remove(Guid ID)
        {
            int index = inventory_items.FindIndex(i => i.ID == ID);
            if (index == -1)
                return false;

            IInventoryItem item = inventory_items[index];
            if (OnItemRemoved != null)
                OnItemRemoved(item);

            return inventory_items.Remove(item);
        }

        //--------------------------------------------------------------------------------------
        public IInventoryItem get(Guid ID)
        {
            return inventory_items.Find(i => i.ID == ID);
        }
        //--------------------------------------------------------------------------------------
        public T get<T>(Guid ID)
            where T : class, IInventoryItem
        {
            return inventory_items.Find(i => i.ID == ID) as T;
        }

        //--------------------------------------------------------------------------------------
        public List<IInventoryItem> getItemsOfType<T>()
            where T : class, IInventoryItem
        {
            return inventory_items.FindAll(i => (i as T) != null).ToList();
        }
    }
}
