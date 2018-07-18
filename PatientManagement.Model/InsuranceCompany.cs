using System.ComponentModel;
using Common.Common;

namespace PatientManagement.Model
{
    public class InsuranceCompany : INotifyPropertyChanged
    {
        public InsuranceCompany()
        {
            Address = new Address();
        }

        public InsuranceCompany(InsuranceCompany insurance)
        {
            Name = insurance.Name;
            TaxId = insurance.TaxId;
            Address = new Address
            {
                StreetOne = insurance.Address.StreetOne,
                StreetTwo = insurance.Address.StreetTwo,
                City = insurance.Address.City,
                State = insurance.Address.State,
                ZipCode = insurance.Address.ZipCode
            };
        }

        public string Name { get; set; }

        public string TaxId { get; set; }


        public Address Address { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}