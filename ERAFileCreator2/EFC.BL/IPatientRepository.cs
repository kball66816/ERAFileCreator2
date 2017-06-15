using System.Collections.ObjectModel;

namespace EFC.BL
{
    public interface IPatientRepository
    {
        void Add(Patient patient);

        void Delete(Patient patient);

        ObservableCollection<Patient> GetAllPatients();
   
    }
}
