using System.ComponentModel;

namespace PatientManagement.DAL
{
    /// <summary>
    ///     Base Properties of a person
    /// </summary>
    public abstract class Person : INotifyPropertyChanged
    {
        private string firstName;

        private string fullName;

        private string lastName;

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

        public string FullName
        {
            get
            {
                var fullName = this.FirstName;

                if (!string.IsNullOrEmpty(this.LastName)) fullName = fullName + " " + this.LastName;
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

        public string Suffix { get; set; }
        public string Prefix { get; set; }
        public string MiddleInitial { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null) this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}