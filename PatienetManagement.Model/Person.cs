using System.ComponentModel;

namespace PatientManagement.Model
{
    /// <summary>
    /// Base Properties of a person
    /// </summary>
    public abstract class Person:INotifyPropertyChanged
    {
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if(value !=firstName)
                {
                    firstName = value;
                    RaisePropertyChanged("FirstName");
                    RaisePropertyChanged("Name");
                    RaisePropertyChanged("FullName");
                }
              
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
        }
                
        public string MiddleInitial { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged!=null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
