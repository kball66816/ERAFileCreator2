using PatientManagement.Model;

namespace PatientManagement.DAL
{
    public interface IProvider
    {
        void AddBillingProvider(Provider billingProvider);
        Provider GetBillingProvider();
    }
}