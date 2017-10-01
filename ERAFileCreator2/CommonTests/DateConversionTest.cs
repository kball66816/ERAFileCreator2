using System;
using Common.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonTests
{
    [TestClass]
    public class DateConversionTest
    {
        [TestMethod]
        public void DateTimeShortConversion()
        {
            //Arrange

            DateTime date = new DateTime(2016,01,16);

            //Act
            var expected = "20160116";
            var actual = date.ConvertedDate();

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void DateTimeLongConversion()
        {
            //Arrange
            DateTime date = new DateTime(2016,01,16,0,1,1);

            //Act
            var expected = "20160116";
            var actual = date.ConvertedDate();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DateTimeMatchedToFail()
        {
            //Arrange
            DateTime date = new DateTime(2016, 01, 01);
            //Act
            var expected = "20160116";
            var actual = date.ConvertedDate();

            Assert.AreNotEqual(expected, actual);
        }
    }
}
