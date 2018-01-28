using System.Collections.ObjectModel;
using System.ComponentModel;
using Common.Common.Services;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;

namespace PatientManagement.ViewModel
{
    public class PatientListViewModel : INotifyPropertyChanged
    {
        private readonly IPatientRepository patientRepository = new PatientRepository();

        private Patient patient;

        private ObservableCollection<Patient> patients;

        public PatientListViewModel()
        {
            Patients = patientRepository.GetAllPatients();
        }

        public ObservableCollection<Patient> Patients
        {
            get => patients;
            private set
            {
                if (value != patients)
                {
                    patients = value;
                    RaisePropertyChanged("Patients");
                }
            }
        }

        public Patient Patient
        {
            get => patient;
            set
            {
                if (value == patient) return;
                patient = value;
                RaisePropertyChanged("Patient");
                Messenger.Default.Send(Patient);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}