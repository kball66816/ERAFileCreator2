using System;
using System.Collections.Generic;
using PatientManagement.Model;

namespace PatientManagement.DAL
{
    public interface IAdjustmentRepository
    {
        void Add(Adjustment adjustment);

        void Delete(Adjustment adjustment);

        Adjustment UpdateAdjustment(Adjustment adjustment);

        List<Adjustment> GetAllAdjustments();

        Adjustment GetSelectedAdjustment(Guid id);

        IEnumerable<Adjustment> GetSelectedAdjustments(Guid chargeId);
    }
}