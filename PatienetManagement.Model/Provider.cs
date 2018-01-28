using System.ComponentModel;
using Common.Common;
using PatientManagement.Model.Interfaces;

namespace PatientManagement.Model
{
    /// <summary>
    ///     Base properties of a provider
    /// </summary>
    public class Provider : IPerson, INotifyPropertyChanged
    {
        private string businessName;

        private string firstName;

        private string fullName;

        private string lastName;

        private string npi;

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

        public string BusinessName
        {
            get => businessName;
            set
            {
                businessName = value;
                RaisePropertyChanged("BusinessName");
            }
        }

        public string FullName
        {
            get
            {
                var fullName = FirstName;

                if (!string.IsNullOrEmpty(LastName)) fullName = fullName += " " + LastName;
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

        public Address Address { get; set; }

        public bool IsAlsoRendering { get; set; }

        public string Npi
        {
            get => npi;
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

        public string FirstName
        {
            get => firstName;
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

        public string LastName
        {
            get => lastName;
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

        public string Suffix { get; set; }
        public string Prefix { get; set; }
        public string MiddleInitial { get; set; }


        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}