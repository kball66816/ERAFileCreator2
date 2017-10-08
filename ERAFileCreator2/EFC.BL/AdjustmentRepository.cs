using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientManagement.DAL;
using PatientManagement.Model;

namespace EFC.BL
{
    class AdjustmentRepository:IAdjustmentRepository
    {
        public AdjustmentRepository(PrimaryCharge charge)
        {
            Adjustments = charge.AdjustmentList;
        }

        private ObservableCollection<Adjustment> Adjustments;

        public void Add(Adjustment adjustment)
        {
            Adjustments.Add(adjustment);
        }

        public void Delete(Adjustment adjustment)
        {
            Adjustments.Remove(adjustment);
        }

        public Adjustment UpdateAdjustment(Adjustment adjustment)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Adjustment> GetAllAdjustments()
        {
            return Adjustments;
        }

        public Adjustment GetSelectedAdjustment(Guid id)
        {
          return  Adjustments.FirstOrDefault(a => a.Id == id);
        }
    }
}
