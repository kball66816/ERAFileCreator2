using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    public static class AddonChargeService
    {
        static AddonChargeService()
        {
            ChargeRepository = new AddonChargeRepository();
        }

        private static IAddonChargeRepository ChargeRepository { get; }

        public static AddonCharge GetNewAddonCharge()
        {
            return new AddonCharge();
        }

        public static AddonCharge Clone(this AddonCharge addonCharge)
        {
            return new AddonCharge(addonCharge);
        }

        public static void Add(this AddonCharge addon)
        {
            ChargeRepository.Add(addon);
        }

        public static void AssociateChargeId(this AddonCharge addon,Guid chargeId)
        {
            addon.PrimaryChargeId = chargeId;
        }
    }
}
