using System.Windows.Input;
using Common.Common.Services;
using EraFileCreator.Service.Messaging;
using EraFileCreator.Services;
using PatientManagement.DAL;

namespace EraFileCreator.ViewModels
{
    public class PatientViewModel : BaseViewModel
    {
        private Patient _selectedPatient;
        public PatientService PatientService { get; }
        public PatientViewModel(ISettingsService settingService)
        {
            this.PatientService = new PatientService(settingService);
            this.SelectedPatient = this.PatientService.LoadInitialPatient();
            this.PatientService.PatientRepository.Add(this.SelectedPatient);
            Messenger.Default.Register<ServiceDescription>(this, this.OnChargeReceived);
            Messenger.Default.Register<Provider>(this, this.OnProviderReceived, "BillingProvider");
            Messenger.Default.Register<SaveFileMessage>(this, this.OnSaveFileMessage,
                "SaveTextFiletoSelectedDirectory");
            Messenger.Default.Register<ListClearedMessage>(this, this.OnListClearedMessageReceived,
                "Patient List Cleared");
            this.AddPatientCommand = new Command(this.AddNewPatient, this.CanAddNewPatient);
        }

        public Patient SelectedPatient
        {
            get => this._selectedPatient;
            set
            {
                if (value == this._selectedPatient) return;
                this._selectedPatient = value;
                this.RaisePropertyChanged("SelectedPatient");
            }
        }

        public ICommand AddPatientCommand { get; }

        private void OnListClearedMessageReceived(ListClearedMessage obj)
        {
            this.SelectedPatient = PatientService.LoadInitialPatient();
            PatientService.PatientRepository.Add(this.SelectedPatient);
        }

        private void OnChargeReceived(ServiceDescription charge)
        {
            this.SelectedPatient.Charges.Add(charge);
        }

        private void OnSaveFileMessage(SaveFileMessage obj)
        {
            PatientService.SaveSettings(this.SelectedPatient);
        }

        private void OnProviderReceived(Provider provider)
        {
            this.SelectedPatient.RenderingProvider = provider.CopyProvider();
            this.RaisePropertyChanged("SelectedPatient");
        }

        private void AddNewPatient(object obj)
        {
            this.SelectedPatient = PatientService.GetNewPatientBasedOnSettings(this.SelectedPatient);
            PatientService.PatientRepository.Add(this.SelectedPatient);
            this.RaisePropertyChanged("CheckAmount");
        }

        private bool CanAddNewPatient(object obj)
        {
            var canAdd = false;
            if (obj is Patient patient)
            {
                canAdd = !string.IsNullOrEmpty(patient.FirstName) &&
                         !string.IsNullOrEmpty(patient.LastName)
                         && patient.Charges.Count > 0;
            }
            return canAdd;
        }
    }
}