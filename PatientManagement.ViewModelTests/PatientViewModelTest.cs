using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientManagement.ViewModel;
using System;
using PatientManagement.Model;

namespace PatientManagement.ViewModelTests
{
    [TestClass]
    public class PatientViewModelTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("A");
        }

        [TestMethod]
        public void TestMethod2()
        {
            var cvm = new PrimaryChargeViewModel();
            cvm.SelectedCharge = new PrimaryCharge();
            cvm.AddChargeToPatientCommand.CanExecute(true);
            cvm.AddChargeToPatientCommand.Execute(null);
            var pvm = new PatientViewModel();
            const int expected = 1;
            var actual = pvm.SelectedPatient.Charges.Count;
            Assert.AreEqual(expected, actual);
        }
    }
}
