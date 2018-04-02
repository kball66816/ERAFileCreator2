﻿using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    public static class ProviderService
    {
        static ProviderService()
        {
            SettingsService = new SettingsService();
            BillingProviderRepository = new BillingProviderRepository();
        }

        public static readonly ISettingsService SettingsService;
        public static readonly IProvider BillingProviderRepository;

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
