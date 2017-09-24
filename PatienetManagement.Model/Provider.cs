using Common.Common;

namespace PatientManagement.Model
{
    /// <summary>
    /// Base properties of a provider
    /// </summary>
    public class Provider : Person
    {
        public Provider(Provider provider)
        {
            Address = provider.Address;
            FirstName = provider.FirstName;
            LastName = provider.LastName;
            Npi = provider.Npi;
            MiddleInitial = provider.MiddleInitial;
        }

        public Provider()
        {
            Address = new Address();
        }

        public Address Address { get; set; }

        public bool IsAlsoRendering { get; set; }

        private string npi;

        public string Npi
        {
            get { return npi; }
            set
            {
                if (value != npi)
                {
                    npi = value;
                    RaisePropertyChanged("Npi");
                }
            }
        }

        public bool IsIndividual { get; set; }
    }
}
