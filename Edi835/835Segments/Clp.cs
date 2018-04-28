using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatientManagement.Model;

namespace Edi835._835Segments
{
    public class Clp : SegmentBase
    {
        public Clp(ServiceDescription serviceDescription)
        {
            SegmentIdentifier = "CLP";

            ClaimSubmittersIdentifier = serviceDescription.BillId;
            ClaimStatusCode = "1";
            TotalClaimChargeAmount = serviceDescription.ChargeCost+serviceDescription.AdditionalServiceDescriptions.Sum(s=>s.ChargeCost);
            TotalClaimPaymentAmount = serviceDescription.PaymentAmount +
                                      serviceDescription.AdditionalServiceDescriptions.Sum(s => s.SumOfChargePaid);
            PatientResponsibility = serviceDescription.Copay +
                                    serviceDescription.AdditionalServiceDescriptions.Sum(s => s.Copay);
            ClaimFilingIndicatorCode = "12";
            PayerClaimControlNumber = "EMC5841338";
            FacilityTypeCode = serviceDescription.PlaceOfService.ServiceLocation;
            ClaimFrequencyCode = string.Empty;
            DrgCode = string.Empty;
            DrgWeight = 0;
            Dischargefractionpercentage = 0;
        }

        private string ClaimSubmittersIdentifier { get; }
        private string ClaimStatusCode { get; }
        private decimal TotalClaimChargeAmount { get; }
        private decimal TotalClaimPaymentAmount { get; }
        private decimal PatientResponsibility { get; }
        private string ClaimFilingIndicatorCode { get; }
        private string PayerClaimControlNumber { get; }
        private string FacilityTypeCode { get; }
        private string ClaimFrequencyCode { get; }
        private string DrgCode { get; }
        private int DrgWeight { get; }
        private int Dischargefractionpercentage { get; }

        public string BuildClp()
        {
            var buildClp = new StringBuilder();

            buildClp.Append(SegmentIdentifier);
            buildClp.Append(DataElementTerminator);
            buildClp.Append(ClaimSubmittersIdentifier);
            buildClp.Append(DataElementTerminator);
            buildClp.Append(ClaimStatusCode);
            buildClp.Append(DataElementTerminator);
            buildClp.Append(TotalClaimChargeAmount);
            buildClp.Append(DataElementTerminator);
            buildClp.Append(TotalClaimPaymentAmount);
            buildClp.Append(DataElementTerminator);

            if (PatientResponsibility > 0) buildClp.Append(PatientResponsibility);
            buildClp.Append(DataElementTerminator);
            buildClp.Append(ClaimFilingIndicatorCode);
            buildClp.Append(DataElementTerminator);
            buildClp.Append(PayerClaimControlNumber);

            if (!string.IsNullOrEmpty(FacilityTypeCode))
            {
                buildClp.Append(DataElementTerminator);
                buildClp.Append(FacilityTypeCode);
            }

            if (!string.IsNullOrEmpty(ClaimFrequencyCode))
            {
                buildClp.Append(DataElementTerminator);
                buildClp.Append(ClaimFrequencyCode);
            }

            if (!string.IsNullOrEmpty(DrgCode))
            {
                buildClp.Append(DataElementTerminator);
                buildClp.Append(DrgCode);
                buildClp.Append(DataElementTerminator);
                buildClp.Append(DrgWeight);
            }

            if (Dischargefractionpercentage > 0)
            {
                buildClp.Append(DataElementTerminator);
                buildClp.Append(Dischargefractionpercentage);
            }

            buildClp.Append(SegmentTerminator);
            return buildClp.ToString();
        }
    }
}