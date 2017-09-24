using System;
using System.Collections.ObjectModel;
using PatientManagement.Model;

namespace PatientManagement.DAL
{
    public interface IPatientRepository
    {
        void Add(Patient patient);

        void Delete(Patient patient);

        ObservableCollection<Patient> GetAllPatients();

        Patient GetSelectedPatient(Guid id);

    }
}
