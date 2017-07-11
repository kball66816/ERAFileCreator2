using EFC.BL;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System;

namespace WPFERA.Services
{
    public class PatientRepository : IPatientRepository, INotifyPropertyChanged
    {
        ObservableCollection<Patient> patientList = new ObservableCollection<Patient>();

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Add(Patient patient)
        {
            patientList.Add(patient);
            RaisePropertyChanged("patientList");
        }

        public void Delete(Patient patient)
        {
            patientList.Remove(patient);
        }

        public ObservableCollection<Patient> GetAllPatients()
        {
            return patientList;
        }

        public Patient GetSelectedPatient(string billId)
        {
            return patientList.First(p => p.BillId == billId);
        }
    }
}
