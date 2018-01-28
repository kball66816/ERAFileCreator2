using System.Text;

namespace Edi835.Segments
{
    public class Se : SegmentBase
    {
        public Se(int totalSegmentCount)
        {
            SegmentIdentifier = "SE";
            TransactionSegmentCount = totalSegmentCount;
            TransactionSetControlNumber = "000000001";
        }

        private int TransactionSegmentCount { get; }
        private string TransactionSetControlNumber { get; }

        public string BuildSe()
        {
            var buildSe = new StringBuilder();

            buildSe.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(TransactionSegmentCount)
                .Append(DataElementTerminator)
                .Append(TransactionSetControlNumber)
                .Append(SegmentTerminator);

            return buildSe.ToString();
        }
    }
}