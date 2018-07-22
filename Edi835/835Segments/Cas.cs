using System.Text;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class Cas : SegmentBase
    {
        public Cas(ServiceDescription charge)
        {
            this.Charge = charge;
            this.SegmentIdentifier = "CAS";
            this.ClaimAdjustmentGroupCode = "PR";
            this.ClaimAdjustmentReasonCode = "3";
            this.MonetaryAmount = charge.Copay;
        }

        public Cas(Adjustment adjustment)
        {
            this.Adjustment = adjustment;
            this.SegmentIdentifier = "CAS";
            this.ClaimAdjustmentGroupCode = adjustment.AdjustmentType;
            this.ClaimAdjustmentReasonCode = adjustment.AdjustmentReasonCode;
            this.MonetaryAmount = adjustment.AdjustmentAmount;
        }

        private string ClaimAdjustmentGroupCode { get; }
        private string ClaimAdjustmentReasonCode { get; }
        private decimal MonetaryAmount { get; }
        private ServiceDescription Charge { get; }
        private Adjustment Adjustment { get; }

        public string BuildCas()
        {
            var buildCas = new StringBuilder();
            {
                buildCas.Append(this.SegmentIdentifier);
                buildCas.Append(this.DataElementTerminator);
                buildCas.Append(this.ClaimAdjustmentGroupCode);
                buildCas.Append(this.DataElementTerminator);
                buildCas.Append(this.ClaimAdjustmentReasonCode);
                buildCas.Append(this.DataElementTerminator);
                buildCas.Append(this.MonetaryAmount);
                buildCas.Append(this.SegmentTerminator);
            }
            return buildCas.ToString();
        }
    }
}