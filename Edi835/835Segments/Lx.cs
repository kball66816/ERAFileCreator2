using System.Text;

namespace Edi835._835Segments
{
    public class Lx : SegmentBase
    {
        public Lx()
        {
            this.SegmentIdentifier = "LX";

            this.ClaimSequenceNumber = "1";
        }

        private string ClaimSequenceNumber { get; }

        public string BuildLx()
        {
            var buildLx = new StringBuilder();

            buildLx.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.ClaimSequenceNumber)
                .Append(this.SegmentTerminator);

            return buildLx.ToString();
        }
    }
}