using Common.Common.Services;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.ComponentModel;
using System.Windows.Input;
using PatientManagement.ViewModel.Service.Messaging;

namespace PatientManagement.ViewModel
{
    public class PatientViewModel : INotifyPropertyChanged
    {
        private readonly IPatientRepository patientRepository;

        private Patient selectedPatient;

        public PatientViewModel()
        {
            LoadInitialPatient();
            Messenger.Default.Register<InitializationCompleteMessage>(this, OnInitializationComplete);
            Messenger.Default.Register<Provider>(this, OnProviderReceived, "AddRenderingProvider");
            Messenger.Default.Register<SaveFileMessage>(this, OnSaveFileMessage, "SaveTextFiletoSelectedDirectory");
            Messenger.Default.Register<ResumeManualActionMessage>(this, OnResumeManualActionMessage, "Disable");
            patientRepository = new PatientRepository();
            AddPatientCommand = new Command(AddPatient, CanAddPatient);
            isAddPatientEnabled = true;
        }

        private bool isAddPatientEnabled;

        private void OnResumeManualActionMessage(ResumeManualActionMessage resume)
        {
            isAddPatientEnabled = resume.IsEnabled;
            if (isAddPatientEnabled)
            {
                LoadInitialPatient();
                patientRepository.Add(SelectedPatient);
            }
        }

        public Patient SelectedPatient
        {
            get => selectedPatient;
            set
            {
                if (value == selectedPatient) return;
                selectedPatient = value;
                RaisePropertyChanged("SelectedPatient");
                SendPatientCharges();
            }
        }

        public ICommand AddPatientCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnInitializationComplete(InitializationCompleteMessage message)
        {
            patientRepository.Add(SelectedPatient);
            SendPatientId();
        }

        private void OnSaveFileMessage(SaveFileMessage obj)
        {
            SaveSettings();
        }

        private void OnProviderReceived(Provider provider)
        {
            selectedPatient.RenderingProvider = provider;
            RaisePropertyChanged("SelectedPatient");
        }

        private void SendPatientCharges()
        {
            Messenger.Default.Send(selectedPatient.Charges, "UpdateChargesList");
        }

        private void LoadInitialPatient()
        {
            SelectedPatient = new Patient();
            SelectedPatient = SettingsService.PullDefaultPatient();
            SendPatientId();
        }

        private void SendPatientId()
        {
            Messenger.Default.Send(new SendGuidService(SelectedPatient.Id), "PatientIdSent");
        }

        private void AddPatient(object obj)
        {
            Messenger.Default.Send(SelectedPatient, "AddRenderingProvider");
            ReturnNewPatient();
            patientRepository.Add(SelectedPatient);
            SendPatientId();
            RaisePropertyChanged("CheckAmount");
        }

        private void ReturnNewPatient()
        {
            if (SettingsService.ReuseSamePatientEnabled)
                GetNewPatientDependentOnUserPromptPreference();

            else if (SettingsService.ReuseSamePatientEnabled == false)
                SelectedPatient = new Patient();
            RaisePropertyChanged("Patient");
        }

        private void GetNewPatientDependentOnUserPromptPreference()
        {
            if (SettingsService.PatientPromptEnabled)
                PromptTypeOfNewPatient();

            else if (SettingsService.PatientPromptEnabled == false)
                CloneSelectedPatient();
        }

        private void PromptTypeOfNewPatient()
        {
            var dialogPrompt = new MessageBoxService(selectedPatient);

            if (dialogPrompt.ShowDialog())
                CloneSelectedPatient();

            else
                SelectedPatient = new Patient();
        }

        private void CloneSelectedPatient()
        {
            SelectedPatient = patientRepository.GetSelectedPatient(selectedPatient.Id).CopyPatient();
            RaisePropertyChanged("SelectedPatient");
        }

        private bool CanAddPatient(object obj)
        {
            bool b = false;
            if (isAddPatientEnabled)
            {
                b = !string.IsNullOrEmpty(SelectedPatient.FirstName) &&
                    !string.IsNullOrEmpty(selectedPatient.LastName);
            }

            return b;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveSettings()
        {
            Messenger.Default.Send(new SettingsSavedMessage(), "UpdateSettings");
            SettingsService.SetDefaultPatient(selectedPatient);
        }
    }
}