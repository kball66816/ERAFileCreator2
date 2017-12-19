using Edi835.Segments;
using PatientManagement.Model;
using System.Linq;
using System.Text;

namespace EDI835.Segments
{
    public class Clp : SegmentBase
    {
        public Clp(Patient patient)
        {
            PrimaryCharge charge = patient.Charges.FirstOrDefault();
            SegmentIdentifier = "CLP";

            if (charge == null) return;


            ClaimSubmittersIdentifier = charge.BillId;
            ClaimStatusCode = "1";
            TotalClaimChargeAmount = patient.Charges.Sum(c=>c.ChargeCost);
            TotalClaimPaymentAmount = patient.Charges.Sum(c=>c.SumOfChargePaid);
            PatientResponsibility = charge.Copay;
            ClaimFilingIndicatorCode = "12";
            PayerClaimControlNumber = "EMC5841338";
            FacilityTypeCode = charge.PlaceOfService.ServiceLocation;
            ClaimFrequencyCode = string.Empty;
            DrgCode = string.Empty;
            DrgWeight = 0;
            Dischargefractionpercentage = 0;
        }

        private string ClaimSubmittersIdentifier { get; set; }
        private string ClaimStatusCode { get; set; }
        private decimal TotalClaimChargeAmount { get; set; }
        private decimal TotalClaimPaymentAmount { get; set; }
        private decimal PatientResponsibility { get; set; }
        private string ClaimFilingIndicatorCode { get; set; }
        private string PayerClaimControlNumber { get; set; }
        private string FacilityTypeCode { get; set; }
        private string ClaimFrequencyCode { get; set; }
        private string DrgCode { get; set; }
        private int DrgWeight { get; set; }
        private int Dischargefractionpercentage { get; set; }

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

            if (PatientResponsibility > 0)
            {

                buildClp.Append(PatientResponsibility);
              
            }
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

            if(Dischargefractionpercentage >0)
            {
                buildClp.Append(DataElementTerminator);
                buildClp.Append(Dischargefractionpercentage);
            }

            buildClp.Append(SegmentTerminator);
            return buildClp.ToString();
        }
    }
}
