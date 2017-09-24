using Common.Common;
using EFC.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientManagement.Model;

namespace CommonTests
{
    [TestClass]
    public class DecimalTruncateTest
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
            string textBoxExample = "14.999";
            decimal.TryParse(textBoxExample, out decimal value);
            decimal actual = value.Truncated(2);

            //Act
            decimal expected = 14.99m;

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void DecimalTruncateChargesWithNoDecimal()
        {
            //Arrange
            var charge = new Charge();
            string textBoxExample = "14";
            decimal.TryParse(textBoxExample, out decimal value);
            charge.PaymentAmount = value;
            charge.PaymentAmount.Truncated(2);

            //Act
            decimal expected = 14.00m;
            decimal actual = charge.PaymentAmount.Truncated(2);

            //Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void DecimalTruncateChargesWithNoTextValue()
        {
            //Arrange
            var charge = new Charge();
            string textBoxExample = "";
            decimal.TryParse(textBoxExample, out decimal value);
            charge.PaymentAmount = value;
            charge.PaymentAmount.Truncated(2);

            //Act
            decimal expected = 0.00m;
            decimal actual = charge.PaymentAmount.Truncated(2);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
