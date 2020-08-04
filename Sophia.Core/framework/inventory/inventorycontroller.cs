using System;
using System.Collections.Generic;
using System.Linq;

namespace Sophia
{
    /// <summary>
    /// An inventory system
    /// </summary>
    public class InventoryController
    {
        //--------------------------------------------------------------------------------------
        // Delegate
        /// <summary>
        /// A delegate created to fire when an item was added
        /// </summary>
        /// <param name="item">The added item</param>
        public delegate void Added(IInventoryItem item);
        /// <summary>
        /// A delegate created to fire when an item was removed
        /// </summary>
        /// <param name="item">The removed item</param>
        public delegate void Removed(IInventoryItem item);

        public Added OnItemAdded;
        public Removed OnItemRemoved;

        //--------------------------------------------------------------------------------------
        // Properties
        public int Count
        {
            get { return inventory_items.Count; }
        }

        //--------------------------------------------------------------------------------------
        // Fields
        private readonly List<IInventoryItem> inventory_items = null;

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor for the inventory controller
        /// </summary>
        /// <param name="items">Initial items present in the inventory</param>
        public InventoryController(IInventoryItem[] initialItems = null)
        {
            inventory_items = initialItems != null
                ? new List<IInventoryItem>(initialItems)
                : new List<IInventoryItem>();
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Add a new item to the inventory
        /// </summary>
        /// <param name="item">The item to be added</param>
        /// <returns>Returns true when it was added, false if not</returns>
        public bool add(IInventoryItem item)
        {
            if (inventory_items.Find(i => i.ID == item.ID) != null)
                return false;

            inventory_items.Add(item);
            if (OnItemAdded != null)
                OnItemAdded(item);
            return true;
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Remove an item from the inventory
        /// </summary>
        /// <param name="item">Item to be removed</param>
        /// <returns>Returns true when removed, false when not removed</returns>
        public bool remove(IInventoryItem item)
        {
            return inventory_items.Remove(item);
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Remove an item from the inventory
        /// </summary>
        /// <param name="identifier">Identifier of the item to be removed</param>
        /// <returns>Return true when removed, false when not removed</returns>
        public bool remove(Guid identifier)
        {
            int index = inventory_items.FindIndex(i => i.ID == identifier);
            if (index == -1)
                return false;

            IInventoryItem item = inventory_items[index];
            if (OnItemRemoved != null)
                OnItemRemoved(item);

            return inventory_items.Remove(item);
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve an item from the inventory
        /// </summary>
        /// <param name="identifier">Identifier of the item to be retrieved</param>
        /// <returns>The requested item when found, null otherwise</returns>
        public IInventoryItem get(Guid identifier)
        {
            return inventory_items.Find(i => i.ID == identifier);
        }
        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve an item from the inventory
        /// </summary>
        /// <typeparam name="T">Type of the item to be retrieved</typeparam>
        /// <param name="identifier">Identifier of the item to be retrieved</param>
        /// <returns>The requested item, null otherwise</returns>
        public T getAs<T>(Guid identifier)
            where T : class, IInventoryItem
        {
            return inventory_items.Find(i => i.ID == identifier) as T;
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Retrieve items of a certain type
        /// </summary>
        /// <typeparam name="T">The type of the items to be retrieved</typeparam>
        /// <returns>A list of all items of the specific type</returns>
        public List<IInventoryItem> getItemsOfType<T>()
            where T : class, IInventoryItem
        {
            return inventory_items.FindAll(i => (i as T) != null).ToList();
        }

        //--------------------------------------------------------------------------------------
        /// <summary>
        /// Remove all items from the inventory
        /// </summary>
        public void clear()
        {
            foreach(IInventoryItem item in inventory_items)
            {
                if (OnItemRemoved != null)
                    OnItemRemoved(item);
            }

            inventory_items.Clear();
        }
    }
}
