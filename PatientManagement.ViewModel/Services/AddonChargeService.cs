using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using System;

namespace PatientManagement.ViewModel.Services
{
    public static class AddonChargeService
    {
        static AddonChargeService()
        {
            ChargeRepository = new AddonChargeRepository();
        }

        public static IAddonChargeRepository ChargeRepository { get; }

        public static AddonCharge GetNewAddonCharge()
        {
            return new AddonCharge();
        }

        public static AddonCharge Clone(this AddonCharge addonCharge)
        {
            return new AddonCharge(addonCharge);
        }

        public static void AssociateChargeId(this AddonCharge addon,Guid chargeId)
        {
            addon.PrimaryChargeId = chargeId;
        }
    }
}
