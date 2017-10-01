using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientManagement.Model;

namespace PatientManagement.DAL
{
    public interface IProvider
    {
        void AddBillingProvider(Provider billingProvider);
        Provider GetBillingProvider();
    }
}
