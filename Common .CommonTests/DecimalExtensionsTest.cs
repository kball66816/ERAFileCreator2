using Common.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.CommonTests
{
    [TestClass]
    public class DecimalExtensionsTest
    {
        [TestMethod]
        public void DecimalTruncate()
        {
            Assert.AreEqual(-1.12m, -1.129m.Truncated(2));
            Assert.AreEqual(-1.12m, -1.120m.Truncated(2));
            Assert.AreEqual(-1.12m, -1.125m.Truncated(2));
            Assert.AreEqual(-1.12m, -1.1255m.Truncated(2));
            Assert.AreEqual(-1.12m, -1.1254m.Truncated(2));
            Assert.AreEqual(0m, 0.0001m.Truncated(3));
            Assert.AreEqual(0m, -0.0001m.Truncated(3));
            Assert.AreEqual(0m, -0.0000m.Truncated(3));
            Assert.AreEqual(0m, 0.0000m.Truncated(3));
            Assert.AreEqual(1.1m, 1.12m.Truncated(1));
            Assert.AreEqual(1.1m, 1.15m.Truncated(1));
            Assert.AreEqual(1.1m, 1.19m.Truncated(1));
            Assert.AreEqual(1.1m, 1.111m.Truncated(1));
            Assert.AreEqual(1.1m, 1.199m.Truncated(1));
            Assert.AreEqual(1.2m, 1.2m.Truncated(1));
            Assert.AreEqual(0.1m, 0.14m.Truncated(1));
            Assert.AreEqual(0, -0.05m.Truncated(1));
            Assert.AreEqual(0, -0.049m.Truncated(1));
            Assert.AreEqual(0, -0.051m.Truncated(1));
            Assert.AreEqual(-0.1m, -0.14m.Truncated(1));
            Assert.AreEqual(-0.1m, -0.15m.Truncated(1));
            Assert.AreEqual(-0.1m, -0.16m.Truncated(1));
            Assert.AreEqual(-0.1m, -0.19m.Truncated(1));
            Assert.AreEqual(-0.1m, -0.199m.Truncated(1));
            Assert.AreEqual(-0.1m, -0.101m.Truncated(1));
            Assert.AreEqual(0m, -0.099m.Truncated(1));
            Assert.AreEqual(0m, -0.001m.Truncated(1));
            Assert.AreEqual(1m, 1.99m.Truncated(0));
            Assert.AreEqual(1m, 1.01m.Truncated(0));
            Assert.AreEqual(-1m, -1.99m.Truncated(0));
            Assert.AreEqual(-1m, -1.01m.Truncated(0));
            Assert.AreEqual(0m, 0.0001m.Truncated(2));
            Assert.AreEqual(0.00m, 0m.Truncated(2));
        }

        [TestMethod]
        public void DecimalTruncateChargesWithExtraDigit()
        {
            //Arrange
            var textBoxExample = "14.999";
            decimal.TryParse(textBoxExample, out var value);
            var actual = value.Truncated(2);

            //Act
            var expected = 14.99m;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DecimalTruncateChargesWithNoDecimal()
        {
            //Arrange
            var textBoxExample = "14";
            decimal.TryParse(textBoxExample, out var value);
            var cost = value;

            //Act
            var expected = 14.00m;
            var actual = cost.Truncated(2);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DecimalTruncateChargesWithNoTextValue()
        {
            //Arrange
            var textBoxExample = "";
            decimal.TryParse(textBoxExample, out var value);
            var cost = value;

            //Act
            var expected = 0.00m;
            var actual = cost.Truncated(2);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}