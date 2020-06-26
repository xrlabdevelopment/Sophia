using System.Collections.Generic;
using UnityEngine;

namespace Sophia.Deprecated
{
    public class Inventory : BaseMonoBehaviour
    {
        //--------------------------------------------------------------------------------------
        // Delegate
        public delegate void InventoryItemAdded(IInventoryItem item);
        public delegate void InventoryItemRemoved(IInventoryItem item);

        public InventoryItemAdded OnInventoryItemAdded;
        public InventoryItemRemoved OnInventoyItemRemoved;

        //--------------------------------------------------------------------------------------
        // Inspector
        [SerializeField]
        private readonly IInventoryItem[] InitialItems;

        //--------------------------------------------------------------------------------------
        // Fields
        private InventoryController inventory_controller = null;

        #region Unity Messages

        //--------------------------------------------------------------------------------------
        private void Awake()
        {
            inventory_controller = new InventoryController(InitialItems);

            inventory_controller.OnItemAdded += onItemAdded;
            inventory_controller.OnItemRemoved += onItemRemoved;
        }

        #endregion

        //--------------------------------------------------------------------------------------
        public void add(IInventoryItem item)
        {
            inventory_controller.add(item);
        }
        //--------------------------------------------------------------------------------------
        public void remove(IInventoryItem item)
        {
            inventory_controller.remove(item);
        }

        //--------------------------------------------------------------------------------------
        private void onItemAdded(IInventoryItem item)
        {
            if (OnInventoryItemAdded != null)
                OnInventoryItemAdded(item);
        }
        //--------------------------------------------------------------------------------------
        private void onItemRemoved(IInventoryItem item)
        {
            if (OnInventoyItemRemoved != null)
                OnInventoyItemRemoved(item);
        }
    }
}
