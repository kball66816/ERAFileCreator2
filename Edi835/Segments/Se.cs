using System.Text;

namespace EDI835.Segments
{
    class Se:SegmentBase
    {
        public Se(int totalSegmentCount)
        {
            TransactionSegmentCount = totalSegmentCount;
            TransactionSetControlNumber = "000000001";


        }

        private int TransactionSegmentCount { get; set; }
        private string TransactionSetControlNumber { get; set; }

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
