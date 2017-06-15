using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFC.BL;

namespace EFC.BLTests
{
    [TestClass]
    public class PatientChargeTest
    {

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
