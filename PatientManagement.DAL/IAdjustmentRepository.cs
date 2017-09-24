using PatientManagement.Model;
using System;
using System.Collections.ObjectModel;

namespace PatientManagement.DAL
{
    interface IAdjustmentRepository
    {
        void Add(Adjustment adjustment);

        void Delete(Adjustment adjustment);

        Adjustment UpdatePatient(Adjustment adjustment);

        ObservableCollection<Adjustment> GetAllAdjustments();

        Adjustment GetSelectedAdjustment(Guid id);
    }
}
