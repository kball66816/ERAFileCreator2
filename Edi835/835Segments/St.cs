using System.Text;

namespace Edi835._835Segments
{
    public class St : SegmentBase
    {
        public St()
        {
            SegmentIdentifier = "ST";
            TransactionsetIdentifierCode = "835";
            TransactionSetControlNumber = "000000001";
        }

        private string TransactionsetIdentifierCode { get; }
        private string TransactionSetControlNumber { get; }

        public string BuildSt()
        {
            var buildSt = new StringBuilder();

            buildSt.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(TransactionsetIdentifierCode)
                .Append(DataElementTerminator)
                .Append(TransactionSetControlNumber)
                .Append(SegmentTerminator);

            return buildSt.ToString();
        }
    }
}