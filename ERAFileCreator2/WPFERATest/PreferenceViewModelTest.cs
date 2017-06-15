using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPFERA.ViewModel;

namespace WPFERATest
{
    [TestClass]
    public class PreferenceViewModelTest
    {
        [TestMethod]
        public void SaveCommandTestValueToTrue()
        {
            PreferenceViewModel Pvm = new PreferenceViewModel();

            //Arrange
            Pvm.Preference.EnablePatientReusePrompt= true;

            //Act
            Pvm.SavePreferenceCommand.Execute(true);

            var actual = Pvm.Settings.PatientPromptEnabled;
            var expected = true;

            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void SaveCommandTestValueToFalse()
        {
            PreferenceViewModel Pvm = new PreferenceViewModel();

            //Arrange
            Pvm.Preference.EnablePatientReusePrompt = false;

            //Act
            Pvm.SavePreferenceCommand.Execute(true);

            var actual = Pvm.Settings.PatientPromptEnabled;
            var expected = false;

            //Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
