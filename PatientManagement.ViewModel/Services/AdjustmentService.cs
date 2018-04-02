using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using System;
using Common.Common.Services;

namespace PatientManagement.ViewModel.Services
{
    internal static class AdjustmentService
    {
        public static Adjustment GetNewAdjustment()
        {
            return new Adjustment();
        }

    }
}
