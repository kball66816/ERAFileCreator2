using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    public interface ISettingsService
    {
        bool ReuseCharge { get; set; }

        bool PatientPromptEnabled { get; set; }

        bool ReuseSamePatientEnabled { get; set; }

        void SetDefaultPreferences(Preference preference);

        Preference PullDefaultPreferences(Preference preference);

        InsuranceCompany PullDefaultInsurance(InsuranceCompany insurance);

        void SetDefaultInsurance(InsuranceCompany insurance);
        Patient PullDefaultPatient(Patient patient);

        Provider PullDefaultRenderingProvider(Provider renderingProvider);

        void SetDefaultRenderingProvider(Provider renderingProvider);

        void SetDefaultPatient(Patient patient);

        Provider PullDefaultBillingProvider(Provider billingProvider);

        void SetDefaultBillingProvider(Provider billingProvider);
    }
}
