using System.ComponentModel;

namespace Common.Common
{
    public class Address : INotifyPropertyChanged
    {
        private string state;

        public Address()
        {
            if (this.State == null) this.State = "AL";
        }

        public string StreetOne { get; set; }
        public string StreetTwo { get; set; }
        public string City { get; set; }

        public string State
        {
            get => this.state;
            set
            {
                if (value == this.state) return;
                this.state = value;
                this.RaisePropertyChanged("State");
            }
        }

        public string ZipCode { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}