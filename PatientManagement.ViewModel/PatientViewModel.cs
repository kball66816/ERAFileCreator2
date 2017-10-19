using Common.Common;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class PatientViewModel : INotifyPropertyChanged
    {
        public PatientViewModel()
        {

            Settings = new SettingsService();
            LoadInitialPatient();
            patientRepository.Add(SelectedPatient);
            LoadCommands();
            Messenger.Default.Register<Patient>(this, OnPatientReceived);
            Messenger.Default.Register<PrimaryCharge>(this, OnPrimaryChargeReceived, "Patient");
            Messenger.Default.Register<Provider>(this, OnProviderReceived, "RenderingProvider");
        }

        private void OnProviderReceived(Provider provider)
        {
            selectedPatient.RenderingProvider = provider;
            RaisePropertyChanged("SelectedPatient");
        }

        private void OnPrimaryChargeReceived(PrimaryCharge charge)
        {
            IPrimaryChargeRepository crp = new PrimaryChargeRepository(SelectedPatient);
            crp.Add(charge);
            RaisePropertyChanged("SelectedPatient");
            Messenger.Default.Send(selectedPatient.Charges, "UpdateChargesList");
        }


        private void OnPatientReceived(Patient patient)
        {
            SelectedPatient = patient;
            RaisePropertyChanged("SelectedPatient");
            Messenger.Default.Send(selectedPatient.Charges, "UpdateChargesList");
        }


        private void LoadInitialPatient()
        {
            SelectedPatient = new Patient();
            SelectedPatient = Settings.PullDefaultPatient();
        }

        private IPatientRepository patientRepository = new PatientRepository();

        private SettingsService Settings { get; set; }

        private Patient selectedPatient;

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                if (value == selectedPatient) return;
                selectedPatient = value;
                RaisePropertyChanged("SelectedPatient");
            }
        }

        public ICommand AddPatientCommand { get; private set; }

        private void AddPatient(object obj)
        {
            Messenger.Default.Send(SelectedPatient, "AddRenderingProvider");
            ReturnNewPatient();
            patientRepository.Add(SelectedPatient);
            Messenger.Default.Send(selectedPatient.Charges, "UpdateChargesList");
            RaisePropertyChanged("CheckAmount");
        }

        private void ReturnNewPatient()
        {
            if (Settings.ReuseSamePatientEnabled)
            {
                GetNewPatientDependentOnUserPromptPreference();
            }

            else if (Settings.ReuseSamePatientEnabled == false)
            {
                SelectedPatient = new Patient();

            }
            RaisePropertyChanged("Patient");
        }

        private void GetNewPatientDependentOnUserPromptPreference()
        {
            if (Settings.PatientPromptEnabled)
            {
                PromptTypeOfNewPatient();
            }

            else if (Settings.PatientPromptEnabled == false)
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

            return selectedPatient;
        }


        private void CloneSelectedPatient()
        {

            selectedPatient = patientRepository.GetSelectedPatient(selectedPatient.Id).CopyPatient();
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


        public ICommand SaveFileCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadCommands()
        {
            AddPatientCommand = new Command(AddPatient, CanAddPatient);
            SaveFileCommand = new Command(Save, CanSave);
        }

        public ICommand AddChargeToPatientCommand { get; set; }

        //public ICommand UpdateRenderingProviderCommand { get; private set; }

        //private void UpdateRenderingProvider(object obj)
        //{
        //    Messenger.Default.Send(selectedPatient.RenderingProvider);
        //}

        private static bool CanSave(object obj)
        {
            return true;
        }

        private void SaveSettings()
        {
            Messenger.Default.Send<SettingsSavedMessage>(new SettingsSavedMessage(),"UpdateSettings");
            Settings.SetDefaultPatient(selectedPatient);
        }

        private void Save(object obj)
        {

            Messenger.Default.Send(new UpdateRepositoriesMessage());
            SaveSettings();
            var edi = new UpdatedEdi();
            var save = new SaveToFile();
            save.SaveFile(edi.Create835File());
        }

        public decimal PatientCount { get; private set; }

    }
}
