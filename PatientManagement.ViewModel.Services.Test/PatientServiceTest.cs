using System;
using EraFileCreator.Mocks;
using EraFileCreator.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PatientManagement.ViewModel.Services.Test
{
    [TestClass]
    public class PatientServiceTest
    {
        [TestMethod]
        public void LoadInitialPatientTest()
        {
            //Arrange
            PatientService.SettingsService = new SettingsServiceMock();
            PatientService.DialogPrompt = new MessageBoxServiceMock();
            var patient = PatientService.LoadInitialPatient();
            //Act
            var actual = patient.FirstName;
            const string expected = "John";

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void GetNewPatientBasedOnSettingsReuseSamePatientEnabledTest()
        {
            //Arrange
            PatientService.SettingsService = new SettingsServiceMock
                {ReuseSamePatientEnabled = true};
            PatientService.DialogPrompt = new MessageBoxServiceMock();
            var patient = PatientService.LoadInitialPatient();

            //Act
            patient.FirstName = "Jacob";
            patient.LastName = "Smith";

            PatientService.PatientRepository.Add(patient);
            var patient2 = PatientService.GetNewPatientBasedOnSettings(patient);


            Assert.AreEqual(patient.FirstName, patient2.FirstName);
        }

        [TestMethod]
        public void GetNewPatientBasedOnSettingsReuseSamePatientDisabledTest()
        {
            //Arrange
            PatientService.SettingsService = new SettingsServiceMock
                {ReuseSamePatientEnabled = false};
            PatientService.DialogPrompt = new MessageBoxServiceMock();
            var patient = PatientService.LoadInitialPatient();
            var patient2 = PatientService.LoadInitialPatient();

            //Act
            patient.FirstName = "Jacob";
            patient.LastName = "Smith";

            PatientService.PatientRepository.Add(patient);
            patient2 = PatientService.GetNewPatientBasedOnSettings(patient);

            Assert.AreNotEqual(patient.FirstName, patient2.FirstName);
        }

        [TestMethod]
        public void DialogPromptTest()
        {
            //Arrange
            PatientService.SettingsService = new SettingsServiceMock
            {
                ReuseSamePatientEnabled = false,
                PatientPromptEnabled = true
            };
            PatientService.DialogPrompt = new MessageBoxServiceMock();
            var patient = PatientService.LoadInitialPatient();

            //Act
            patient.FirstName = "Jacob";
            patient.LastName = "Smith";
            PatientService.PatientRepository.Add(patient);

            Console.WriteLine(PatientService.DialogPrompt.MessageBoxMessage(patient.FullName));
        }
    }
}