using System;
using System.Collections.Generic;
using PatientManagement.Model;

namespace PatientManagement.DAL
{
    public interface IPrimaryChargeRepository
    {
        void Add(PrimaryCharge charge);

        void Delete(PrimaryCharge charge);

        PrimaryCharge UpdateCharge(PrimaryCharge charge);

        List<PrimaryCharge> GetAllCharges();

        PrimaryCharge GetSelectedCharge(Guid id);

        IEnumerable<PrimaryCharge> GetSelectedCharges(Guid patientId);
    }
}