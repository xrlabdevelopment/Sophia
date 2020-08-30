using System.Diagnostics;

namespace Sophia.Core.Utilities
{
    public class BitField
    {
        //-------------------------------------------------------------------------------------
        // Properties
        public int Value
        {
            get { return bit_field; }
        }

        //-------------------------------------------------------------------------------------
        // Fields
        private int bit_field;

        //-------------------------------------------------------------------------------------
        public static BitField generateBitField(int value)
        {
            return new BitField(value);
        }
        //-------------------------------------------------------------------------------------
        public static int generateBitFieldValue(int value)
        {
            long x = 1 << value;

            Debug.Assert(x < int.MaxValue, "Out of range");

            return x <= int.MaxValue
                ? 1 << value
                : 0;
        }

        //-------------------------------------------------------------------------------------
        public BitField()
        {
            bit_field = 0;
        }
        //-------------------------------------------------------------------------------------
        public BitField(int value)
        {
            long x = 1 << value;

            Debug.Assert(x < int.MaxValue, "Out of range");

            bit_field = x <= int.MaxValue
                ? 1 << value
                : 0;
        }
        //-------------------------------------------------------------------------------------
        public BitField(BitField field)
        {
            bit_field = field.bit_field;
        }

        //-------------------------------------------------------------------------------------
        public void shift(BitField other)
        {
            int field_1 = this.bit_field;
            int field_2 = other.bit_field;

            long x = field_1 << field_2;

            Debug.Assert(x < int.MaxValue, "Out of range");

            this.bit_field = x <= int.MaxValue
                ? other.bit_field
                : 0;
        }

        //-------------------------------------------------------------------------------------
        public override bool Equals(object obj)
        {
            BitField other = (obj as BitField);
            if(other != null)
            {
                return this == other.Value;
            }

            System.Int32? i = (obj as System.Int32?);
            if(i != null)
            {
                if (!i.HasValue)
                    return false;

                return this == i.Value;
            }

            return false;
        }
        //-------------------------------------------------------------------------------------
        public override int GetHashCode()
        {
            return bit_field.GetHashCode();
        }
        //-------------------------------------------------------------------------------------
        public override string ToString()
        {
            return bit_field.ToString();
        }

        //-------------------------------------------------------------------------------------
        public static bool operator ==(BitField field, int value)
        {
            return field.Value == value;
        }
        public static bool operator !=(BitField field, int value)
        {
            return field.Value != value;
        }

        //-------------------------------------------------------------------------------------
        public static BitField operator <<(BitField field, int value)
        {
            long x = 1 << value;

            Debug.Assert(x < int.MaxValue, "Out of range");

            field.bit_field = x <= int.MaxValue
                ? 1 << value
                : 0;

            return field;
        }

        //-------------------------------------------------------------------------------------
        public static int operator &(BitField field, int value)
        {
            Debug.Assert(value < int.MaxValue, "Out of range");

            if (value > int.MaxValue)
                return -1;

            return (field.bit_field & value);
        }
        //-------------------------------------------------------------------------------------
        public static int operator &(BitField field, BitField other)
        {
            return (field.bit_field & other.bit_field);
        }
        //-------------------------------------------------------------------------------------
        public static int operator |(BitField field, int value)
        {
            Debug.Assert(value < int.MaxValue, "Out of range");

            if (value > int.MaxValue)
                return -1;

            return (field.bit_field | value);
        }
        //-------------------------------------------------------------------------------------
        public static BitField operator |(BitField field, BitField other)
        {
            BitField new_field = new BitField();

            new_field.bit_field = (field.bit_field | other.bit_field);

            return new_field;
        }
    }
}
