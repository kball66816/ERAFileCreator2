using System.Text;

namespace EDI835.Segments
{
    public class Ge : SegmentBase
    {
        public Ge()
        {
            SegmentIdentifier = "GE";
            TransactionSetQuantity = "1";
            GroupControlNumber = "201541257";
        }


        public string BuildGe()
        {
            var buildGe = new StringBuilder();
            buildGe.Append("GE")
                .Append(DataElementTerminator)
                .Append(TransactionSetQuantity)
                .Append(DataElementTerminator)
                .Append(GroupControlNumber)
                .Append(SegmentTerminator);

            return buildGe.ToString();
        }

        private string TransactionSetQuantity { get; set; }
        private string GroupControlNumber { get; set; }
    }
}
