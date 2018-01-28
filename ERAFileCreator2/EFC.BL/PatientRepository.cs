using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using PatientManagement.DAL;
using PatientManagement.Model;

namespace EFC.BL
{
    public class PatientRepository : IPatientRepository, INotifyPropertyChanged
    {
        private static readonly ObservableCollection<Patient> patientList = new ObservableCollection<Patient>();

        public event PropertyChangedEventHandler PropertyChanged;


        public void Add(Patient patient)
        {
            patientList.Add(patient);
            RaisePropertyChanged("patientList");
        }

        public void Delete(Patient patient)
        {
            patientList.Remove(patient);
            RaisePropertyChanged("patientList");
        }

        public ObservableCollection<Patient> GetAllPatients()
        {
            return patientList;
        }

        public Patient GetSelectedPatient(Guid id)
        {
            return patientList.FirstOrDefault(p => p.Id == id);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}