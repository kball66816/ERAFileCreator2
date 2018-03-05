using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using System;

namespace PatientManagement.ViewModel.Services
{
    internal static class AdjustmentService
    {
        static AdjustmentService()
        {
            AdjustmentRepository = new AdjustmentRepository();
        }

        public static readonly IAdjustmentRepository AdjustmentRepository;

        public static Adjustment GetNewAdjustment()
        {
            return new Adjustment();
        }

        public static void AssociateChargeId(Adjustment adjustment, Guid chargeId)
        {
            adjustment.ChargeId = chargeId;
        }
    }
}
