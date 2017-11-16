using PatientManagement.DAL;
using PatientManagement.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace EFC.BL
{
    public class PrimaryChargeRepository:IPrimaryChargeRepository
    {
        public PrimaryChargeRepository(Patient patient)
        {
            charges = patient.Charges;
        }

        private ObservableCollection<PrimaryCharge> charges;

        public void Add(PrimaryCharge charge)
        {
            charges.Add(charge);
        }

        public void Delete(PrimaryCharge charge)
        {
            charges.Remove(charge);
        }

        public PrimaryCharge UpdateCharge(PrimaryCharge charge)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<PrimaryCharge> GetAllCharges()
        {
            return charges;
        }

        public PrimaryCharge GetSelectedCharge(Guid id)
        {
            return charges.FirstOrDefault(c=>c.Id == id);
        }
    }
}
