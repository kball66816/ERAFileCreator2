using System.Collections.Generic;
using PatientManagement.DAL;

namespace EraFileCreator.Services
{
    public interface ISettingsService
    {
        bool ReuseCharge { get; set; }

        bool PatientPromptEnabled { get; set; }

        bool ReuseSamePatientEnabled { get; set; }

        List<InsuranceCompany> GetInsuranceCompanies();

        void SaveInsuranceCompanies(List<InsuranceCompany> insurancecompanies);
        void SetDefaultPreferences(Preference preference);

        Preference PullDefaultPreferences(Preference preference);

        //InsuranceCompany PullDefaultInsurance(InsuranceCompany insurance);

        //void SetDefaultInsurance(InsuranceCompany insurance);
        Patient PullDefaultPatient(Patient patient);

        Provider PullDefaultRenderingProvider(Provider renderingProvider);

        void SetDefaultRenderingProvider(Provider renderingProvider);

        void SetDefaultPatient(Patient patient);

        Provider PullDefaultBillingProvider(Provider billingProvider);

        void SetDefaultBillingProvider(Provider billingProvider);
    }
}
