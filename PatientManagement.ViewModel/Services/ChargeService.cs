using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    public static class ChargeService
    {
        static ChargeService()
        {
            SettingsService = new SettingsService();
        }

        private static readonly ISettingsService SettingsService;

        public static PrimaryCharge GetNewCharge()
        {
            return new PrimaryCharge();
        }

        private static PrimaryCharge Clone(PrimaryCharge charge)
        {
            return new PrimaryCharge(charge);
        }

        public static void AssociateAddonWithCharge(PrimaryCharge charge, AddonCharge addon)
        {
            charge.AddonCharges.Add(addon);
        }
        public static void AssociateAdjustmentWithCharge(PrimaryCharge charge, Adjustment adjustment)
        {
            charge.Adjustments.Add(adjustment);
        }
        public static PrimaryCharge SetNewOrClonedChargeByUserSettings(PrimaryCharge charge)
        {
            charge = SettingsService.ReuseCharge ? Clone(charge) : GetNewCharge();

            return charge;
        }
    }
}
