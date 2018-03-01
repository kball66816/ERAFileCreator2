using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    internal static class AdjustmentService
    {
        static AdjustmentService()
        {
            AdjustmentRepository = new AdjustmentRepository();
        }

        private static readonly IAdjustmentRepository AdjustmentRepository;

        public static Adjustment GetNewAdjustment()
        {
            return new Adjustment();
        }

        public static void AssociateChargeId(Adjustment adjustment, Guid chargeId)
        {
            adjustment.ChargeId = chargeId;
        }

        public static void Add(Adjustment adjustment)
        {
            AdjustmentRepository.Add(adjustment);
        }
    }
}
