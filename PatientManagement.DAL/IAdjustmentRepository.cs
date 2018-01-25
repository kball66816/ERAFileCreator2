using PatientManagement.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PatientManagement.DAL
{
    public interface IAdjustmentRepository
    {
        void Add(Adjustment adjustment);

        void Delete(Adjustment adjustment);

        Adjustment UpdateAdjustment(Adjustment adjustment);

        List<Adjustment> GetAllAdjustments();

        Adjustment GetSelectedAdjustment(Guid id);
    }
}
