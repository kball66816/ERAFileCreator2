using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatientManagement.Model;

namespace Edi835._835Segments
{
    public class Clp : SegmentBase
    {
        public Clp(IEnumerable<PrimaryCharge> encounters)
        {
            SegmentIdentifier = "CLP";

            if (encounters == null) return;

            encounters = encounters.ToList();
            ClaimSubmittersIdentifier = encounters.FirstOrDefault()?.BillId;
            ClaimStatusCode = "1";
            TotalClaimChargeAmount = encounters.Sum(c => c.ChargeCost);
            TotalClaimPaymentAmount = encounters.Sum(c => c.SumOfChargePaid);
            PatientResponsibility = encounters.Sum(c => c.Copay);
            ClaimFilingIndicatorCode = "12";
            PayerClaimControlNumber = "EMC5841338";
            FacilityTypeCode = encounters.FirstOrDefault()?.PlaceOfService.ServiceLocation;
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