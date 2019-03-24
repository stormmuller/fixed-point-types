namespace FixedTypes.Tests
{
    using NUnit.Framework;
    using Types;

    [TestFixture]
    public class FixedNumberTests
    {
        [Test]
        public void DefaultsTo0RawValue()
        {
            var number = new Fixed32(16);

            Assert.AreEqual(0, number.RawValue);
        }

        [Test]
        public void WholeNumberConstructorAddsWholeNumber()
        {
            var number = new Fixed32(16, 1);

            Assert.AreEqual(1, number.WholeNumber);
        }

        [Test]
        public void WholeNumberConstructorDoesNotAddFraction()
        {
            var number = new Fixed32(16, 1);

            Assert.AreEqual(0, number.Fraction);
        }

        [Test]
        public void AddsWholeNumbers()
        {
            var leftHandSide = new Fixed32(16, 2);
            var rightHandSide = new Fixed32(16, 3);

            var result = leftHandSide + rightHandSide;

            Assert.AreEqual(5, result.WholeNumber);
            Assert.AreEqual(0, result.Fraction);
        }

        [Test]
        public void SubrtactsWholeNumbers()
        {
            var leftHandSide = new Fixed32(16, 2);
            var rightHandSide = new Fixed32(16, 3);

            var result = leftHandSide - rightHandSide;

            Assert.AreEqual(-1, result.WholeNumber);
            Assert.AreEqual(0, result.Fraction);
        }

        [Test]
        [TestCase(1, 2, 2, 2.5d)]
        [TestCase(3, 2, 2, 3.5d)]
        [TestCase(-3, 2, 2, 0.5d)]
        [TestCase(1, 6, 84, 84.1666666666666667d)]
        public void AddsFractionalNumbers(int fractionNumerator, int fractionDenominator, int wholeNumberToAddTo, double expected)
        {
            var leftHandSide = new Fixed32(16, fractionNumerator) / new Fixed32(16, fractionDenominator);
            var rightHandSide = new Fixed32(16, wholeNumberToAddTo);

            var result = leftHandSide + rightHandSide;

            Assert.That((double)result, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        [TestCase(1, 2, 2, -1.5d)]
        [TestCase(3, 2, 2, -0.5d)]
        [TestCase(-3, 2, 2, -3.5d)]
        [TestCase(1, 6, 84, -83.83333333333333d)]
        public void SubtractsFractionalNumbers(int fractionNumerator, int fractionDenominator, int wholeNumberToAddTo, double expected)
        {
            var leftHandSide = new Fixed32(16, fractionNumerator) / new Fixed32(16, fractionDenominator);
            var rightHandSide = new Fixed32(16, wholeNumberToAddTo);

            var result = leftHandSide - rightHandSide;

            Assert.That((double)result, Is.EqualTo(expected).Within(0.0001));
        }

        [Test]
        public void ConvertsToDouble()
        {
            var leftHandSide = new Fixed32(16, 1);

            Assert.AreEqual(1d, (double)leftHandSide);
        }

        [Test]
        public void MultipliesWholeNumbers()
        {
            var leftHandSide = new Fixed32(16, 9);
            var rightHandSide = new Fixed32(16, 4);

            var result = leftHandSide * rightHandSide;

            Assert.AreEqual(36, result.WholeNumber);
        }

        [Test]
        [TestCase(14, 2, 7)]
        [TestCase(2, 2, 1)]
        [TestCase(-2, 2, -1)]
        [TestCase(-14, 2, -7)]
        [TestCase(36, 4, 9)]
        [TestCase(-36, 4, -9)]
        [TestCase(36, -4, -9)]
        public void DividesWholeNumbers(int number1, int number2, int expectedResult)
        {
            var numerator = new Fixed32(16, number1);
            var denomenator = new Fixed32(16, number2);

            var result = numerator / denomenator;

            Assert.AreEqual(expectedResult, result.WholeNumber);
        }

        [Test]
        [TestCase(1, 2, 0.5d)]
        [TestCase(-1, 2, -0.5d)]
        [TestCase(1, -2, -0.5d)]
        [TestCase(-1, -2, 0.5d)]
        [TestCase(22, 7, 3.142857142857143d)]
        [TestCase(1, 3, 0.333333333333334d)]
        public void DividesFractionalNumbers(int number1, int number2, double expectedResult)
        {
            var numerator = new Fixed32(16, number1);
            var denomenator = new Fixed32(16, number2);

            var result = numerator / denomenator;

            Assert.That((double)result, Is.EqualTo(expectedResult).Within(0.0001));
        }

        [Test]
        [TestCase(-1, 2, 0)]
        [TestCase(1, -2, 0)]
        [TestCase(22, 7, 3)]
        [TestCase(22, -7, -3)]
        public void CalculatesWholeNumberCorrectlyWhenNumberIdFraction(int number1, int number2, int expectedResult)
        {
            var numerator = new Fixed32(16, number1);
            var denomenator = new Fixed32(16, number2);

            var result = numerator / denomenator;

            Assert.AreEqual(expectedResult, result.WholeNumber);
        }

        [Test]
        [TestCase(1, 2, 0)]
        [TestCase(-1, 2, 0)]
        [TestCase(1, -2, 0)]
        [TestCase(-1, -2, 0)]
        [TestCase(22, 7, 3)]
        [TestCase(22, -7, -3)]
        [TestCase(1, 3, 0)]
        public void ImplicitIntegerOperatorReturnsWholeNumber(int number1, int number2, int expectedResult)
        {
            var numerator = new Fixed32(16, number1);
            var denomenator = new Fixed32(16, number2);

            var result = numerator / denomenator;
            int integerValue = result;

            Assert.AreEqual(result.WholeNumber, integerValue);
            Assert.AreEqual(expectedResult, integerValue);
        }
    }
}
