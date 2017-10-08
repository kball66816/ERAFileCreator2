using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientManagement.Model;
using PatientManagement.DAL;

namespace EFC.BL
{
   public class AddonChargeRepository:IAddonChargeRepository
    {
        public AddonChargeRepository(PrimaryCharge charge)
        {
            addonCharges = charge.AddonChargeList;
        }

        private ObservableCollection<AddonCharge> addonCharges;

        public void Add(AddonCharge charge)
        {
            addonCharges.Add(charge);
        }

        public void Delete(AddonCharge charge)
        {
            addonCharges.Remove(charge);
        }

        public AddonCharge UpdateCharge(AddonCharge charge)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<AddonCharge> GetAllCharges()
        {
            return addonCharges;
        }

        public AddonCharge GetSelectedCharge(Guid id)
        {
            return addonCharges.FirstOrDefault(c=>c.Id == id);
        }

        public IAdjustmentRepository AdjustmentRepository { get; set; }
    }
}
