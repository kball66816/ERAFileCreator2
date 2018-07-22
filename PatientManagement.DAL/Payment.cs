using System;
using System.Collections.Generic;
using System.ComponentModel;
using Common.Common.Extensions;

namespace PatientManagement.DAL
{
    public class Payment
    {
        private decimal _amount;

        private DateTime _date;

        public Dictionary<string, string> Types = new Dictionary<string, string>
        {
            {"EFT", "ACH"},
            {"Financial Institution Option", "BPO"},
            {"Check", "CHK"},
            {"Wire Transfer", "FWT"},
            {"Non Payment", "NON"}
        };

        public Payment()
        {
            if (this.Type == null) this.Type = "CHK";
            this.Date = DateTime.Today;
            this.Number = DateTime.Now.ToString("yyyyMMddhhmmssff");
        }

        public decimal Amount
        {
            get => this._amount;
            set
            {
                if (value != this._amount)
                {
                    this._amount = value.Truncated(2);
                    this.RaisePropertyChanged("Amount");
                }
            }
        }

        public DateTime Date
        {
            get => this._date;
            set
            {
                if (value != this._date)
                {
                    this._date = value;
                    this.RaisePropertyChanged("CheckDate");
                }
            }
        }

        public string Type { get; set; }

        public string Number { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}