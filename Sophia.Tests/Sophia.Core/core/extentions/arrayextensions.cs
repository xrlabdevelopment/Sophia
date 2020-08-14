using Sophia.Core.Extensions;

using NUnit.Framework;

namespace Sophia.Tests.Core
{
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
}
