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
   public class AdjustmentRepository : IAdjustmentRepository
    {

        private static readonly List<Adjustment> Adjustments = new List<Adjustment>();

        public void Add(Adjustment adjustment)
        {
            var existing = GetSelectedAdjustment(adjustment.Id);
            if (existing == null)
            {
                Adjustments.Add(adjustment);
            }
        }

        public void Delete(Adjustment adjustment)
        {
            Adjustments.Remove(adjustment);
        }

        public Adjustment UpdateAdjustment(Adjustment adjustment)
        {
            throw new NotImplementedException();
        }

        public List<Adjustment> GetAllAdjustments()
        {
            return Adjustments;
        }

        public Adjustment GetSelectedAdjustment(Guid id)
        {
            return Adjustments.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Adjustment> GetSelectedAdjustments(Guid chargeId)
        {
            return Adjustments.Where(a => a.ChargeId == chargeId && a.AdjustmentAmount > 0);
        }
    }
}
