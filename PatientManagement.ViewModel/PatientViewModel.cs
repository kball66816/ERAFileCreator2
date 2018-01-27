using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;

namespace PatientManagement.ViewModel
{
    public class PatientViewModel : INotifyPropertyChanged
    {
        public PatientViewModel()
        {
            LoadInitialPatient();
            Messenger.Default.Register<InitializationCompleteMessage>(this, OnInitializationComplete);
            Messenger.Default.Register<Provider>(this, OnProviderReceived, "AddRenderingProvider");
            Messenger.Default.Register<SaveFileMessage>(this, OnSaveFileMessage, "SaveTextFiletoSelectedDirectory");
            patientRepository = new PatientRepository();
            AddPatientCommand = new Command(AddPatient, CanAddPatient);
        }

        private readonly IPatientRepository patientRepository;

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

        private Patient selectedPatient;

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                if (value == selectedPatient) return;
                selectedPatient = value;
                RaisePropertyChanged("SelectedPatient");
                SendPatientCharges();
            }
        }

        public ICommand AddPatientCommand { get; private set; }

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
            {
                GetNewPatientDependentOnUserPromptPreference();
            }

            else if (SettingsService.ReuseSamePatientEnabled == false)
            {
                SelectedPatient = new Patient();
            }
            RaisePropertyChanged("Patient");
        }

        private void GetNewPatientDependentOnUserPromptPreference()
        {
            if (SettingsService.PatientPromptEnabled)
            {
                PromptTypeOfNewPatient();
            }

            else if (SettingsService.PatientPromptEnabled == false)
            {
                CloneSelectedPatient();
            }
        }

        private void PromptTypeOfNewPatient()
        {
            var dialogPrompt = new DialogService(selectedPatient);

            if (dialogPrompt.ShowDialog())
            {
                CloneSelectedPatient();
            }

            else
            {
                SelectedPatient = new Patient();
            }
        }

        private void CloneSelectedPatient()
        {
            SelectedPatient = patientRepository.GetSelectedPatient(selectedPatient.Id).CopyPatient();
            RaisePropertyChanged("SelectedPatient");
        }

        private bool CanAddPatient(object obj)
        {
            return
                !string.IsNullOrEmpty(SelectedPatient.FirstName) &&
             !string.IsNullOrEmpty(selectedPatient.LastName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
