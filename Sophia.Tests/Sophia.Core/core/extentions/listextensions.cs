using Sophia.Core.Extensions;

using NUnit.Framework;
using System.Collections.Generic;

namespace Sophia.Tests.Core
{
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
