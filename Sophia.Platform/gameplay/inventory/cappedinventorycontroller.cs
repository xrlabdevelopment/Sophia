namespace Sophia.Platform.Gameplay
{
    public class CappedInventoryController : InventoryController
    {
        //--------------------------------------------------------------------------------------
        // Properties
        /// <summary>
        /// Max amount of items in this inventory
        /// </summary>
        public int MaxItems
        {
            get;
            private set;
        } = 1;

        //--------------------------------------------------------------------------------------
        public CappedInventoryController(int maxItems)
        {
            MaxItems = maxItems;
        }

        //--------------------------------------------------------------------------------------
        public override InventoryResult add(IInventoryItem item)
        {
            if (Count >= MaxItems)
                return InventoryResult.MAX_ITEMS;

            return base.add(item);
        }
    }
}
