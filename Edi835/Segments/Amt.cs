using System.Text;
using PatientManagement.Model;

namespace EDI835.Segments
{
    public class Amt : SegmentBase
    {
        public Amt(Charge charge)
        {
            this.Charge = charge;
            SegmentIdentifier = "AMT";
            AmountQualifier = "B6";
            allowedAmount = charge.AllowedAmount;
        }
        public string BuildAmt()
        {

            var buildAmt = new StringBuilder();

            buildAmt.Append(SegmentIdentifier);
            buildAmt.Append(DataElementTerminator);
            buildAmt.Append(AmountQualifier);
            buildAmt.Append(DataElementTerminator);
            buildAmt.Append(Charge.AllowedAmount);
            buildAmt.Append(SegmentTerminator);
            return buildAmt.ToString();
        }

        private string AmountQualifier { get; set; }
        private Charge Charge { get; set; }
        private decimal allowedAmount { get; set; }
    }
}
