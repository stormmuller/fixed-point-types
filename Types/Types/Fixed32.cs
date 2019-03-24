namespace Types
{
    public struct Fixed32
    {
        public readonly int Scale;
        public const int Epsilon = 1;

        private const int FractionMask = 0xffff;
        private const int DefaultScale = 16;

        public long RawValue { get; private set; }

        public Fixed32(int scale) : this(scale, 0) { }

        public Fixed32(int scale, int wholeNumber)
        {
            this.Scale = scale;
            this.RawValue = wholeNumber << scale;
        }

        public int WholeNumber
        {
            get
            {
                return (int)(this.RawValue >> this.Scale) +
                    (this.RawValue < 0 && this.Fraction != 0 ? 1 : 0);
            }
        }

        public int Fraction
        {
            get
            {
                return (int)(this.RawValue & FractionMask);
            }
        }

        public static Fixed32 operator +(Fixed32 leftHandSide, Fixed32 rightHandSide)
        {
            leftHandSide.RawValue += rightHandSide.RawValue;

            return leftHandSide;
        }

        public static Fixed32 operator -(Fixed32 leftHandSide, Fixed32 rightHandSide)
        {
            leftHandSide.RawValue -= rightHandSide.RawValue;

            return leftHandSide;
        }

        public static Fixed32 operator *(Fixed32 leftHandSide, Fixed32 rightHandSide)
        {
            var result = leftHandSide.RawValue * rightHandSide.RawValue;

            leftHandSide.RawValue = result >> leftHandSide.Scale;

            return leftHandSide;
        }

        public static Fixed32 operator /(Fixed32 leftHandSide, Fixed32 rightHandSide)
        {
            var result = (leftHandSide.RawValue << leftHandSide.Scale) / rightHandSide.RawValue;

            leftHandSide.RawValue = result;

            return leftHandSide;
        }

        public static explicit operator double(Fixed32 number)
        {
            return (double)number.RawValue / (1 << number.Scale);
        }

        public static implicit operator int(Fixed32 number)
        {
            return number.WholeNumber;
        }

        public static implicit operator Fixed32(int number)
        {
            return new Fixed32(DefaultScale, number);
        }

        public override string ToString()
        {
            return ((double)this).ToString();
        }
    }
}
