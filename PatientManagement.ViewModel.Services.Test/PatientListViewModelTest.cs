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
    public class PatientListViewModelTest
    {
        [TestMethod]
        public void ClearPatientListTest()
        {
            var vm = new PatientListViewModel(new SettingsServiceMock());
            var count = vm.Patients.Count;
            if (vm.ClearPatientList.CanExecute(vm.Patients))
            {
                vm.ClearPatientList.Execute(vm.Patients);
            }
            Assert.AreNotEqual(count, vm.Patients);
        }
    }
}
