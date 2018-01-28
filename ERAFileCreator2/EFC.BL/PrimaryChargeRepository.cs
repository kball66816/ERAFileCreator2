using System;
using System.Collections.Generic;
using System.Linq;
using PatientManagement.DAL;
using PatientManagement.Model;

namespace EFC.BL
{
    public class PrimaryChargeRepository : IPrimaryChargeRepository
    {
        private static readonly List<PrimaryCharge> Charges = new List<PrimaryCharge>();

        public void Add(PrimaryCharge charge)
        {
            var existing = GetSelectedCharge(charge.Id);
            if (existing == null) Charges.Add(charge);
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
            return Charges.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<PrimaryCharge> GetSelectedCharges(Guid patientId)
        {
            return Charges.Where(c => c.PatientId == patientId && c.ChargeCost > 0);
        }
    }
}