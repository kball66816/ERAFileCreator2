using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PatientManagement.DAL;
using PatientManagement.Model;

namespace WPFERA.Services
{
    class ChargeRepository : IChargeRepository, INotifyPropertyChanged
    {

        ObservableCollection<Charge> charges = new ObservableCollection<Charge>();

        public void Add(Charge charge)
        {
           charges.Add(charge);
        }

        public void Delete(Charge charge)
        {
            charges.Remove(charge);
        }

        public ObservableCollection<Charge> GetAllCharges()
        {
            return charges;
        }

        public Charge GetSelectedCharge(Guid id)
        {
            return charges.FirstOrDefault(c => c.Id == id);
        }

        public Charge UpdateCharge(Charge charge)
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
