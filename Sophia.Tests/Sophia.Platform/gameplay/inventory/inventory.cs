using System;
using NUnit.Framework;
using Sophia.Platform.Gameplay;

namespace Sophia.Tests.Platform
{
    [TestFixture]
    public class testsuit_Inventory
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
            InventoryController inventory = new InventoryController();

            inventory.add(new MyInventoryItem());

            Assert.AreEqual(inventory.Count, 1);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_inventory_remove_item()
        {
            InventoryController inventory = new InventoryController();

            MyInventoryItem item = new MyInventoryItem();
            inventory.add(item);

            Assert.AreEqual(inventory.Count, 1);

            inventory.remove(item);

            Assert.AreEqual(inventory.Count, 0);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_inventory_remove_identifier()
        {
            InventoryController inventory = new InventoryController();

            MyInventoryItem item = new MyInventoryItem();
            inventory.add(item);

            Assert.AreEqual(inventory.Count, 1);

            inventory.remove(item.ID);

            Assert.AreEqual(inventory.Count, 0);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_inventory_get()
        {
            InventoryController inventory = new InventoryController();

            MyInventoryItem item = new MyInventoryItem();
            inventory.add(item);

            Assert.AreEqual(inventory.Count, 1);

            IInventoryItem result = inventory.get(item.ID);

            Assert.That(result, Is.Not.Null);

            result = inventory.get(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_inventory_getAs()
        {
            InventoryController inventory = new InventoryController();

            MyInventoryItem item = new MyInventoryItem();
            inventory.add(item);
            MyOtherInventoryItem other = new MyOtherInventoryItem();
            inventory.add(other);

            Assert.AreEqual(inventory.Count, 2);

            MyInventoryItem result = inventory.getAs<MyInventoryItem>(item.ID);

            Assert.That(result, Is.Not.Null);

            MyOtherInventoryItem other_result = inventory.getAs<MyOtherInventoryItem>(item.ID);

            Assert.That(other_result, Is.Null);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_inventory_getItemsOfType()
        {
            InventoryController inventory = new InventoryController();

            inventory.add(new MyInventoryItem());
            inventory.add(new MyInventoryItem());

            Assert.AreEqual(inventory.Count, 2);

            IInventoryItem[] elements = inventory.getItemsOfType<MyInventoryItem>();

            Assert.AreEqual(elements.Length, 2);

            elements = inventory.getItemsOfType<MyOtherInventoryItem>();

            Assert.AreEqual(elements.Length, 0);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_inventory_clear()
        {
            InventoryController inventory = new InventoryController();

            inventory.add(new MyInventoryItem());

            Assert.AreEqual(inventory.Count, 1);

            inventory.clear();

            Assert.AreEqual(inventory.Count, 0);
        }
    }
}
