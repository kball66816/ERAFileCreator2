using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class PatientViewModel : INotifyPropertyChanged
    {
        public PatientViewModel()
        {
            LoadInitialPatient();
            patientRepository.Add(SelectedPatient);
            LoadCommands();
            Messenger.Default.Register<Patient>(this, OnPatientReceived);
            Messenger.Default.Register<PrimaryCharge>(this, OnPrimaryChargeReceived, "Patient");
            Messenger.Default.Register<Provider>(this, OnProviderReceived, "AddRenderingProvider");
            Messenger.Default.Register<SaveFileMessage>(this,OnSaveFileMessage,"SaveFile");
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
            IPrimaryChargeRepository crp = new PrimaryChargeRepository(SelectedPatient);
            crp.Add(charge);
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

        private Patient PromptTypeOfNewPatient()
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
            return SelectedPatient;
        }

        private void CloneSelectedPatient()
        {
            SelectedPatient = patientRepository.GetSelectedPatient(selectedPatient.Id).CopyPatient();
            RaisePropertyChanged("SelectedPatient");
        }

        private bool CanAddPatient(object obj)
        {
            return !string.IsNullOrEmpty(SelectedPatient.FirstName)
                && !string.IsNullOrEmpty(selectedPatient.LastName);
        }

        public Adjustment SelectedChargeAdjustmentIndex { get; set; }

        private ObservableCollection<Adjustment> adjustments;

        public ObservableCollection<Adjustment> Adjustments
        {
            get { return adjustments; }
            set
            {
                if (value == adjustments) return;
                adjustments = value;
                RaisePropertyChanged("Adjustments");
            }
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

        public ICommand AddChargeToPatientCommand { get; set; }


        private void SaveSettings()
        {
            Messenger.Default.Send(new SettingsSavedMessage(), "UpdateSettings");
            SettingsService.SetDefaultPatient(selectedPatient);
        }
    }
}
