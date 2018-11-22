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
            var patientService =
                new PatientService(new SettingsServiceMock());
            var patient = patientService.LoadInitialPatient();
            var actual = patient.FirstName;
            const string expected = "John";

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void GetNewPatientBasedOnSettingsReuseSamePatientEnabledTest()
        {

            var patientService =
                new PatientService(new SettingsServiceMock());
            var patient = patientService.LoadInitialPatient();
            patientService.SettingsService.ReuseSamePatientEnabled = true;
            patient.FirstName = "Jacob";
            patient.LastName = "Smith";

            patientService.PatientRepository.Add(patient);
            var patient2 = patientService.GetNewPatientBasedOnSettings(patient);

            Assert.AreEqual(patient.FirstName, patient2.FirstName);
        }

        [TestMethod]
        public void GetNewPatientBasedOnSettingsReuseSamePatientDisabledTest()
        {
            var patientService =
                new PatientService(new SettingsServiceMock());
            var patient = patientService.LoadInitialPatient();
            var patient2 = patientService.LoadInitialPatient();

            patient.FirstName = "Jacob";
            patient.LastName = "Smith";

            patientService.PatientRepository.Add(patient);
            patient2 = patientService.GetNewPatientBasedOnSettings(patient);

            Assert.AreNotEqual(patient.FirstName, patient2.FirstName);
        }

        [TestMethod]
        public void DialogPromptTest()
        {
            var patientService =
                new PatientService(new SettingsServiceMock());
            var patient = patientService.LoadInitialPatient();

            patient.FirstName = "Jacob";
            patient.LastName = "Smith";
            patientService.PatientRepository.Add(patient);

            Console.WriteLine(patientService.DialogPrompt.MessageBoxMessage(patient.FullName));
        }
    }
}