﻿using System.Linq;
using EraView;
using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    public class SettingsService
    {

        public bool ReuseChargeForNextPatient
        {
            get
            {
                return Settings.Default.ReuseChargeForNextPatient;
            }
        }
        public bool PatientPromptEnabled
        {
            get
            {
                return Settings.Default.EnableReusePatientPrompt;
            }
        }

        public bool ReuseSamePatientEnabled
        {
            get
            {
                return Settings.Default.ReusePatient;
            }
        }

        public bool AddonPromptEnabled
        {
            get
            {
                return Settings.Default.EnableReuseAddonPrompt;
            }
        }

        public bool ReuseSameAddonEnabled
        {
            get
            {
                return Settings.Default.ReuseAddon;
            }
        }
        public void SetDefaultPreferences(Preference preference)
        {
            Settings.Default.EnableReusePatientPrompt = preference.EnablePatientReusePrompt;
            Settings.Default.EnableReuseAddonPrompt = preference.EnableAddonReusePrompt;
            Settings.Default.ReusePatient = preference.ReusePatient;
            Settings.Default.ReuseAddon = preference.ReuseAddon;
            Settings.Default.ReloadLastPatient = preference.ReloadLastPatientFromLastSession;
            Settings.Default.ReuseChargeForNextPatient = preference.ReuseLastChargeForNextPatient;
            Settings.Default.Save();
        }

        public Preference PullDefaultPreferences(Preference preference)
        {
            preference.EnableAddonReusePrompt = Settings.Default.EnableReuseAddonPrompt;
            preference.EnablePatientReusePrompt = Settings.Default.EnableReusePatientPrompt;
            preference.ReusePatient = Settings.Default.ReusePatient;
            preference.ReuseAddon = Settings.Default.ReuseAddon;
            preference.ReloadLastPatientFromLastSession = Settings.Default.ReloadLastPatient;
            preference.ReuseLastChargeForNextPatient = Settings.Default.ReuseChargeForNextPatient;

            return preference;
        }
        public InsuranceCompany PullDefaultInsurance(InsuranceCompany insurance)
        {

            if(!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyName))
            {
                insurance.Name = Settings.Default.InsuranceCompanyName;
            }
            if (!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyTaxId))
            {
                insurance.TaxId = Settings.Default.InsuranceCompanyTaxId;
            }

            if(!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyAddressLineOne))
            {
                insurance.Address.StreetOne = Settings.Default.InsuranceCompanyAddressLineOne;
            }

            if(!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyAddressLineTwo))
            {
                insurance.Address.StreetTwo = Settings.Default.InsuranceCompanyAddressLineTwo;
            }

            if(!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyAddressCity))
            {
                insurance.Address.City = Settings.Default.InsuranceCompanyAddressCity;
            }

            if(!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyAddressState))
            {
                insurance.Address.State = Settings.Default.InsuranceCompanyAddressState;
            }

            if(!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyAddressZipCode.ToString()))
            {
                insurance.Address.ZipCode = Settings.Default.InsuranceCompanyAddressZipCode;
            }

            return insurance;
        }

        public void SetDefaultInsurance(InsuranceCompany insurance)
        {
            Settings.Default.InsuranceCompanyName = insurance.Name;
            Settings.Default.InsuranceCompanyTaxId = insurance.TaxId;
            Settings.Default.InsuranceCompanyAddressLineOne = insurance.Address.StreetOne;
            Settings.Default.InsuranceCompanyAddressLineTwo = insurance.Address.StreetTwo;
            Settings.Default.InsuranceCompanyAddressCity = insurance.Address.City;
            Settings.Default.InsuranceCompanyAddressState = insurance.Address.State;
            Settings.Default.InsuranceCompanyAddressZipCode = insurance.Address.ZipCode;
            Settings.Default.Save();

        }

        public Patient PullDefaultPatient()
        {
            var patient = new Patient();
            if (Settings.Default.ReloadLastPatient)
            {
                return LoadPatientFromSettings(patient);
            }
            else
            {
                return patient;
            }
        }

        private Patient LoadPatientFromSettings(Patient patient)
        {
            if (!string.IsNullOrEmpty(Settings.Default.PatientFirstName))
            {
                patient.FirstName = Settings.Default.PatientFirstName;
            }

            if (!string.IsNullOrEmpty(Settings.Default.PatientLastName))
            {
                patient.LastName = Settings.Default.PatientLastName;
            }

            if (!string.IsNullOrEmpty(Settings.Default.PatientCopay))
            {
                decimal.TryParse(Settings.Default.PatientCopay, out decimal value);
                
               // patient.Charges.FirstOrDefault().Copay = value;
            }

            LoadDefaultRenderingProvider(patient);
            return patient;
        }

        private Provider LoadDefaultRenderingProvider(Patient patient)
        {
            var provider = new Provider();
            if(!string.IsNullOrEmpty(Settings.Default.RenderingProviderFirstName))
            {
               provider.FirstName = Settings.Default.RenderingProviderFirstName;
            }

            if(!string.IsNullOrEmpty(Settings.Default.RenderingProviderLastName))
            {
                provider.LastName = Settings.Default.RenderingProviderLastName;
            }

            if(!string.IsNullOrEmpty(Settings.Default.RenderingProviderNpi))
            {
                provider.Npi = Settings.Default.RenderingProviderNpi;
            }

            return provider;
        }

        public void SetDefaultPatient(Patient patient)
        {
            Settings.Default.PatientFirstName = patient.FirstName;
            Settings.Default.PatientLastName = patient.LastName;
            Settings.Default.PatientCopay = patient.Charges.FirstOrDefault().Copay.ToString();
            Settings.Default.RenderingProviderFirstName = patient.RenderingProvider.FirstName;
            Settings.Default.RenderingProviderLastName = patient.RenderingProvider.LastName;
            Settings.Default.RenderingProviderNpi = patient.RenderingProvider.Npi;
            Settings.Default.Save();
        }

       
        public Provider PullDefaultBillingProvider(Provider billingProvider)
        {
            billingProvider.IsIndividual = Settings.Default.BillingProviderIsIndividual;

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderFirstName))
            {
                billingProvider.FirstName = Settings.Default.BillingProviderFirstName;
            }

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderLastName))
            {
                billingProvider.LastName = Settings.Default.BillingProviderLastName;
            }

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderNpi))
            {
                billingProvider.Npi = Settings.Default.BillingProviderNpi;
            }

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderName ))
            {
                billingProvider.FullName = Settings.Default.BillingProviderName;
            }

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderAddressLineOne))
            {
                billingProvider.Address.StreetOne = Settings.Default.BillingProviderAddressLineOne;
            }

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderAddressLineTwo))
            {
                billingProvider.Address.StreetTwo = Settings.Default.BillingProviderAddressLineTwo;
            }

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderAddressCity))
            {
                billingProvider.Address.City = Settings.Default.BillingProviderAddressCity;
            }

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderAddressState))
            {
                billingProvider.Address.State = Settings.Default.BillingProviderAddressState;
            }

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderAddressZipCode))
            {
                billingProvider.Address.ZipCode = Settings.Default.BillingProviderAddressZipCode;
            }

            return billingProvider;
        }

        public void SetDefaultBillingProvider(Provider billingProvider)
        {
            Settings.Default.BillingProviderFirstName = billingProvider.FirstName;
            Settings.Default.BillingProviderLastName = billingProvider.LastName;
            Settings.Default.BillingProviderNpi = billingProvider.Npi;
            Settings.Default.BillingProviderName = billingProvider.FullName;
            Settings.Default.BillingProviderAddressLineOne = billingProvider.Address.StreetOne;
            Settings.Default.BillingProviderAddressLineTwo = billingProvider.Address.StreetTwo;
            Settings.Default.BillingProviderAddressCity = billingProvider.Address.City;
            Settings.Default.BillingProviderAddressState = billingProvider.Address.State;
            Settings.Default.BillingProviderAddressZipCode = billingProvider.Address.ZipCode;
            Settings.Default.BillingProviderIsIndividual = billingProvider.IsIndividual;
            Settings.Default.Save();
        }

    }
}