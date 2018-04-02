using Common.Common.Services;
using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    public static class AddonChargeService
    {
        static AddonChargeService()
        {
            SettingsService = new SettingsService();
        }

        public static AddonCharge GetNewAddonCharge()
        {
            return new AddonCharge();
        }

        private static readonly SettingsService SettingsService;

        private static AddonCharge Clone(this AddonCharge addonCharge)
        {
            return new AddonCharge(addonCharge);
        }

        public static void SendAddonMessage(AddonCharge addonCharge)
        {
            Messenger.Default.Send(addonCharge);
        }

        public static AddonCharge GetNewAddonSettingsBased(AddonCharge addon)
        {
            if (SettingsService.ReuseSameAddonEnabled && SettingsService.PatientPromptEnabled)
            {
                var dialogPrompt = new MessageBoxService();

                addon = dialogPrompt.ShowDialog() ?
                    Clone(addon) : GetNewAddonCharge();
            }

            else if (SettingsService.ReuseSameAddonEnabled)
            {
                addon.Clone();
            }
            else
            {
                addon = GetNewAddonCharge();
            }

            return addon;
        }

        public static void AssociateAdjustmentToCharge(AddonCharge selectedAddonCharge, Adjustment adjustment)
        {
            selectedAddonCharge.Adjustments.Add(adjustment);
        }
    }
}
