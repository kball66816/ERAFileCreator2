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

        public static ServiceDescription GetNewCharge()
        {
            return new ServiceDescription();
        }

        private static ServiceDescription Clone(ServiceDescription charge)
        {
            return new ServiceDescription(charge);
        }

        public static void AssociateAdditionalServiceDescription(ServiceDescription primary, ServiceDescription additional)
        {
            additional.BillId = primary.BillId;
            primary.AdditionalServiceDescriptions.Add(additional);
        }
        public static void AssociateAdjustmentWithCharge(ServiceDescription charge, Adjustment adjustment)
        {
            charge.Adjustments.Add(adjustment);
        }
        public static ServiceDescription SetNewOrClonedChargeByUserSettings(ServiceDescription charge)
        {
            charge = SettingsService.ReuseCharge ? Clone(charge) : GetNewCharge();

            return charge;
        }
    }
}
