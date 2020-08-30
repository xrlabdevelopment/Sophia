using System;
using NUnit.Framework;
using Sophia.Platform.Gameplay;

namespace Sophia.Tests.Platform
{
    [TestFixture]
    public class testsuit_CappedInventory
    {
        internal class MyInventoryItem : IInventoryItem
        {
            //--------------------------------------------------------------------------------------
            // Properties
            public Guid ID
            {
                get;
                private set;
            } = Guid.NewGuid();

            public IInventoryItemView View => throw new NotImplementedException();  // In tests there is not view required
        }

        internal class MyOtherInventoryItem : IInventoryItem
        {
            //--------------------------------------------------------------------------------------
            // Properties
            public Guid ID
            {
                get;
                private set;
            } = Guid.NewGuid();

            public IInventoryItemView View => throw new NotImplementedException();  // In tests there is not view required
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_inventory_add()
        {
            CappedInventoryController inventory = new CappedInventoryController(2);

            inventory.add(new MyInventoryItem());
            inventory.add(new MyInventoryItem());

            Assert.AreEqual(inventory.Count, 2);

            InventoryResult result = inventory.add(new MyInventoryItem());

            Assert.That(result, Is.EqualTo(InventoryResult.MAX_ITEMS));
        }
    }
}
