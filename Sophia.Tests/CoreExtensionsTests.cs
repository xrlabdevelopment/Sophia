using Sophia.Core.Extensions;
using Sophia.Core.Algorithms;

using NUnit.Framework;

using System.Linq;
using System.Collections.Generic;

namespace Sophia.Tests.Core
{
    //arrayextensions.cs
    [TestFixture]
    public class testsuit_ArrayExtensions
    {
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_arrayextensions_concat()
        {
            int[] first = new int[] { 0, 1, 2, 3, 4 };
            int[] second = new int[] { 5, 6, 7, 8, 9 };

            int[] result = first.concat(second);
            for(int i = 0; i < 10; ++i)
            {
                Assert.That(i, Is.EqualTo(result[i]));
            }
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_arrayextensions_fill()
        {
            const int array_size = 10;

            int[] array = new int[array_size];
            array.fill(1);

            for(int i = 0; i < array_size; ++i)
            {
                Assert.That(1, Is.EqualTo(array[i]));
            }
        }
    }
    //collectionextensions.cs
    [TestFixture]
    public class testsuit_CollectionExtensions
    {
        internal class Base
        { }

        internal class DerivedA : Base
        { }
        internal class DerivedB : Base
        { }

        internal class MockRandomImpl : IRandom
        {
            //--------------------------------------------------------------------------------------
            public int next()
            {
                return 0;
            }

            //--------------------------------------------------------------------------------------
            public int next(int maxValue)
            {
                return Algorithms.clamp(0, 0, maxValue);
            }

            //--------------------------------------------------------------------------------------
            public int next(int minValue, int maxValue)
            {
                return Algorithms.clamp(0, minValue, maxValue);
            }

            //--------------------------------------------------------------------------------------
            public void nextBytes(byte[] bytes)
            {
                for (int i = 0; i < bytes.Length; ++i)
                    bytes[i] = (byte)i;
            }

            //--------------------------------------------------------------------------------------
            public double nextDouble()
            {
                return 0.0;
            }
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_filterByType()
        {
            List<Base> list = new List<Base>() { new DerivedA(), new DerivedB() };

            IEnumerable<DerivedA>   filtered_list       = list.filterByType<Base, DerivedA>();
            int                     filtered_list_count = filtered_list.Count();

            Assert.That(filtered_list_count, Is.EqualTo(1));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_removeAllBut()
        {
            List<Base> list = new List<Base>() { new DerivedA(), new DerivedB(), new DerivedB() };

            list.removeAllBut(b => (b as DerivedB) != null);

            Assert.That(list.Count, Is.EqualTo(2));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_isEmpty()
        {
            List<Base> list = new List<Base>() { new DerivedA(), new DerivedB(), new DerivedB() };

            Assert.IsFalse(list.isEmpty());

            list.Clear();

            Assert.IsTrue(list.isEmpty());
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_addRange()
        {
            List<Base> list = new List<Base>() { new DerivedA(), new DerivedB() };
            List<Base> other = new List<Base>() { new DerivedB() };

            list.addRange(null);

            Assert.That(list.Count, Is.EqualTo(2));

            list.addRange(other);

            Assert.That(list.Count, Is.EqualTo(3));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_listToString_source()
        {
            string output = string.Empty;

            List <Base> list = null;

            output = list.listToString();

            Assert.AreEqual(output, "null");

            list = new List<Base>();

            output = list.listToString();

            Assert.AreEqual(output, "[]");

            DerivedA obj_a = new DerivedA();

            list.Add(obj_a);

            output = list.listToString();

            Assert.AreEqual(output, "[" + obj_a + "]");

            DerivedB obj_b = new DerivedB();

            list.Add(obj_b);

            output = list.listToString();

            Assert.AreEqual(output, "[" + obj_a + ", " + obj_b + "]");
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_butFirst()
        {
            List<int> list = new List<int>() { 0, 1, 2, 3 };
            List<int> other = new List<int>() { 1, 2, 3 };

            int[] r = list.butFirst().ToArray();
            for(int i = 0; i < other.Count; ++i)
            {
                Assert.AreEqual(r[i], other[i]);
            }
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_butLast()
        {
            List<int> list = new List<int>() { 0, 1, 2, 3 };
            List<int> other = new List<int>() { 0, 1, 2 };

            int[] r = list.butLast().ToArray();
            for (int i = 0; i < other.Count; ++i)
            {
                Assert.AreEqual(r[i], other[i]);
            }
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_rotateLeft()
        {
            List<int> list = new List<int>() { 0, 1, 2, 3 };
            List<int> other = new List<int>() { 1, 2, 3, 0 };

            int[] r = list.rotateLeft().ToArray();
            for (int i = 0; i < other.Count; ++i)
            {
                Assert.AreEqual(r[i], other[i]);
            }
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_rotateRight()
        {
            List<int> list = new List<int>() { 0, 1, 2, 3 };
            List<int> other = new List<int>() { 3, 0, 1, 2 };

            int[] r = list.rotateRight().ToArray();
            for (int i = 0; i < other.Count; ++i)
            {
                Assert.AreEqual(r[i], other[i]);
            }
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_randomItem_source_random()
        {
            List<int> list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int value = list.randomItem(new MockRandomImpl());
            Assert.AreEqual(value, 9);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_randomItem_source_sampleCount_random()
        {
            List<int> list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] values = list.sampleRandom(2, new MockRandomImpl()).ToArray();
            Assert.AreEqual(values[0], 9);
            Assert.AreEqual(values[1], 1);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_shuffle_random()
        {
            List<int> list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> other = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };

            list.shuffle(new MockRandomImpl());

            Assert.AreEqual(list.Count, other.Count);
            for(int i = 0; i < 10; ++i)
            {
                Assert.AreEqual(list[i], other[i]);
            }
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_collectionExtensions_takeLast()
        {
            List<int> list = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<int> other = new List<int>() { 5, 6, 7, 8, 9 };

            int[] r = list.takeLast(5).ToArray();

            Assert.AreEqual(r.Length, 5);
            for(int i = 0; i < 5; ++i)
            {
                Assert.AreEqual(other[i], r[i]);
            }

            r = other.takeLast(7).ToArray();
            Assert.AreEqual(r.Length, 5);
            for (int i = 0; i < 5; ++i)
            {
                Assert.AreEqual(other[i], r[i]);
            }
        }
    }
    //listextensions.cs
    [TestFixture]
    public class testsuit_ListExtensions
    {
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_listextensions_front()
        {
            List<int> items = new List<int>() { 0, 1, 2, 3 };

            Assert.That(0, Is.EqualTo(items.front()));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_listextensions_back()
        {
            List<int> items = new List<int>() { 0, 1, 2, 3 };

            Assert.That(3, Is.EqualTo(items.back()));
        }
    }
    
}
