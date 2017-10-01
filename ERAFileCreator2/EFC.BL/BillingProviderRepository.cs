using PatientManagement.DAL;
using PatientManagement.Model;

namespace EFC.BL
{
    public class BillingProviderRepository:IProvider
    {
        private static Provider _billingProvider;

        public void AddBillingProvider(Provider provider)
        {
            _billingProvider = provider;
        }

        public Provider GetBillingProvider()
        {
            return _billingProvider;
        }
    }
}
