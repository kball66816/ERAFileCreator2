using System;
using System.Linq;
using EraFileCreator.Mocks;
using EraFileCreator.Services;
using EraFileCreator.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientManagement.DAL;


namespace PatientManagement.ViewModel.Services.Test
{
    [TestClass]
    public class PatientViewModelTest
    {
        [TestMethod]
        public void CanAddNewPatientIsTrueTest()
        {
            var vm = new PatientViewModel(new SettingsServiceMock());
            var patient = new Patient
            {
                FirstName = "Eric",
                LastName = "Johnson"
            };

            var charge = new ServiceDescription();
            patient.Charges.Add(charge);
            if (vm.AddPatientCommand.CanExecute(patient))
            Assert.AreEqual(true, vm.AddPatientCommand.CanExecute(patient));
        }
        [TestMethod]
        public void CanAddNewPatientIsFalseTest()
        {
            var vm = new PatientViewModel(new SettingsServiceMock());
            var patient = new Patient
            {
                FirstName = "John",
                LastName = "Smith"
            };
            Assert.AreEqual(false, vm.AddPatientCommand.CanExecute(patient));
        }
    }
}
