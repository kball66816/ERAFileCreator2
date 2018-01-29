using System.Text;

namespace Edi835._835Segments
{
    public class Ge : SegmentBase
    {
        public Ge()
        {
            SegmentIdentifier = "GE";
            TransactionSetQuantity = "1";
            GroupControlNumber = "201541257";
        }

        private string TransactionSetQuantity { get; }
        private string GroupControlNumber { get; }


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
    }
}