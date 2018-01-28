using System;
using Common.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.CommonTests
{
    [TestClass]
    public class DateConversionTest
    {
        [TestMethod]
        public void CastDateTimeToString()
        {
            var now = DateTime.Today;
            var converted = now.DateToYearFirstShortString();
            Assert.AreEqual(now.ToString("yyyyMMdd"), converted);
        }

        [TestMethod]
        public void DateTimeShortConversion()
        {
            //Arrange
            var date = new DateTime(2016, 01, 16);

            //Act
            var expected = "20160116";
            var actual = date.DateToYearFirstShortString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DateTimeLongConversion()
        {
            //Arrange
            var date = new DateTime(2016, 01, 16, 0, 1, 1);

            //Act
            var expected = "20160116";
            var actual = date.DateToYearFirstShortString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DateTimeMatchedToFail()
        {
            //Arrange
            var date = new DateTime(2016, 01, 01);
            //Act
            var expected = "20160116";
            var actual = date.DateToYearFirstShortString();

            Assert.AreNotEqual(expected, actual);
        }
    }
}