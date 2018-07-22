using System.Linq;
using System.Text;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class Clp : SegmentBase
    {
        public Clp(ServiceDescription serviceDescription)
        {
            this.SegmentIdentifier = "CLP";

            this.ClaimSubmittersIdentifier = serviceDescription.BillId;
            this.ClaimStatusCode = serviceDescription.ClaimStatus.Code;
            this.TotalClaimChargeAmount = serviceDescription.ChargeCost +
                                          serviceDescription.AdditionalServiceDescriptions.Sum(s => s.ChargeCost);
            this.TotalClaimPaymentAmount = serviceDescription.PaymentAmount +
                                           serviceDescription.AdditionalServiceDescriptions.Sum(s => s.SumOfChargePaid);
            this.PatientResponsibility = serviceDescription.Copay +
                                         serviceDescription.AdditionalServiceDescriptions.Sum(s => s.Copay);
            this.ClaimFilingIndicatorCode = "12";
            this.PayerClaimControlNumber = "EMC5841338";
            this.FacilityTypeCode = serviceDescription.PlaceOfService.ServiceLocation;
            this.ClaimFrequencyCode = string.Empty;
            this.DrgCode = string.Empty;
            this.DrgWeight = 0;
            this.Dischargefractionpercentage = 0;
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

            buildClp.Append(this.SegmentIdentifier);
            buildClp.Append(this.DataElementTerminator);
            buildClp.Append(this.ClaimSubmittersIdentifier);
            buildClp.Append(this.DataElementTerminator);
            buildClp.Append(this.ClaimStatusCode);
            buildClp.Append(this.DataElementTerminator);
            buildClp.Append(this.TotalClaimChargeAmount);
            buildClp.Append(this.DataElementTerminator);
            buildClp.Append(this.TotalClaimPaymentAmount);
            buildClp.Append(this.DataElementTerminator);

            if (this.PatientResponsibility > 0) buildClp.Append(this.PatientResponsibility);
            buildClp.Append(this.DataElementTerminator);
            buildClp.Append(this.ClaimFilingIndicatorCode);
            buildClp.Append(this.DataElementTerminator);
            buildClp.Append(this.PayerClaimControlNumber);

            if (!string.IsNullOrEmpty(this.FacilityTypeCode))
            {
                buildClp.Append(this.DataElementTerminator);
                buildClp.Append(this.FacilityTypeCode);
            }

            if (!string.IsNullOrEmpty(this.ClaimFrequencyCode))
            {
                buildClp.Append(this.DataElementTerminator);
                buildClp.Append(this.ClaimFrequencyCode);
            }

            if (!string.IsNullOrEmpty(this.DrgCode))
            {
                buildClp.Append(this.DataElementTerminator);
                buildClp.Append(this.DrgCode);
                buildClp.Append(this.DataElementTerminator);
                buildClp.Append(this.DrgWeight);
            }

            if (this.Dischargefractionpercentage > 0)
            {
                buildClp.Append(this.DataElementTerminator);
                buildClp.Append(this.Dischargefractionpercentage);
            }

            buildClp.Append(this.SegmentTerminator);
            return buildClp.ToString();
        }
    }
}