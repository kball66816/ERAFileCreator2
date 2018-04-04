using System.ComponentModel;

namespace PatientManagement.Model
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

        public string FullName
        {
            get
            {
                var fullName = FirstName;

                if (!string.IsNullOrEmpty(LastName)) fullName = fullName + " " + LastName;
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

        public event PropertyChangedEventHandler PropertyChanged;


        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}