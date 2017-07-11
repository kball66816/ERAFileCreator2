using System.Collections.ObjectModel;
using System.Linq;
namespace EFC.BL
{
    public interface IPatientRepository
    {
        void Add(Patient patient);

        void Delete(Patient patient);

        ObservableCollection<Patient> GetAllPatients();

        Patient GetSelectedPatient(string billId);

   
    }
}
