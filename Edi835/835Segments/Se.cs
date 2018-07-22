using System.Text;

namespace Edi835._835Segments
{
    public class Se : SegmentBase
    {
        public Se(int totalSegmentCount)
        {
            this.SegmentIdentifier = "SE";
            this.TransactionSegmentCount = totalSegmentCount;
            this.TransactionSetControlNumber = "000000001";
        }

        private int TransactionSegmentCount { get; }
        private string TransactionSetControlNumber { get; }

        public string BuildSe()
        {
            var buildSe = new StringBuilder();

            buildSe.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.TransactionSegmentCount)
                .Append(this.DataElementTerminator)
                .Append(this.TransactionSetControlNumber)
                .Append(this.SegmentTerminator);

            return buildSe.ToString();
        }
    }
}