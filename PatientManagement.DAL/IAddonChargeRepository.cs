using System;
using System.Collections.Generic;
using PatientManagement.Model;

namespace PatientManagement.DAL
{
    public interface IAddonChargeRepository
    {
        void Add(AddonCharge charge);

        void Delete(AddonCharge charge);

        AddonCharge UpdateCharge(AddonCharge charge);

        List<AddonCharge> GetAllCharges();

        AddonCharge GetSelectedCharge(Guid id);
    }
}
