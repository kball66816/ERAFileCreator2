using System.Text;

namespace Edi835._835Segments
{
    public class St : SegmentBase
    {
        public St()
        {
            this.SegmentIdentifier = "ST";
            this.TransactionsetIdentifierCode = "835";
            this.TransactionSetControlNumber = "000000001";
        }

        private string TransactionsetIdentifierCode { get; }
        private string TransactionSetControlNumber { get; }

        public string BuildSt()
        {
            var buildSt = new StringBuilder();

            buildSt.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.TransactionsetIdentifierCode)
                .Append(this.DataElementTerminator)
                .Append(this.TransactionSetControlNumber)
                .Append(this.SegmentTerminator);

            return buildSt.ToString();
        }
    }
}