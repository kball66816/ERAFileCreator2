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
            var convertDate = new DateConversion();
            DateTime date = new DateTime(2016,01,16);

            //Act
            var expected = "20160116";
            var actual = convertDate.ConvertedDate(date);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void DateTimeLongConversion()
        {
            //Arrange
            var convertDate = new DateConversion();
            DateTime date = new DateTime(2016,01,16,0,1,1);

            //Act
            var expected = "20160116";
            var actual = convertDate.ConvertedDate(date);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DateTimeMatchedToFail()
        {
            //Arrange
            var convertDate = new DateConversion();
            DateTime date = new DateTime(2016, 01, 01);
            //Act
            var expected = "20160116";
            var actual = convertDate.ConvertedDate(date);

            Assert.AreNotEqual(expected, actual);
        }
    }
}
