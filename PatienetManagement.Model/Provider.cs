using System.ComponentModel;
using Common.Common;
using PatientManagement.Model.Interfaces;

namespace PatientManagement.Model
{
    /// <summary>
    /// Base properties of a provider
    /// </summary>
    public class Provider : IPerson, INotifyPropertyChanged
    {
        public Provider(Provider provider)
        {
            Address = provider.Address;
            FirstName = provider.FirstName;
            LastName = provider.LastName;
            Npi = provider.Npi;
            MiddleInitial = provider.MiddleInitial;
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (value != firstName)
                {
                    firstName = value;
                    RaisePropertyChanged("FirstName");
                    RaisePropertyChanged("Name");
                    RaisePropertyChanged("FullName");
                }

            }
        }

        private string businessName;

        public string BusinessName
        {
            get
            {
                return businessName;
            }
            set
            {
                businessName = value;
                RaisePropertyChanged("BusinessName");
            }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    RaisePropertyChanged("LastName");
                    RaisePropertyChanged("Name");
                    RaisePropertyChanged("FullName");
                }

            }
        }

        private string fullName;

        public string FullName
        {
            get
            {
                string fullName = FirstName;

                if (!string.IsNullOrEmpty(LastName))
                {
                    fullName = fullName += " " + LastName;
                }
                return fullName;

            }

            set
            {
                if (value != fullName)
                {
                    fullName = value;
                    RaisePropertyChanged("FullName");
                }
            }
        }

        public string Suffix { get; set; }
        public string Prefix { get; set; }
        public string MiddleInitial { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;


        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
