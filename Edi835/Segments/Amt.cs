using System.Text;
using Edi835.Segments;
using PatientManagement.Model;

namespace EDI835.Segments
{
    public class Amt : SegmentBase
    {
        public Amt(Charge charge)
        {
            SegmentIdentifier = "AMT";
            AmountQualifier = "B6";
            AllowedAmount = charge.AllowedAmount;
        }
        public string BuildAmt()
        {

            var buildAmt = new StringBuilder();

            buildAmt.Append(SegmentIdentifier);
            buildAmt.Append(DataElementTerminator);
            buildAmt.Append(AmountQualifier);
            buildAmt.Append(DataElementTerminator);
            buildAmt.Append(AllowedAmount);
            buildAmt.Append(SegmentTerminator);
            return buildAmt.ToString();
        }

        private string AmountQualifier { get; set; }
        private decimal AllowedAmount { get; set; }
    }
}
