using EFC.BL;
using PatientManagement.DAL;

namespace EraFileCreator.Services
{
    public static class ProviderService
    {
        public static readonly ISettingsService SettingsService;
        public static readonly IProvider BillingProviderRepository;

        static ProviderService()
        {
            SettingsService = new SettingsService();
            BillingProviderRepository = new BillingProviderRepository();
        }

        public static Provider CopyProvider(this Provider provider)
        {
            return new Provider(provider);
        }

        public static Provider GetNewProvider()
        {
            return new Provider();
        }

        public static Provider LoadBillingProvider()
        {
            return SettingsService.PullDefaultBillingProvider(GetNewProvider());
        }

        public static void SaveBillingProvider(Provider provider)
        {
            SettingsService.SetDefaultBillingProvider(provider);
            BillingProviderRepository.AddBillingProvider(provider);
        }
    }
}