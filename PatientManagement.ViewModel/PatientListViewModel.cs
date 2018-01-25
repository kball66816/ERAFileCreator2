using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Common.Common.Services;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
   public class PatientListViewModel:INotifyPropertyChanged
    {
        public PatientListViewModel()
        {
            Patients = patientRepository.GetAllPatients();

        }

        private IPatientRepository patientRepository = new PatientRepository();

        private ObservableCollection<Patient> patients;

        public ObservableCollection<Patient> Patients
        {
            get { return patients; }
            private set
            {
                if (value != patients)
                {
                    patients = value;
                    RaisePropertyChanged("Patients");
                }

            }
        }

        private Patient patient;

        public Patient Patient
        {
            get { return patient; }
            set
            {
                if (value == patient) return;
                patient = value;
                RaisePropertyChanged("Patient");
                Messenger.Default.Send<Patient>(Patient);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
