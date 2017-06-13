using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERAFileCreator
{
    class Provider
    {
        private string providerFirstName;

        public string ProviderFirstName
        {
            get { return providerFirstName; }
            set            {providerFirstName = value;}
            
        }
        private string providerLastName;

        public string ProviderLastName
        {
            get { return providerLastName; }
            set{ providerLastName = value;}
        }
        private string providerNPI;

        public string ProviderNPI
        {
            get { return providerNPI; }
            set { providerNPI = value; }
        }
    }
}
