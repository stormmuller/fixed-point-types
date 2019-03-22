namespace Types
{
    using System;

    public struct Fixed32
    {
        private const int DECIMAL_SHIFT = 16;

        // We could use a 32-bit integer, but we would run into overflow issues when performing certain
        // arithmetic operations.
        // TODO: optimize to use a 32-bit integer and handle overflows correctly when performing arithmetic operations
        internal Int64 rawValue;

        public Fixed32(int intergerPart)
        {
            rawValue = intergerPart << DECIMAL_SHIFT;
        }

        public Fixed32(int integerPart, int decimalPart)
        {
            rawValue = (integerPart << DECIMAL_SHIFT) + (decimalPart & 0xffff);
        }

        private Fixed32(float value)
        {
            int wholePortion = (int)Math.Truncate(value);
            int decimalPortion = (int)(Math.Abs(value - wholePortion) * (1 << 16));

            if (wholePortion < 0)
            {
                wholePortion--; // If we subtracted 0.5 from -10 or added 0.5 to -11 we'd get integer portion of -11 and decimal portion of 0.5, so we ensure the same behavior exists in a float conversion.
            }

            rawValue = (wholePortion << DECIMAL_SHIFT) | (decimalPortion & 0xffff);
        }

        public static implicit operator Fixed32(int wholeInteger)
        {
            return new Fixed32(wholeInteger);
        }

        public static implicit operator int(Fixed32 fixedInt)
        {
            return fixedInt.IntegerPart;
        }

        public static explicit operator Fixed32(float floatVal)
        {
            return new Fixed32(floatVal);
        }

        public static explicit operator float(Fixed32 fixedInt)
        {
            return fixedInt.ToFloat();
        }

        private Int16 IntegerPart
        {
            get
            {
                return (Int16)((rawValue >> DECIMAL_SHIFT) + (this.rawValue < 0 ? 1 : 0));
            }
        }

        private UInt16 DecimalPart
        {
            get
            {
                return (UInt16)(rawValue & 0xffff);
            }
        }

        public float ToFloat()
        {
            var decimalPartAsFloat = DecimalPart / (float)(1 << DECIMAL_SHIFT);

            // not using the 'IntegerPart' property, as we want to skip the handling of negative values.
            var integerPartAsInt = rawValue >> DECIMAL_SHIFT;

            return integerPartAsInt + decimalPartAsFloat;
        }

        public static Fixed32 operator +(Fixed32 leftHandSide, Fixed32 rightHandSide)
        {
            leftHandSide.rawValue += rightHandSide.rawValue;
            return leftHandSide;
        }

        public static Fixed32 operator -(Fixed32 leftHandSide, Fixed32 rightHandSide)
        {
            leftHandSide.rawValue -= rightHandSide.rawValue;
            return leftHandSide;
        }

        public override string ToString()
        {
            return $"{IntegerPart}.{DecimalPart.ToString().PadLeft(5, '0')}";
        }
    }
}
