using Sophia.Core.Algorithms;

using NUnit.Framework;

using System.Collections.Generic;
using System.Linq;

namespace Sophia.Tests.Core
{
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
}
