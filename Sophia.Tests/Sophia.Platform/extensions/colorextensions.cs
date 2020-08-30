
using NUnit.Framework;

using UnityEngine;
using Sophia.Platform.Extension;

namespace Sophia.Tests.Platform
{
    [TestFixture]
    public class testsuit_Colorextensions
    {
        //-------------------------------------------------------------------------------------
        // Constants
        private const float LightOffset = 0.0625f;
        private const float DarkerFactor = 0.9f;

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_colorextensions_lighter()
        {
            Color color = Color.red;
            Color new_color = color.lighter();
            Color expected_color = new Color(1.0f, LightOffset, LightOffset, 1.0f);
            Assert.That(new_color, Is.EqualTo(expected_color));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_colorextensions_darker()
        {
            Color color = Color.red;
            Color new_color = color.darker();
            Color expected_color = new Color(1.0f - LightOffset, 0.0f, 0.0f, 1.0f);
            Assert.That(new_color, Is.EqualTo(expected_color));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_colorextensions_brightness()
        {
            Color color = Color.red;
            float b = (color.r + color.g + color.b)/3.0f;

            Assert.IsTrue(color.brightness() - b < float.Epsilon);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_colorextensions_withBrightness()
        {
            Color color = Color.red;
            Color new_color = color.withBrightness(0.2f);

            float expected_red = 0.6f;
            float expected_green = 0.0f;
            float expected_blue = 0.0f;
            float expected_alpha = 1.0f;

            Assert.IsTrue(new_color.r - expected_red < float.Epsilon);
            Assert.IsTrue(new_color.g - expected_green < float.Epsilon);
            Assert.IsTrue(new_color.b - expected_blue < float.Epsilon);
            Assert.IsTrue(new_color.a - expected_alpha < float.Epsilon);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_colorextensions_withBrightness_AlmostBlack()
        {
            Color color = Color.red;
            Color new_color = color.withBrightness(float.Epsilon / 10.0f);
            Color expected_color = new Color(float.Epsilon * 0.1f, float.Epsilon * 0.1f, float.Epsilon * 0.1f);

            Assert.That(new_color, Is.EqualTo(expected_color));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_colorextensions_isApproximatelyBlack()
        {
            Color color = new Color(float.Epsilon * 0.1f, float.Epsilon * 0.1f, float.Epsilon * 0.1f);
            Assert.IsTrue(color.isApproximatelyBlack());
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_colorextensions_isApproximatelyWhite()
        {
            Color color = new Color(1.0f - (float.Epsilon * 0.1f), 1.0f - (float.Epsilon * 0.1f), 1.0f - (float.Epsilon * 0.1f));
            Assert.IsTrue(color.isApproximatelyWhite());
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_colorextensions_opaque()
        {
            Color color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
            Color new_color = color.opaque();
            Color expected_color = Color.red;

            Assert.That(new_color, Is.EqualTo(expected_color));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_colorextensions_withAlpha()
        {
            Color color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
            Color new_color = color.withAlpha(0.5f);
            Color expected_color = new Color(1.0f, 0.0f, 0.0f, 0.5f);

            Assert.That(new_color, Is.EqualTo(expected_color));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_colorextensions_invert()
        {
            Color color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            Color new_color = color.invert();
            Color expected_color = new Color(0.0f, 1.0f, 1.0f, 1.0f);

            Assert.That(new_color, Is.EqualTo(expected_color));
        }
    }
}
