using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PatientManagement.DAL.Interfaces;

namespace PatientManagement.DAL
{
    public class Patient : IPerson, INotifyPropertyChanged
    {
        private ObservableCollection<ServiceDescription> charges;

        private string firstName;

        private string fullName;

        private string lastName;

        public Patient()
        {
            this.MemberId = "ZLF1155487866";
            this.Subscriber = new Subscriber();
            this.RenderingProvider = new Provider();
            this.Charges = new ObservableCollection<ServiceDescription>();
            this.Id = Guid.NewGuid();
        }

        public string FullName
        {
            get
            {
                var fullName = this.FirstName;

                if (!string.IsNullOrEmpty(this.LastName)) fullName = fullName += " " + this.LastName;
                return fullName;
            }

            set
            {
                if (value != this.fullName)
                {
                    this.fullName = value;
                    this.RaisePropertyChanged("FullName");
                }
            }
        }

        public ObservableCollection<ServiceDescription> Charges
        {
            get => this.charges;
            set
            {
                if (value == this.charges) return;
                this.charges = value;
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
            get => this.firstName;
            set
            {
                if (value != this.firstName)
                {
                    this.firstName = value;
                    this.RaisePropertyChanged("FirstName");
                    this.RaisePropertyChanged("Name");
                    this.RaisePropertyChanged("FullName");
                }
            }
        }

        public string LastName
        {
            get => this.lastName;
            set
            {
                if (value != this.lastName)
                {
                    this.lastName = value;
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
            if (this.PropertyChanged != null) this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
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