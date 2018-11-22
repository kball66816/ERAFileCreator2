using System;
using Common.Common.Services;
using EFC.BL;
using EraFileCreator.Mocks;
using EraFileCreator.Service.Messaging;
using PatientManagement.DAL;

namespace EraFileCreator.Services
{
    public class PatientService
    {
        public readonly IPatientRepository PatientRepository;

        public readonly ISettingsService SettingsService;

        public IMessageBoxService DialogPrompt { get;}

        public PatientService(ISettingsService settingService)
        {
            this.SettingsService = settingService;
            switch (settingService)
            {
                case SettingsService _:
                    this.DialogPrompt = new MessageBoxService();
                    this.PatientRepository = new PatientRepository();
                    break;
                case SettingsServiceMock _:
                    this.DialogPrompt = new MessageBoxServiceMock();
                    this.PatientRepository = new PatientRepositoryMock();
                    break;
                default:
                    throw new ArgumentException($"The State of Settings Service is invalid");

            }
        }

        private static Patient GetNewPatient()
        {
            return new Patient();
        }

        private Patient ClonePatient(Patient patient)
        {
            return PatientRepository.GetSelectedPatient(patient.Id).CopyPatient();
        }

        public Patient GetNewPatientBasedOnSettings(Patient patient)
        {
            if (this.SettingsService.ReuseSamePatientEnabled && this.SettingsService.PatientPromptEnabled)
                patient = PromptTypeOfNewPatient(patient);

            else if (this.SettingsService.ReuseSamePatientEnabled)
                patient = ClonePatient(patient);

            else
                patient = GetNewPatient();

            return patient;
        }

        private Patient PromptTypeOfNewPatient(Patient patient)
        {
            DialogPrompt.DisplayMessage(patient.FullName);
            patient = DialogPrompt.ShowDialog() ? ClonePatient(patient) : GetNewPatient();
            return patient;
        }

        public Patient LoadInitialPatient()
        {
            var patient = this.SettingsService.PullDefaultPatient(GetNewPatient());
            this.SettingsService.PullDefaultRenderingProvider(patient.RenderingProvider);
            return patient;
        }

        public void SaveSettings(Patient patient)
        {
            Messenger.Default.Send(new SettingsSavedMessage(), "UpdateSettings");
            this.SettingsService.SetDefaultPatient(patient);
        }
    }
}