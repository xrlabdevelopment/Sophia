
using NUnit.Framework;
using Sophia.Platform.Extension;

namespace Sophia.Tests.Platform
{
    [TestFixture]
    public class testsuit_MinMax
    {
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_minmax_int()
        {
            MinMaxInt minmax = new MinMaxInt(0, 255);

            int level = (int)(255 * 0.5f);
            int window = 255 - 0;

            Assert.AreEqual(level, minmax.Level);
            Assert.AreEqual(window, minmax.Window);
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_minmax_float()
        {
            MinMaxFloat minmax = new MinMaxFloat(0.0f, 255.0f);

            float level = 255 * 0.5f;
            float window = 255 - 0;

            Assert.AreEqual(level, minmax.Level);
            Assert.AreEqual(window, minmax.Window);
        }
    }
}
