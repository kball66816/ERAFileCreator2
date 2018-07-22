using PatientManagement.DAL;

namespace EraFileCreator.Services
{
    public static class ChargeService
    {
        private static readonly ISettingsService SettingsService;

        static ChargeService()
        {
            SettingsService = new SettingsService();
        }

        public static ServiceDescription GetNewCharge()
        {
            return new ServiceDescription();
        }

        private static ServiceDescription Clone(ServiceDescription charge)
        {
            return new ServiceDescription(charge);
        }

        public static void AssociateAdditionalServiceDescription(ServiceDescription primary,
            ServiceDescription additional)
        {
            additional.BillId = primary.BillId;
            additional.DateOfService = primary.DateOfService;
            additional.PlaceOfService.ServiceLocation = primary.PlaceOfService.ServiceLocation;
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