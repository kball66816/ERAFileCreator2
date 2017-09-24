﻿using PatientManagement.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using PatientManagement.DAL;

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
            RaisePropertyChanged("patientList");
        }

        public ObservableCollection<Patient> GetAllPatients()
        {
            return patientList;
        }

        public Patient GetSelectedPatient(string billId)
        {
            return GetSelectedPatient(billId);
        }
    }
}
