using System.Text;
using PatientManagement.Model;

namespace Edi835.Segments
{
    public class Cas : SegmentBase
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

        private string ClaimAdjustmentGroupCode { get; }
        private string ClaimAdjustmentReasonCode { get; }
        private decimal MonetaryAmount { get; }
        private PrimaryCharge Charge { get; }
        private Adjustment Adjustment { get; }

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