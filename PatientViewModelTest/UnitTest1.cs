using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientManagement.ViewModel;
using System;

namespace PatientViewModelTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var pvm = new PatientViewModel();

            Console.WriteLine(pvm.SelectedPatient.FirstName);

        }
    }
}
