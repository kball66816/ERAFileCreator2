﻿using Common.Common;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel.Mocks
{
    public class SettingsServiceMock : ISettingsService
    {
        public SettingsServiceMock()
        {
            this.LoadMockBillingProvider();
            this.LoadMockPreference();
            this.LoadMockInsurance();
            this.LoadMockPatient();
            this.LoadMockRenderingProvider();
        }

        private Patient _mockPatient;

        private InsuranceCompany _mockInsurance;

        private Preference _mockPreference;

        private Provider _mockRenderingProvider;

        private Provider _mockBillingProvider;

        private void LoadMockBillingProvider()
        {
            this._mockBillingProvider = new Provider()
            {
                BusinessName = "Provider Business",
                Npi = "9876543213",
                Address = new Address()
                {
                    StreetOne = "123 Fake Street",
                    StreetTwo = "Ste 230",
                    City = "Seattle",
                    State = "WA",
                }
            };
        }

        private void LoadMockRenderingProvider()
        {
            this._mockRenderingProvider = new Provider()
            {
                FirstName = "Provider",
                LastName = "One",
                Npi = "1234567893"
            };
        }
        private void LoadMockPreference()
        {
            this._mockPreference = new Preference()
            {
                ReloadLastPatientFromLastSession = true,
                ReuseAddon = true,
                ReusePatient = true,
                ReuseLastChargeForNextPatient = true,
                EnableAddonReusePrompt = false,
                EnablePatientReusePrompt = false,
            };
        }
        private void LoadMockPatient()
        {
            this._mockPatient = new Patient()
            {
                FirstName = "John",
                LastName = "Smith"
            };
        }
        private void LoadMockInsurance()
        {
            this._mockInsurance = new InsuranceCompany()
            {
                Name = "Aetna",
                TaxId = "123456789",
                Address = new Address()
                {
                    StreetOne = "123 Fake Street",
                    StreetTwo = "Ste 230",
                    City = "Seattle",
                    State = "WA",
                }

            };
        }

        public bool ReuseCharge { get; set; }
        public bool PatientPromptEnabled { get; set; }
        public bool ReuseSamePatientEnabled { get; set; }
        public bool AddonPromptEnabled { get; set; }
        public bool ReuseSameAddonEnabled { get; set; }
        public void SetDefaultPreferences(Preference preference)
        {
            preference.EnableAddonReusePrompt = false;
            preference.EnablePatientReusePrompt = false;
            preference.ReusePatient = true;
            preference.ReuseAddon = true;
            preference.ReloadLastPatientFromLastSession = true;
            preference.ReuseLastChargeForNextPatient = true;

            ReuseCharge = preference.ReloadLastPatientFromLastSession;
            PatientPromptEnabled = preference.EnablePatientReusePrompt;
            ReuseSameAddonEnabled = preference.ReusePatient;
            AddonPromptEnabled = preference.EnableAddonReusePrompt;
            ReuseSameAddonEnabled = preference.ReuseAddon;
        }

        public Preference PullDefaultPreferences(Preference preference)
        {
            preference.EnableAddonReusePrompt = this._mockPreference.EnableAddonReusePrompt;
            preference.EnablePatientReusePrompt = this._mockPreference.EnablePatientReusePrompt;
            preference.ReusePatient = this._mockPreference.ReusePatient;
            preference.ReuseAddon = this._mockPreference.ReuseAddon;
            preference.ReloadLastPatientFromLastSession = this._mockPreference.ReloadLastPatientFromLastSession;
            preference.ReuseLastChargeForNextPatient = this._mockPreference.ReuseLastChargeForNextPatient;

            return preference;
        }

        public InsuranceCompany PullDefaultInsurance(InsuranceCompany insurance)
        {
            if (!string.IsNullOrEmpty(this._mockInsurance.Name))
                insurance.Name = this._mockInsurance.Name;

            if (!string.IsNullOrEmpty(this._mockInsurance.TaxId))
                insurance.TaxId = this._mockInsurance.TaxId;

            if (!string.IsNullOrEmpty(this._mockInsurance.Address.StreetOne))
                insurance.Address.StreetOne = this._mockInsurance.Address.StreetOne;

            if (!string.IsNullOrEmpty(this._mockInsurance.Address.StreetTwo))
                insurance.Address.StreetTwo = this._mockInsurance.Address.StreetTwo;

            if (!string.IsNullOrEmpty(this._mockInsurance.Address.City))
                insurance.Address.City = this._mockInsurance.Address.City;

            if (!string.IsNullOrEmpty(this._mockInsurance.Address.State))
                insurance.Address.State = this._mockInsurance.Address.State;

            if (!string.IsNullOrEmpty(this._mockInsurance.Address.ZipCode))
                insurance.Address.ZipCode = this._mockInsurance.Address.ZipCode;

            return insurance;
        }

        public void SetDefaultInsurance(InsuranceCompany insurance)
        {
            this._mockInsurance.Name = insurance.Name;
            this._mockInsurance.TaxId = insurance.TaxId;
            this._mockInsurance.Address.StreetOne = insurance.Address.StreetOne;
            this._mockInsurance.Address.StreetTwo = insurance.Address.StreetTwo;
            this._mockInsurance.Address.City = insurance.Address.City;
            this._mockInsurance.Address.State = insurance.Address.State;
            this._mockInsurance.Address.ZipCode = insurance.Address.ZipCode;
        }

        public Patient PullDefaultPatient(Patient patient)
        {
            if (this._mockPreference.ReloadLastPatientFromLastSession)
            {
                if (!string.IsNullOrEmpty(this._mockPatient.FirstName))
                    patient.FirstName = this._mockPatient.FirstName;

                if (!string.IsNullOrEmpty(this._mockPatient.LastName))
                    patient.LastName = this._mockPatient.LastName;
            }

            return patient;
        }

        public Provider PullDefaultRenderingProvider(Provider renderingProvider)
        {
            if (!string.IsNullOrEmpty(this._mockRenderingProvider.FirstName))
                renderingProvider.FirstName = this._mockRenderingProvider.FirstName;

            if (!string.IsNullOrEmpty(this._mockRenderingProvider.LastName))
                renderingProvider.LastName = this._mockRenderingProvider.LastName;

            if (!string.IsNullOrEmpty(this._mockRenderingProvider.Npi))
                renderingProvider.Npi = this._mockRenderingProvider.Npi;

            return renderingProvider;
        }

        public void SetDefaultRenderingProvider(Provider renderingProvider)
        {
            this._mockRenderingProvider.FirstName = renderingProvider.FirstName;
            this._mockRenderingProvider.LastName = renderingProvider.LastName;
            this._mockRenderingProvider.Npi = renderingProvider.Npi;
        }

        public void SetDefaultPatient(Patient patient)
        {
            this._mockPatient.FirstName = patient.FirstName;
            this._mockPatient.LastName = patient.LastName;
            this._mockRenderingProvider.FirstName = patient.RenderingProvider.FirstName;
            this._mockRenderingProvider.LastName = patient.RenderingProvider.LastName;
            this._mockRenderingProvider.Npi = patient.RenderingProvider.Npi;
        }

        public Provider PullDefaultBillingProvider(Provider billingProvider)
        {
            billingProvider.IsIndividual = Settings.Default.BillingProviderIsIndividual;

            if (!string.IsNullOrEmpty(this._mockBillingProvider.FirstName))
                billingProvider.FirstName = this._mockBillingProvider.FirstName;

            if (!string.IsNullOrEmpty(this._mockBillingProvider.LastName))
                billingProvider.LastName = this._mockBillingProvider.LastName;

            if (!string.IsNullOrEmpty(this._mockBillingProvider.Npi))
                billingProvider.Npi = this._mockBillingProvider.Npi;

            if (!string.IsNullOrEmpty(this._mockBillingProvider.BusinessName))
                billingProvider.FullName = this._mockBillingProvider.BusinessName;

            if (!string.IsNullOrEmpty(this._mockBillingProvider.Address.StreetOne))
                billingProvider.Address.StreetOne = this._mockBillingProvider.Address.StreetOne;

            if (!string.IsNullOrEmpty(this._mockBillingProvider.Address.StreetTwo))
                billingProvider.Address.StreetTwo = this._mockBillingProvider.Address.StreetTwo;

            if (!string.IsNullOrEmpty(this._mockBillingProvider.Address.City))
                billingProvider.Address.City = this._mockBillingProvider.Address.City;

            if (!string.IsNullOrEmpty(this._mockBillingProvider.Address.State))
                billingProvider.Address.State = this._mockBillingProvider.Address.State;

            if (!string.IsNullOrEmpty(this._mockBillingProvider.Address.ZipCode))
                billingProvider.Address.ZipCode = this._mockBillingProvider.Address.ZipCode;
            return billingProvider;
        }

        public void SetDefaultBillingProvider(Provider billingProvider)
        {
            this._mockBillingProvider.FirstName = billingProvider.FirstName;
            this._mockBillingProvider.LastName = billingProvider.LastName;
            this._mockBillingProvider.Npi = billingProvider.Npi;
            this._mockBillingProvider.BusinessName = billingProvider.FullName;
            this._mockBillingProvider.Address.StreetOne = billingProvider.Address.StreetOne;
            this._mockBillingProvider.Address.StreetTwo = billingProvider.Address.StreetTwo;
            this._mockBillingProvider.Address.City = billingProvider.Address.City;
            this._mockBillingProvider.Address.State = billingProvider.Address.State;
            this._mockBillingProvider.Address.ZipCode = billingProvider.Address.ZipCode;
            this._mockBillingProvider.IsIndividual = billingProvider.IsIndividual;
        }
    }
}