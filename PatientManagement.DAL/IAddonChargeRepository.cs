using System;
using System.Collections.ObjectModel;
using PatientManagement.Model;

namespace PatientManagement.DAL
{
    public interface IAddonChargeRepository
    {
        void Add(AddonCharge charge);

        void Delete(AddonCharge charge);

        AddonCharge UpdateCharge(AddonCharge charge);

        ObservableCollection<AddonCharge> GetAllCharges();

        AddonCharge GetSelectedCharge(Guid id);
    }
}
