using System.Text;

namespace Edi835._835Segments
{
    public class Ge : SegmentBase
    {
        public Ge()
        {
            this.SegmentIdentifier = "GE";
            this.TransactionSetQuantity = "1";
            this.GroupControlNumber = "201541257";
        }

        private string TransactionSetQuantity { get; }
        private string GroupControlNumber { get; }


        public string BuildGe()
        {
            var buildGe = new StringBuilder();
            buildGe.Append("GE")
                .Append(this.DataElementTerminator)
                .Append(this.TransactionSetQuantity)
                .Append(this.DataElementTerminator)
                .Append(this.GroupControlNumber)
                .Append(this.SegmentTerminator);

            return buildGe.ToString();
        }
    }
}