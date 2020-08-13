using Sophia.Core.Utilities;

using NUnit.Framework;

namespace Sophia.Tests.Core
{
    [TestFixture]
    public class testsuit_BitField
    {
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_bitfield_leftShift_integer()
        {
            BitField field = new BitField(0);

            field <<= 1;

            Assert.That(field.Value, Is.EqualTo(1 << 1));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_bitfield_leftShift_bitField()
        {
            BitField field = new BitField(0);
            BitField other = new BitField(1);

            field.shift(other);

            Assert.That(field.Value, Is.EqualTo(1 << 1));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_bitfield_AND_integer()
        {
            BitField field = new BitField(0);

            int r = field & 1;

            Assert.That(r, Is.EqualTo(1));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_bitfield_AND_bitField()
        {
            BitField field = new BitField(0);
            BitField other = new BitField(0);

            int r = field & other;

            Assert.That(r, Is.EqualTo(1));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_bitfield_OR_integer()
        {
            BitField field = new BitField(0);

            int r = field | 2;

            Assert.That(r, Is.EqualTo(3));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_bitfield_OR_bitField()
        {
            BitField field = new BitField(0);
            BitField other = new BitField(1);

            int r = field | other;

            Assert.That(r, Is.EqualTo(3));
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_bitfield_isEqual_integer()
        {
            BitField field = new BitField(0);

            Assert.IsTrue(field == 1);
            Assert.IsTrue(field.Equals(1));
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_bitfield_isEqual_bitField()
        {
            BitField field = new BitField(0);
            BitField other = new BitField(0);

            Assert.IsTrue(field != other);
            Assert.IsTrue(field.Equals(other));
        }

        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_bitfield_isNotEqual_integer()
        {
            BitField field = new BitField(0);

            Assert.IsTrue(field != 2);
        }
        //--------------------------------------------------------------------------------------
        [TestCase]
        public void test_bitfield_isNotEqual_bitField()
        {
            BitField field = new BitField(0);
            BitField other = new BitField(1);

            Assert.IsTrue(field != other);
            Assert.IsFalse(field.Equals(other));
        }
    }
}
