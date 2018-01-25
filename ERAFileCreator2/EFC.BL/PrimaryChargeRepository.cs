using PatientManagement.DAL;
using PatientManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFC.BL
{
    public class PrimaryChargeRepository:IPrimaryChargeRepository
    {

        private static readonly List<PrimaryCharge> Charges = new List<PrimaryCharge>();

        public void Add(PrimaryCharge charge)
        {
            Charges.Add(charge);
        }

        public void Delete(PrimaryCharge charge)
        {
            Charges.Remove(charge);
        }

        public PrimaryCharge UpdateCharge(PrimaryCharge charge)
        {
            throw new NotImplementedException();
        }

        public List<PrimaryCharge> GetAllCharges()
        {
            return Charges;
        }

        public PrimaryCharge GetSelectedCharge(Guid id)
        {
            return Charges.FirstOrDefault(c=>c.Id == id);
        }
    }
}
