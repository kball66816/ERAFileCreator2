using System.Text;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class Amt : SegmentBase
    {
        public Amt(ServiceDescription charge)
        {
            this.SegmentIdentifier = "AMT";
            this.AmountQualifier = "B6";
            this.AllowedAmount = charge.AllowedAmount;
        }

        private string AmountQualifier { get; }
        private decimal AllowedAmount { get; }

        public string BuildAmt()
        {
            var buildAmt = new StringBuilder();

            buildAmt.Append(this.SegmentIdentifier);
            buildAmt.Append(this.DataElementTerminator);
            buildAmt.Append(this.AmountQualifier);
            buildAmt.Append(this.DataElementTerminator);
            buildAmt.Append(this.AllowedAmount);
            buildAmt.Append(this.SegmentTerminator);
            return buildAmt.ToString();
        }
    }
}