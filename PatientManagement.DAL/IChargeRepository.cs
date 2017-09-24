using PatientManagement.Model;
using System;
using System.Collections.ObjectModel;

namespace PatientManagement.DAL
{
    public interface IChargeRepository
    {
        void Add(Charge charge);

        void Delete(Charge charge);

        Charge UpdateCharge(Charge charge);

        ObservableCollection<Charge> GetAllCharges();

        Charge GetSelectedCharge(Guid id);
    } 
}
