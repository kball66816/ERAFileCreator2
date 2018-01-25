using PatientManagement.Model;
using System;
using System.Collections.Generic;

namespace PatientManagement.DAL
{
    public interface IPrimaryChargeRepository
    {
        void Add(PrimaryCharge charge);

        void Delete(PrimaryCharge charge);

        PrimaryCharge UpdateCharge(PrimaryCharge charge);

        List<PrimaryCharge> GetAllCharges();

        PrimaryCharge GetSelectedCharge(Guid id);
    } 
}
