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
            Messenger.Default.Register<Patient>(this, OnPatientReceived);
            Messenger.Default.Register<PrimaryCharge>(this, OnPrimaryChargeReceived, "Patient");
            Messenger.Default.Register<Provider>(this, OnProviderReceived, "AddRenderingProvider");
            Messenger.Default.Register<SaveFileMessage>(this,OnSaveFileMessage,"SaveTextFiletoSelectedDirectory");
        }

        private void OnInitializationComplete(InitializationCompleteMessage message)
        {

            patientRepository.Add(SelectedPatient);
            LoadCommands();
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


        private void OnPrimaryChargeReceived(PrimaryCharge charge)
        {
            //IPrimaryChargeRepository crp = new PrimaryChargeRepository(SelectedPatient);
            //crp.Add(charge);
            RaisePropertyChanged("SelectedPatient");
            SendPatientCharges();
        }


        private void OnPatientReceived(Patient patient)
        {
            SelectedPatient = patient;
            RaisePropertyChanged("SelectedPatient");
            Messenger.Default.Send(selectedPatient,"GiveSelectedPatientProvider");
            SendPatientCharges();
        }


        private void LoadInitialPatient()
        {
            SelectedPatient = new Patient();
            SelectedPatient = SettingsService.PullDefaultPatient();
            Messenger.Default.Send(new SendGuidService(SelectedPatient.Id),"PatientIdSent");
        }

        private IPatientRepository patientRepository = new PatientRepository();


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
            SendPatientCharges();
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
            bool canAdd = selectedPatient.Charges.Count > 0 &&
            !string.IsNullOrEmpty(SelectedPatient.FirstName) &&
            !string.IsNullOrEmpty(selectedPatient.LastName);

            return canAdd;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadCommands()
        {
            AddPatientCommand = new Command(AddPatient, CanAddPatient);
        }

        private void SaveSettings()
        {
            Messenger.Default.Send(new SettingsSavedMessage(), "UpdateSettings");
            SettingsService.SetDefaultPatient(selectedPatient);
        }
    }
}
