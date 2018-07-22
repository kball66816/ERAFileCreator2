using System.ComponentModel;
using Common.Common;
using PatientManagement.DAL.Interfaces;

namespace PatientManagement.DAL
{
    /// <summary>
    ///     Base properties of a provider
    /// </summary>
    public class Provider : IPerson, INotifyPropertyChanged
    {
        private string _businessName;

        private string _firstName;

        private string _fullName;

        private string _lastName;

        private string _npi;

        public Provider(Provider provider)
        {
            this.Address = provider.Address;
            this.FirstName = provider.FirstName;
            this.LastName = provider.LastName;
            this.Npi = provider.Npi;
            this.MiddleInitial = provider.MiddleInitial;
        }

        public Provider()
        {
            this.Address = new Address();
        }

        public string BusinessName
        {
            get => this.IsIndividual ? this.FullName : this._businessName;
            set
            {
                this._businessName = value;
                this.RaisePropertyChanged("BusinessName");
            }
        }

        public string FullName
        {
            get
            {
                var fullName = this.FirstName;

                if (!string.IsNullOrEmpty(this.LastName)) fullName += " " + this.LastName;
                return fullName;
            }

            set
            {
                if (value != this._fullName)
                {
                    this._fullName = value;
                    this.RaisePropertyChanged("FullName");
                }
            }
        }

        public Address Address { get; set; }

        public string Npi
        {
            get => this._npi;
            set
            {
                if (value != this._npi)
                {
                    this._npi = value;
                    this.RaisePropertyChanged("Npi");
                }
            }
        }

        public bool IsIndividual { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string FirstName
        {
            get => this._firstName;
            set
            {
                if (value != this._firstName)
                {
                    this._firstName = value;
                    this.RaisePropertyChanged("FirstName");
                    this.RaisePropertyChanged("Name");
                    this.RaisePropertyChanged("FullName");
                }
            }
        }

        public string LastName
        {
            get => this._lastName;
            set
            {
                if (value != this._lastName)
                {
                    this._lastName = value;
                    this.RaisePropertyChanged("LastName");
                    this.RaisePropertyChanged("Name");
                    this.RaisePropertyChanged("FullName");
                }
            }
        }

        public string Suffix { get; set; }
        public string Prefix { get; set; }
        public string MiddleInitial { get; set; }


        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}