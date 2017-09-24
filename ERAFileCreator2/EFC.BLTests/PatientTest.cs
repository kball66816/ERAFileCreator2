using EFC.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientManagement.Model;

namespace EFC.BLTests
{
    [TestClass]
    public class PatientTest
    {
        [TestMethod]
        public void PatientWithPlatformId()
        {
            //arrange
            var patient = new Patient
            {
                UsePlatformId = true,
                FirstName = "John",
                LastName = "Smith",
                BillId = "100",
            };


            //act
            string actual = "100";
            string expected = patient.FormattedBillId;
            //assert
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void PatientWithClassicId()
        {
            //arrange
            var patient = new Patient
            {
                UsePlatformId = false,
                FirstName = "John",
                LastName = "Smith",
                BillId = "100",
            };


            //act
            string actual = "10000000100SMIJOH";
            //assert
            Assert.AreEqual(actual, patient.FormattedBillId);
        }
        [TestMethod]
        public void PatientWithConcatenatedNameIfMoreThanThreeCharactersInFirstAndLastName()
        {
            //arrange
            var patient = new Patient()
            {
                UsePlatformId = false,
                FirstName = "John",
                LastName = "Smith",
                BillId = "100",
            };


            //act
            string actual = "10000000100SMIJOH";
            //assert
            Assert.AreEqual(actual, patient.FormattedBillId);

        }
        [TestMethod]
        public void PatientWithConcatenatedNameIfLessThanThreeCharactersInFirstAndLastName()
        {
            //arrange
            var patient = new Patient
            {
                UsePlatformId = false,
                FirstName = "Ha",
                LastName = "Vo",
                BillId = "100",
            };


            //act
            string actual = "10000000100VOHA";
            //assert
            Assert.AreEqual(actual, patient.FormattedBillId);

        }

        [TestMethod]
        public void CopyPatient()
        {
            //arrange
            var patient = new Patient()
            {
                UsePlatformId = true,
                FirstName = "Ha",
                LastName = "Vo",
                BillId = "100",
            };

            var patient2 = patient.CopyPatient();
            //act
            patient2.BillId = "105";
            var actual = patient.BillId;
            var expected = "105";

            //assert
            Assert.AreNotEqual(expected, actual);
        }
    }
}
