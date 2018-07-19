using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Common.Common.Services;
using EraFileCreator.Service.Messaging;
using EraFileCreator.Services;
using PatientManagement.DAL;

namespace EraFileCreator.ViewModels
{
    public class PatientListViewModel : INotifyPropertyChanged
    {

        private Patient _patient;

        private ObservableCollection<Patient> _patients;

        public PatientListViewModel()
        {
            this.Patients = PatientService.PatientRepository.GetAllPatients();
            this.ClearPatientList = new Command(this.ClearPatientListCommand, this.CanClearPatientList);
        }

        private bool CanClearPatientList(object obj)
        {
            return PatientService.PatientRepository.GetAllPatients() != null;
        }

        private void ClearPatientListCommand(object obj)
        {
            var dialog = new MessageBoxService();
            dialog.ClearMessage("Patient List");
            if (dialog.NewDialogResult == MessageBoxResult.Yes)
            {
                PatientService.PatientRepository.GetAllPatients().Clear();
                Messenger.Default.Send(new ListClearedMessage(), "Patient List Cleared");
                Messenger.Default.Send(new UpdateCalculations());
            }
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


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}