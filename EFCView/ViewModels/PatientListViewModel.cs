using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Common.Common.Services;
using EraFileCreator.Service.Messaging;
using EraFileCreator.Services;
using PatientManagement.DAL;

namespace EraFileCreator.ViewModels
{
    public class PatientListViewModel : BaseViewModel
    {
        private Patient _patient;

        private ObservableCollection<Patient> _patients;

        private readonly PatientService _patientService;
        public PatientListViewModel()
        {
            _patientService = new PatientService(new SettingsService());
            this.Patients = _patientService.PatientRepository.GetAllPatients();
            this.ClearPatientList = new Command(this.ClearPatientListCommand, this.CanClearPatientList);
        }


        public ICommand ClearPatientList { get; set; }

        public ObservableCollection<Patient> Patients
        {
            get => this._patients;
            private set
            {
                if (value != this._patients)
                {
                    this._patients = value;
                    this.RaisePropertyChanged("Patients");
                }
            }
        }

        public Patient Patient
        {
            get => this._patient;
            set
            {
                if (value == this._patient) return;
                this._patient = value;
                this.RaisePropertyChanged("Patient");
                Messenger.Default.Send(this.Patient);
            }
        }

        private bool CanClearPatientList(object obj)
        {
            return _patientService.PatientRepository.GetAllPatients() != null;
        }

        private void ClearPatientListCommand(object obj)
        {
            var dialog = new MessageBoxService();
            dialog.ClearMessage("Patient List");
            if (dialog.NewDialogResult == MessageBoxResult.Yes)
            {
                _patientService.PatientRepository.GetAllPatients().Clear();
                Messenger.Default.Send(new ListClearedMessage(), "Patient List Cleared");
                Messenger.Default.Send(new UpdateCalculations());
            }
        }
    }
}