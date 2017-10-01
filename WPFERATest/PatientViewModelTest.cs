using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFC.BL;
using System.Linq;
using PatientManagement.Model;
using PatientManagement.ViewModel;

namespace WPFERATest
{
    [TestClass]
    public class PatientViewModelTest
    {
        [TestMethod]
        public void SaveCommandTest()
        {
            //Arrange
            var Pvm = new PatientViewModel();

            //Act
            Pvm.SaveFileCommand.Execute(true);

           //Assert
           //Test passes. File Command Opens
        }

       

        [TestMethod]
        public void AddAddonChargeAdjustmentCommandTest()
        {
            //Arrange
            var pvm = new PatientViewModel();
            var preference = new Preference()
            {
                EnableAddonReusePrompt = false
            };
            pvm.AddonAdjustment.AdjustmentAmount = 100;

            //Act
            pvm.AddAddonCommand.Execute(true);
            pvm.AddAddonChargeAdjustmentCommand.Execute(true);
            var expected = 100;
            var actual = pvm.Addon.AdjustmentList.Last().AdjustmentAmount;
            //Assert

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateRenderingProviderCommandIsAlsoRenderingTrueTest()
        {
            //Arrange
            var Pvm = new PatientViewModel();

            Pvm.BillingProvider.FirstName = "John";
            Pvm.BillingProvider.IsAlsoRendering = true;
            //Act
            Pvm.UpdateRenderingProviderCommand.Execute(true);
            
            var expected = "John";
            var actual = Pvm.SelectedPatient.RenderingProvider.FirstName;
            //Assert

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdateRenderingProviderCommandIsAlsoRenderingFalseTest()
        {
            //Arrange
            var Pvm = new PatientViewModel();

            Pvm.BillingProvider.FirstName = "John";
            Pvm.BillingProvider.IsAlsoRendering = false;
            //Act
            Pvm.UpdateRenderingProviderCommand.Execute(true);

            string expected = null;
            var actual = Pvm.SelectedPatient.RenderingProvider.FirstName;
            //Assert

            Assert.AreEqual(expected, actual);
        }

    }
}
