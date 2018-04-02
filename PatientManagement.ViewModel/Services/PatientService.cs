using Common.Common.Services;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Service.Messaging;

namespace PatientManagement.ViewModel.Services
{
    public static class PatientService
    {
        static PatientService()
        {
            PatientRepository = new PatientRepository();
            SettingsService = new SettingsService();
        }

        public static readonly IPatientRepository PatientRepository;

        public static ISettingsService SettingsService;

        public static IMessageBoxService DialogPrompt = new MessageBoxService();

        private static Patient GetNewPatient()
        {
            return new Patient();
        }

        private static Patient ClonePatient(Patient patient)
        {
            return PatientRepository.GetSelectedPatient(patient.Id).CopyPatient();
        }

        public static Patient GetNewPatientBasedOnSettings(Patient patient)
        {
            if (SettingsService.ReuseSamePatientEnabled && SettingsService.PatientPromptEnabled)
            {
                patient = PromptTypeOfNewPatient(patient);
            }

            else if (SettingsService.ReuseSamePatientEnabled)
            {
                patient = ClonePatient(patient);
            }

            else
            {
                patient = GetNewPatient();
            }

            return patient;
        }

        private static Patient PromptTypeOfNewPatient(Patient patient)
        {
            DialogPrompt.DisplayMessage(patient.FullName);
            patient = DialogPrompt.ShowDialog() ? ClonePatient(patient) : GetNewPatient();
            return patient;
        }
        public static Patient LoadInitialPatient()
        {
            return SettingsService.PullDefaultPatient(GetNewPatient());
        }

        public static void SaveSettings(Patient patient)
        {
            Messenger.Default.Send(new SettingsSavedMessage(), "UpdateSettings");
            SettingsService.SetDefaultPatient(patient);
        }
    }
}
