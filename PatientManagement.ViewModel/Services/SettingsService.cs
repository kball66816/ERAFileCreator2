using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    public class SettingsService : ISettingsService
    {
        public SettingsService()
        {
            this.PatientPromptEnabled = Settings.Default.EnableReusePatientPrompt;

            this.ReuseSamePatientEnabled = Settings.Default.ReusePatient;

            this.ReuseCharge = Settings.Default.ReuseCharge;
        }
        public bool PatientPromptEnabled { get; set; }
        public bool ReuseSamePatientEnabled { get; set; }

        public bool ReuseCharge { get; set; }

        public void SetDefaultPreferences(Preference preference)
        {
            Settings.Default.EnableReusePatientPrompt = preference.EnablePatientReusePrompt;
            Settings.Default.EnableReuseAddonPrompt = preference.EnableAddonReusePrompt;
            Settings.Default.ReusePatient = preference.ReusePatient;
            Settings.Default.ReuseAddon = preference.ReuseAddon;
            Settings.Default.ReloadLastPatient = preference.ReloadLastPatientFromLastSession;
            Settings.Default.ReuseCharge = preference.ReuseLastChargeForNextPatient;
            Settings.Default.Save();
        }


        public Preference PullDefaultPreferences(Preference preference)
        {
            preference.EnableAddonReusePrompt = Settings.Default.EnableReuseAddonPrompt;
            preference.EnablePatientReusePrompt = Settings.Default.EnableReusePatientPrompt;
            preference.ReusePatient = Settings.Default.ReusePatient;
            preference.ReuseAddon = Settings.Default.ReuseAddon;
            preference.ReloadLastPatientFromLastSession = Settings.Default.ReloadLastPatient;
            preference.ReuseLastChargeForNextPatient = Settings.Default.ReuseCharge;

            return preference;
        }

        public InsuranceCompany PullDefaultInsurance(InsuranceCompany insurance)
        {
            if (!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyName))
                insurance.Name = Settings.Default.InsuranceCompanyName;
            if (!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyTaxId))
                insurance.TaxId = Settings.Default.InsuranceCompanyTaxId;

            if (!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyAddressLineOne))
                insurance.Address.StreetOne = Settings.Default.InsuranceCompanyAddressLineOne;

            if (!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyAddressLineTwo))
                insurance.Address.StreetTwo = Settings.Default.InsuranceCompanyAddressLineTwo;

            if (!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyAddressCity))
                insurance.Address.City = Settings.Default.InsuranceCompanyAddressCity;

            if (!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyAddressState))
                insurance.Address.State = Settings.Default.InsuranceCompanyAddressState;

            if (!string.IsNullOrEmpty(Settings.Default.InsuranceCompanyAddressZipCode))
                insurance.Address.ZipCode = Settings.Default.InsuranceCompanyAddressZipCode;

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

        public Patient PullDefaultPatient(Patient patient)
        {
            if (Settings.Default.ReloadLastPatient)
            {
                patient = LoadPatientFromSettings(patient);
            }

            return patient;
        }

        private static Patient LoadPatientFromSettings(Patient patient)
        {
            if (!string.IsNullOrEmpty(Settings.Default.PatientFirstName))
                patient.FirstName = Settings.Default.PatientFirstName;

            if (!string.IsNullOrEmpty(Settings.Default.PatientLastName))
                patient.LastName = Settings.Default.PatientLastName;
            return patient;
        }

        public Provider PullDefaultRenderingProvider(Provider renderingProvider)
        {
            if (!string.IsNullOrEmpty(Settings.Default.RenderingProviderFirstName))
                renderingProvider.FirstName = Settings.Default.RenderingProviderFirstName;

            if (!string.IsNullOrEmpty(Settings.Default.RenderingProviderLastName))
                renderingProvider.LastName = Settings.Default.RenderingProviderLastName;

            if (!string.IsNullOrEmpty(Settings.Default.RenderingProviderNpi))
                renderingProvider.Npi = Settings.Default.RenderingProviderNpi;

            return renderingProvider;
        }

        public void SetDefaultRenderingProvider(Provider renderingProvider)
        {
            Settings.Default.RenderingProviderFirstName = renderingProvider.FirstName;
            Settings.Default.RenderingProviderLastName = renderingProvider.LastName;
            Settings.Default.RenderingProviderNpi = renderingProvider.Npi;
            Settings.Default.Save();
        }

        public void SetDefaultPatient(Patient patient)
        {
            Settings.Default.PatientFirstName = patient.FirstName;
            Settings.Default.PatientLastName = patient.LastName;
            Settings.Default.RenderingProviderFirstName = patient.RenderingProvider.FirstName;
            Settings.Default.RenderingProviderLastName = patient.RenderingProvider.LastName;
            Settings.Default.RenderingProviderNpi = patient.RenderingProvider.Npi;
            Settings.Default.Save();
        }


        public Provider PullDefaultBillingProvider(Provider billingProvider)
        {
            billingProvider.IsIndividual = Settings.Default.BillingProviderIsIndividual;

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderFirstName))
                billingProvider.FirstName = Settings.Default.BillingProviderFirstName;

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderLastName))
                billingProvider.LastName = Settings.Default.BillingProviderLastName;

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderNpi))
                billingProvider.Npi = Settings.Default.BillingProviderNpi;

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderName))
                billingProvider.BusinessName = Settings.Default.BillingProviderName;

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderAddressLineOne))
                billingProvider.Address.StreetOne = Settings.Default.BillingProviderAddressLineOne;

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderAddressLineTwo))
                billingProvider.Address.StreetTwo = Settings.Default.BillingProviderAddressLineTwo;

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderAddressCity))
                billingProvider.Address.City = Settings.Default.BillingProviderAddressCity;

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderAddressState))
                billingProvider.Address.State = Settings.Default.BillingProviderAddressState;

            if (!string.IsNullOrEmpty(Settings.Default.BillingProviderAddressZipCode))
                billingProvider.Address.ZipCode = Settings.Default.BillingProviderAddressZipCode;
            return billingProvider;
        }

        public void SetDefaultBillingProvider(Provider billingProvider)
        {
            Settings.Default.BillingProviderFirstName = billingProvider.FirstName;
            Settings.Default.BillingProviderLastName = billingProvider.LastName;
            Settings.Default.BillingProviderNpi = billingProvider.Npi;
            Settings.Default.BillingProviderName = billingProvider.BusinessName;
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