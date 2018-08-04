using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PatientManagement.DAL.Interfaces;

namespace PatientManagement.DAL
{
    public class Patient : IPerson, INotifyPropertyChanged
    {
        private ObservableCollection<ServiceDescription> _charges;

        private string _firstName;

        private string _fullName;

        private string _lastName;

        public Patient()
        {
            this.Id = Guid.NewGuid();
            this.MemberId = "ZLF1155487866";
            this.Subscriber = new Subscriber();
            this.RenderingProvider = new Provider();
            this.Charges = new ObservableCollection<ServiceDescription>();
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

        public ObservableCollection<ServiceDescription> Charges
        {
            get => this._charges;
            set
            {
                if (value == this._charges) return;
                this._charges = value;
                this.RaisePropertyChanged("Charges");
            }
        }

        public string MemberId { get; set; }

        public Guid Id { get; set; }

        public bool IncludeSubscriber { get; set; }

        public Subscriber Subscriber { get; set; }

        public Provider RenderingProvider { get; set; }

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
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Patient CopyPatient()
        {
            var clone = (Patient) this.MemberwiseClone();
            clone.Id = Guid.NewGuid();
            clone.Charges = new ObservableCollection<ServiceDescription>();
            clone.RenderingProvider = new Provider(this.RenderingProvider);
            return clone;
        }
    }
}