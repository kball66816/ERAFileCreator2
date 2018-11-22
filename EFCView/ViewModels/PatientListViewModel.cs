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

        public PatientService PatientService { get; }
        public PatientListViewModel(ISettingsService settingsService)
        {
            this.PatientService = new PatientService(settingsService);
            this.Patients = this.PatientService.PatientRepository.GetAllPatients();
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
            return this.PatientService.PatientRepository.GetAllPatients() != null;
        }

        private void ClearPatientListCommand(object obj)
        {
            this.PatientService.PatientRepository.GetAllPatients().Clear();
            Messenger.Default.Send(new ListClearedMessage(), "Patient List Cleared");
            Messenger.Default.Send(new UpdateCalculations());
        }
    }
}