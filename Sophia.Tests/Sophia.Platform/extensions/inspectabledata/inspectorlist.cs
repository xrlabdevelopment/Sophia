
using NUnit.Framework;
using System.Linq;
using Sophia.Platform.Extension;

namespace Sophia.Tests.Platform
{
    [TestFixture]
    public class testsuit_InspectorList
    {
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_InspectorList_Add()
        {
            IntList int_list = new IntList();

            int_list.Add(0);

            Assert.That(int_list.Count, Is.EqualTo(1));
            Assert.That(int_list.First, Is.EqualTo(0));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_InspectorList_Clear()
        {
            IntList int_list = new IntList();

            int_list.Add(0);

            Assert.That(int_list.Count, Is.EqualTo(1));

            int_list.Clear();

            Assert.That(int_list.Count, Is.EqualTo(0));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_InspectorList_Contains()
        {
            IntList int_list = new IntList();

            int_list.Add(0);

            Assert.IsTrue(int_list.Contains(0));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_InspectorList_CopyTo()
        {
            IntList int_list = new IntList();
            int[] int_array = new int[1];

            int_list.CopyTo(int_array, 1);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_InspectorList_Remove()
        {
            IntList int_list = new IntList();

            int_list.Add(0);

            Assert.IsTrue(int_list.Contains(0));

            int_list.Remove(0);

            Assert.IsFalse(int_list.Contains(0));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_InspectorList_IndexOf()
        {
            IntList int_list = new IntList();

            int_list.Add(0);
            int_list.Add(1);
            int_list.Add(2);

            Assert.AreEqual(int_list.IndexOf(1), 1);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_InspectorList_Insert()
        {
            IntList int_list = new IntList();

            int_list.Add(0);
            int_list.Add(1);
            int_list.Add(2);

            Assert.AreEqual(int_list.IndexOf(1), 1);

            int_list.Insert(1, 5);

            Assert.AreEqual(int_list.IndexOf(5), 1);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_InspectorList_RemoveAt()
        {
            IntList int_list = new IntList();

            int_list.Add(0);
            int_list.Add(1);
            int_list.Add(2);

            Assert.AreEqual(int_list.Count, 3);

            int_list.RemoveAt(1);

            Assert.AreEqual(int_list.Count, 2);
        }
    }
}
