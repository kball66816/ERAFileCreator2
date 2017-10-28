﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientManagement.ViewModel;
using PatientManagement.ViewModel.Services;

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

            var actual = SettingsService.PatientPromptEnabled;
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

            var actual = SettingsService.PatientPromptEnabled;
            var expected = false;

            //Assert
            Assert.AreEqual(actual, expected);
        }
    }
}
