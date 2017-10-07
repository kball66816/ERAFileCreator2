using System.Text;
using Edi835.Segments;
using PatientManagement.Model;

namespace EDI835.Segments
{
    public class Cas:SegmentBase
    {
        public Cas(PrimaryCharge charge)
        {
            Charge = charge;
            SegmentIdentifier = "CAS";
            ClaimAdjustmentGroupCode = "PR";
            ClaimAdjustmentReasonCode = "3";
            MonetaryAmount = charge.Copay;
        }

        public Cas(Adjustment adjustment)
        {
            Adjustment = adjustment;
            SegmentIdentifier = "CAS";
            ClaimAdjustmentGroupCode = adjustment.AdjustmentType;
            ClaimAdjustmentReasonCode = adjustment.AdjustmentReasonCode;
            MonetaryAmount = adjustment.AdjustmentAmount;
        }

        private string ClaimAdjustmentGroupCode { get; set; }
        private string ClaimAdjustmentReasonCode { get; set; }
        private decimal MonetaryAmount { get; set; }
        private PrimaryCharge Charge { get; set; }
        private Adjustment Adjustment { get; set; }

        public string BuildCas()
        {

            var buildCas = new StringBuilder();
            {
                buildCas.Append(SegmentIdentifier);
                buildCas.Append(DataElementTerminator);
                buildCas.Append(ClaimAdjustmentGroupCode);
                buildCas.Append(DataElementTerminator);
                buildCas.Append(ClaimAdjustmentReasonCode);
                buildCas.Append(DataElementTerminator);
                buildCas.Append(MonetaryAmount);
                buildCas.Append(SegmentTerminator);
            }
            return buildCas.ToString();
        }
    }
}
