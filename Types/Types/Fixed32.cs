namespace Types
{
    public struct Fixed32
    {
        public int Scale { get; private set; }
        public long RawValue { get; private set; }
        public int FractionMask { get; private set; }
        public const int Epsilon = 1;

        public Fixed32(int scale) : this(scale, 0) { }

        public Fixed32(int scale, int wholeNumber)
        {
            this.Scale = scale;
            this.RawValue = wholeNumber << scale;
            this.FractionMask = 0xffff;
        }

        public int WholeNumber
        {
            get
            {
                return (int)(this.RawValue >> this.Scale);
            }
        }

        public int Fraction
        {
            get
            {
                return (int)(this.RawValue & this.FractionMask);
            }
        }

        public static Fixed32 operator +(Fixed32 leftHandSide, Fixed32 rightHandSide)
        {
            leftHandSide.RawValue += rightHandSide.RawValue;

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

        public double ToDouble()
        {
            return (double)this.RawValue / (1 << this.Scale);
        }

        public override string ToString()
        {
            return ToDouble().ToString();
        }
    }
}
