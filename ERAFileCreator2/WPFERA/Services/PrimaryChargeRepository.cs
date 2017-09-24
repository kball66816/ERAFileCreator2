using PatientManagement.DAL;
using PatientManagement.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace WPFERA.Services
{
    class PrimaryChargeRepository : IPrimaryChargeRepository, INotifyPropertyChanged
    {

        private ObservableCollection<PrimaryCharge> charges = new ObservableCollection<PrimaryCharge>();

        public void Add(PrimaryCharge charge)
        {
           charges.Add(charge);
        }

        public void Delete(PrimaryCharge charge)
        {
            charges.Remove(charge);
        }

        public ObservableCollection<PrimaryCharge> GetAllCharges()
        {
            return charges;
        }

        public PrimaryCharge GetSelectedCharge(Guid id)
        {
            return charges.FirstOrDefault(c => c.Id == id);
        }

        public PrimaryCharge UpdateCharge(PrimaryCharge charge)
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
