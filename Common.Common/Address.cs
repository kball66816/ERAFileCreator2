using System.Collections.Generic;
using System.ComponentModel;

namespace Common.Common
{
    public class Address : INotifyPropertyChanged
    {
        private string state;

        public Address()
        {
            if (State == null) State = "AL";
        }

        public string StreetOne { get; set; }
        public string StreetTwo { get; set; }
        public string City { get; set; }

        public string State
        {
            get => state;
            set
            {
                if (value == state) return;
                state = value;
                RaisePropertyChanged("State");
            }
        }

        public string ZipCode { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}