using System;
using System.Collections.Generic;
using System.ComponentModel;
using Common.Common;
using Common.Common.Extensions;

namespace PatientManagement.Model
{
    public class InsuranceCompany : INotifyPropertyChanged
    {
        private decimal checkAmount;

        private DateTime checkDate;


        public Dictionary<string, string> PaymentTypes = new Dictionary<string, string>
        {
            {"EFT", "ACH"},
            {"Financial Institution Option", "BPO"},
            {"Check", "CHK"},
            {"Wire Transfer", "FWT"},
            {"Non Payment", "NON"}
        };


        public InsuranceCompany()
        {
            if (PaymentType == null) PaymentType = "CHK";
            Address = new Address();

            CheckDate = DateTime.Today;
            CheckNumber = DateTime.Now.ToString("yyyyMMddhhmmssff");
        }

        public InsuranceCompany(InsuranceCompany insurance)
        {
            Name = insurance.Name;
            TaxId = insurance.TaxId;
            CheckDate = insurance.CheckDate;
            PaymentType = insurance.PaymentType;
            Address = new Address
            {
                StreetOne = insurance.Address.StreetOne,
                StreetTwo = insurance.Address.StreetTwo,
                City = insurance.Address.City,
                State = insurance.Address.State,
                ZipCode = insurance.Address.ZipCode
            };
            CheckAmount = insurance.CheckAmount;
            CheckDate = DateTime.Today;
            CheckNumber = DateTime.Now.ToString("yyyyMMddhhmmssff");
        }

        public string Name { get; set; }

        public string TaxId { get; set; }

        public decimal CheckAmount
        {
            get => checkAmount;
            set
            {
                if (value != checkAmount)
                {
                    checkAmount = value.Truncated(2);
                    RaisePropertyChanged("CheckAmount");
                }
            }
        }

        public DateTime CheckDate
        {
            get => checkDate;
            set
            {
                if (value != checkDate)
                {
                    checkDate = value;
                    RaisePropertyChanged("CheckDate");
                }
            }
        }

        public Address Address { get; set; }

        public string PaymentType { get; set; }

        public string CheckNumber { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}