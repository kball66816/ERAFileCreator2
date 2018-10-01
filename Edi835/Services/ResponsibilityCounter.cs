using System.Collections.Generic;
using System.Linq;
using PatientManagement.DAL;

namespace Edi835.Services
{
    public class ResponsibilityCounter
    {
        public decimal ClaimChargeAmountSum(ServiceDescription serviceDescription)
        {
            return serviceDescription.ChargeCost +
                serviceDescription.AdditionalServiceDescriptions.Sum(s => s.ChargeCost);
        }
        public decimal ClaimPaymentAmountSum(ServiceDescription serviceDescription)
        {
            return serviceDescription.PaymentAmount +
                   serviceDescription.AdditionalServiceDescriptions.Sum(s => s.SumOfChargePaid);
        }
        public IEnumerable<Adjustment> PatientResponsibilitySum(ServiceDescription serviceDescription)
        {
            var totalPatientResponsibility = serviceDescription.Adjustments.Where(a => a.AdjustmentType == "PR").ToList();
            foreach (var additionalService in serviceDescription.AdditionalServiceDescriptions)
            {
                var additionalList = additionalService.Adjustments.Where(a => a.AdjustmentType == "PR").ToList();
                totalPatientResponsibility.AddRange(additionalList);
            }

            return totalPatientResponsibility;
        }
    }
}
