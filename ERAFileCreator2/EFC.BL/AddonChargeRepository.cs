using System;
using System.Collections.Generic;
using System.Linq;
using PatientManagement.Model;
using PatientManagement.DAL;

namespace EFC.BL
{
   public class AddonChargeRepository:IAddonChargeRepository
    {
        private static readonly List<AddonCharge> AddonCharges = new List<AddonCharge>();

        public void Add(AddonCharge charge)
        {
            AddonCharges.Add(charge);
        }

        public void Delete(AddonCharge charge)
        {
            AddonCharges.Remove(charge);
        }

        public AddonCharge UpdateCharge(AddonCharge charge)
        {
            throw new NotImplementedException();
        }

        public List<AddonCharge> GetAllCharges()
        {
            return AddonCharges;
        }

        public AddonCharge GetSelectedCharge(Guid id)
        {
            return AddonCharges.FirstOrDefault(c=>c.Id == id);
        }
    }
}