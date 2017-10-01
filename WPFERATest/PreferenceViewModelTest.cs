using EraView.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WPFERATest
{
    [TestClass]
    public class PreferenceViewModelTest
    {
        [TestMethod]
        public void SaveCommandTestValueToTrue()
        {
            PreferenceViewModel pvm = new PreferenceViewModel();

            //Arrange
            pvm.Preference.EnablePatientReusePrompt= true;

            //Act
            pvm.SavePreferenceCommand.Execute(true);

            var actual = pvm.Settings.PatientPromptEnabled;
            var expected = true;

            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void SaveCommandTestValueToFalse()
        {
            PreferenceViewModel pvm = new PreferenceViewModel();

            //Arrange
            pvm.Preference.EnablePatientReusePrompt = false;

            //Act
            pvm.SavePreferenceCommand.Execute(true);

            var actual = pvm.Settings.PatientPromptEnabled;
            var expected = false;

            //Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
