using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Common.Extensions;

namespace PatientManagement.Model
{
    public class Payment
    {
        public Payment()
        {
            if (Type == null) Type = "CHK";
            Date = DateTime.Today;
            Number = DateTime.Now.ToString("yyyyMMddhhmmssff");
        }
        private decimal _amount;

        private DateTime _date;
        public decimal Amount
        {
            get => this._amount;
            set
            {
                if (value != this._amount)
                {
                    this._amount = value.Truncated(2);
                    RaisePropertyChanged("Amount");
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
                    RaisePropertyChanged("CheckDate");
                }
            }
        }
        public string Type { get; set; }

        public string Number { get; set; }

        public Dictionary<string, string> Types = new Dictionary<string, string>
        {
            {"EFT", "ACH"},
            {"Financial Institution Option", "BPO"},
            {"Check", "CHK"},
            {"Wire Transfer", "FWT"},
            {"Non Payment", "NON"}
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
