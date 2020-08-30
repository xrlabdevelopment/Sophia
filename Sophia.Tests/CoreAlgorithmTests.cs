using Sophia.Core.Algorithms;
using NUnit.Framework;

namespace Sophia.Tests.Core
{
    //clamp.cs
     [TestFixture]
    public class testsuit_Clamp
    {
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_clamp_min_value()
        {
            int min = 0;
            int max = 10;

            int value = -5;

            Assert.That(Algorithms.clamp(value, min, max), Is.EqualTo(min));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_clamp_max_value()
        {
            int min = 0;
            int max = 10;

            int value = 15;

            Assert.That(Algorithms.clamp(value, min, max), Is.EqualTo(max));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_clamp_actual_value()
        {
            int min = 0;
            int max = 10;

            int value = 5;

            Assert.That(Algorithms.clamp(value, min, max), Is.EqualTo(value));
        }
    }
    //random.cs
       internal class MockRandomImpl : IRandom
    {
        //--------------------------------------------------------------------------------------
        public int next()
        {
            return 5;
        }

        //--------------------------------------------------------------------------------------
        public int next(int maxValue)
        {
            return Algorithms.clamp(5, 0, maxValue);
        }

        //--------------------------------------------------------------------------------------
        public int next(int minValue, int maxValue)
        {
            return Algorithms.clamp(5, minValue, maxValue);
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
            return 5.0;
        }
    }

    [TestFixture]
    public class testsuit_Random
    {
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_random_nextDouble()
        {
            MockRandomImpl mock_random = new MockRandomImpl();

            Assert.That(mock_random.nextDouble(), Is.EqualTo(5.0));
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_random_nextBytes()
        {
            MockRandomImpl mock_random = new MockRandomImpl();

            byte[] bytes = new byte[10];

            mock_random.nextBytes(bytes);

            for (int i = 0; i < 10; ++i)
                Assert.That(bytes[i] == (byte)i);

        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_random_nextInteger()
        {
            MockRandomImpl mock_random = new MockRandomImpl();

            Assert.That(mock_random.next(), Is.EqualTo(5));
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_random_nextInteger_maxValue()
        {
            MockRandomImpl mock_random = new MockRandomImpl();

            Assert.That(mock_random.next(10), Is.EqualTo(5));
            Assert.That(mock_random.next(3), Is.EqualTo(3));
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_random_nextInteger_minMaxValue()
        {
            MockRandomImpl mock_random = new MockRandomImpl();

            Assert.That(mock_random.next(0, 10), Is.EqualTo(5));
            Assert.That(mock_random.next(7, 10), Is.EqualTo(7));
            Assert.That(mock_random.next(0, 3), Is.EqualTo(3));
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_random_ToString()
        {
            MockRandomImpl mock_random = new MockRandomImpl();

            Assert.That(mock_random.GetType().ToString(), Is.EqualTo(mock_random.ToString()));
        }
    }
}