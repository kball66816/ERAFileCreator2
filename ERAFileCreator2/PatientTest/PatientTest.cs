using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFC.BL;
using System.Collections.Generic;

namespace PatientTest
{
    [TestClass]
    public class PatientTest
    {
        [TestMethod]
        public void PatientWithPlatformId()
        {
            //arrange
            var patient = new Patient();
            patient.IdFormatSwitch = false;
            patient.PatientFirstName = "John";
            patient.PatientLastName = "Smith";
            patient.BillId = "100";
            patient.PatientCopay = 50;


            //act
            string actual = "100-1";
            //assert
            Assert.AreEqual(actual, patient.FormattedBillId());
        }
        [TestMethod]
        public void PatientWithClassicId()
        {
            //arrange
            var patient = new Patient();
            patient.IdFormatSwitch = true;
            patient.PatientFirstName = "John";
            patient.PatientLastName = "Smith";
            patient.BillId = "100";
            patient.PatientCopay = 50;


            //act
            string actual = "10000000100SmiJoh";
            //assert
            Assert.AreEqual(actual, patient.FormattedBillId());
        }
        [TestMethod]
        public void PatientWithConcatenatedNameIfMoreThanThreeCharactersInFirstAndLastName()
        {
            //arrange
            var patient = new Patient();
            patient.IdFormatSwitch = true;
            patient.PatientFirstName = "John";
            patient.PatientLastName = "Smith";
            patient.BillId = "100";
            patient.PatientCopay = 50;


            //act
            string actual = "SmiJoh";
            //assert
            Assert.AreEqual(actual, patient.ClassicIdConcatination());

        }
        [TestMethod]
        public void PatientWithConcatenatedNameIfLessThanThreeCharactersInFirstAndLastName()
        {
            //arrange
            var patient = new Patient();
            patient.IdFormatSwitch = true;
            patient.PatientFirstName = "Ha";
            patient.PatientLastName = "Vo";
            patient.BillId = "100";
            patient.PatientCopay = 50;


            //act
            string actual = "VoHa";
            //assert
            Assert.AreEqual(actual, patient.ClassicIdConcatination());

        }

        [TestMethod]
        public void ReturnPatientList()
        {
            //arrange
            var expected = 1;
            var patient = new Patient();

            patient.IdFormatSwitch = true;
            patient.PatientFirstName = "Ha";
            patient.PatientLastName = "Vo";
            patient.BillId = "100";
            patient.PatientCopay = 50;

            var charge = new Charge();
            charge.ModifierFour = "AA";
            charge.PrimaryPaymentAmount = 50;
            patient.ChargeList.Add(charge);

            //act
            var retrievePatients = new RetrievePatients();
            retrievePatients.PatientList.Add(patient);
            var actual = retrievePatients.PatientList.Count;

            //assert
            Assert.AreEqual(expected, actual);
        }
            [TestMethod]

        public void AddChargeToPatientTest()
        {
            var actual = 1;
            var retrievePatients = new RetrievePatients();
            var patient = new Patient();

            patient.IdFormatSwitch = true;
            patient.PatientFirstName = "John";
            patient.PatientLastName = "Smith";
            patient.BillId = "100";
            patient.PatientCopay = 50;


            var charge = new Charge();
            charge.PrimaryProcedureCode = "99215";
            charge.PrimaryPaymentAmount = 50;
            patient.ChargeList.Add(charge);
            var result = patient.ChargeList.Count;

            Assert.AreEqual(actual, result);
        }
    }
}
