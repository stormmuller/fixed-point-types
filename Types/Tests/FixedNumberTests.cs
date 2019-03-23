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
        public void ConvertsToDouble()
        {
            var leftHandSide = new Fixed32(16, 1);

            Assert.AreEqual(1d, leftHandSide.ToDouble());
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

            Assert.That(result.ToDouble(), Is.EqualTo(expectedResult).Within(0.00001));
        }
    }
}
