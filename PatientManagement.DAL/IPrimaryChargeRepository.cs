using PatientManagement.Model;
using System;
using System.Collections.ObjectModel;

namespace PatientManagement.DAL
{
    public interface IPrimaryChargeRepository
    {
        void Add(PrimaryCharge charge);

        void Delete(PrimaryCharge charge);

        PrimaryCharge UpdateCharge(PrimaryCharge charge);

        ObservableCollection<PrimaryCharge> GetAllCharges();

        PrimaryCharge GetSelectedCharge(Guid id);
    } 
}
