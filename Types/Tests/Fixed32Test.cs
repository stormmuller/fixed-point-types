using NUnit.Framework;
using Types;
using Types.Exceptions;

namespace Tests
{
    [TestFixture]
    public class Fixed32Test
    {
        [Test]
        public void InitializesWithCorrectIntegerValue()
        {
            var number = new Fixed32(4238);

            Assert.AreEqual("4238", number.ToString());
        }

        [Test]
        public void InitializesWithCorrectDecimalValue()
        {
            var number = new Fixed32(1, 1);

            Assert.AreEqual("1.00001", number.ToString());
        }

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 65536)]
        [TestCase(1, 1, 65537)]
        [TestCase(3, 5, 196613)]
        public void SetsCorrectRawValue(int intPart, int decimalPart, long expectedRawValue)
        {
            var number = new Fixed32(intPart, decimalPart);

            Assert.AreEqual(expectedRawValue, number.RawValue);
        }


        [Test]
        [TestCase(2, 3, 5)]
        [TestCase(2000, 3, 2003)]
        [TestCase(200, 1, 201)]
        [TestCase(200, 0, 200)]
        [TestCase(200, -1, 199)]
        public void CorrectlyAddsIntegerParts(int lhs, int rhs, int result)
        {
            var number1 = new Fixed32(lhs);
            var number2 = new Fixed32(rhs);

            var expectedResult = new Fixed32(result);

            var total = number1 + number2;

            Assert.AreEqual(expectedResult, total);
            Assert.AreEqual(result.ToString(), total.ToString());
        }

        [Test]
        [TestCase(2, 3, 5, "0.00005")]
        [TestCase(2000, 3, 2003, "0.02003")]
        [TestCase(200, 1, 201, "0.00201")]
        [TestCase(200, 0, 200, "0.002")]
        public void CorrectlyAddsDecimalParts(int lhs, int rhs, int result, string resultString)
        {
            var number1 = new Fixed32(0, lhs);
            var number2 = new Fixed32(0, rhs);

            var expectedResult = new Fixed32(0, result);

            var total = number1 + number2;

            Assert.AreEqual(expectedResult, total);
            Assert.AreEqual(resultString, total.ToString());
        }

        [Test]
        [TestCase(2, 3, -1)]
        [TestCase(3, 2, 1)]
        [TestCase(5000, 1, 4999)]
        [TestCase(1, 5000, -4999)]
        public void CorrectlySubtractsIntegerParts(int lhs, int rhs, int result)
        {
            var number1 = new Fixed32(lhs);
            var number2 = new Fixed32(rhs);

            var expectedResult = new Fixed32(result);

            var total = number1 - number2;

            Assert.AreEqual(expectedResult, total);
            Assert.AreEqual(result.ToString(), total.ToString());
        }

        [Test]
        [TestCase(3, 2, 1, "0.00001")]
        [TestCase(5000, 1, 4999, "0.04999")]
        public void CorrectlySubtractsDecimalParts(int lhs, int rhs, int result, string resultAsString)
        {
            var number1 = new Fixed32(0, lhs);
            var number2 = new Fixed32(0, rhs);

            var expectedResult = new Fixed32(0, result);

            var total = number1 - number2;

            Assert.AreEqual(expectedResult, total);
            Assert.AreEqual(resultAsString, total.ToString());
        }

        [Test]
        [TestCase(3, 1, 0, 1, 3, 0, "3")]
        [TestCase(-3, 1, 0, 1, -3, 0, "-3")]
        [TestCase(3, 10, 0, 3, 3, 7, "3.00007")]
        [TestCase(-3, 10, 0, 3, -3, 7, "-3.00007")]
        [TestCase(334, 10, 102, 3, 232, 7, "232.00007")]
        public void CorrectlySubtractsFixedNumbers(int lhsInt, int lhsDec, int rhsInt, int rhsDec, int resultInt, int resultDec, string resultAsString)
        {
            var number1 = new Fixed32(lhsInt, lhsDec);
            var number2 = new Fixed32(rhsInt, rhsDec);

            var expectedResult = new Fixed32(resultInt, resultDec);

            var total = number1 - number2;

            Assert.AreEqual(expectedResult, total);
            Assert.AreEqual(resultAsString, total.ToString());
        }

        [Test]
        [TestCase(3, 0, 1, 0, 3, 0, "3")]
        [TestCase(3, 0, 4, 0, 12, 0, "12")]
        [TestCase(2, 0, 0, 5000, 0, 10000, "0.1")]
        public void CorrectlyMultipiesFixedNumbers(int lhsInt, int lhsDec, int rhsInt, int rhsDec, int resultInt, int resultDec, string resultAsString)
        {
            var number1 = new Fixed32(lhsInt, lhsDec);
            var number2 = new Fixed32(rhsInt, rhsDec);

            var expectedResult = new Fixed32(resultInt, resultDec);

            var total = number1 * number2;

            Assert.AreEqual(expectedResult, total);
            Assert.AreEqual(resultAsString, total.ToString());
        }

        [Test]
        public void ThrowsNegativeDecimalPartException()
        {
            Assert.Throws<NegativeDecimalPartException>(() => new Fixed32(0, -1));
        }
    }
}
