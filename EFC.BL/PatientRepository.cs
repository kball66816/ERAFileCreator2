using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using PatientManagement.DAL;

namespace EFC.BL
{
    public class PatientRepository : IPatientRepository, INotifyPropertyChanged
    {
        private static readonly ObservableCollection<Patient> PatientList = new ObservableCollection<Patient>();

        public event PropertyChangedEventHandler PropertyChanged;


        public void Add(Patient patient)
        {
            PatientList.Add(patient);
            RaisePropertyChanged("patientList");
        }

        public void Delete(Patient patient)
        {
            PatientList.Remove(patient);
            RaisePropertyChanged("patientList");
        }

        public ObservableCollection<Patient> GetAllPatients()
        {
            return PatientList;
        }

        public Patient GetSelectedPatient(Guid id)
        {
            return PatientList.FirstOrDefault(p => p.Id == id);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}