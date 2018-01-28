using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PatientManagement.Model.Interfaces;

namespace PatientManagement.Model
{
    public class Patient : IPerson, INotifyPropertyChanged
    {
        private ObservableCollection<PrimaryCharge> charges;

        private string firstName;

        private string fullName;

        private string lastName;

        public Patient()
        {
            MemberId = "ZLF1155487866";
            Subscriber = new Subscriber();
            RenderingProvider = new Provider();
            Charges = new ObservableCollection<PrimaryCharge>();
            Id = Guid.NewGuid();
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

        public ObservableCollection<PrimaryCharge> Charges
        {
            get => charges;
            set
            {
                if (value == charges) return;
                charges = value;
                RaisePropertyChanged("Charges");
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

        public Patient CopyPatient()
        {
            var clone = (Patient) MemberwiseClone();
            clone.Id = Guid.NewGuid();
            clone.Charges = new ObservableCollection<PrimaryCharge>();
            return clone;
        }
    }
}