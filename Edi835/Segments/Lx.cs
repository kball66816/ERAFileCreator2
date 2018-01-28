using System.Text;

namespace Edi835.Segments
{
    public class Lx:SegmentBase
    {
        public Lx()
        {
            SegmentIdentifier = "LX";

            ClaimSequenceNumber = "1";
        }

        private string ClaimSequenceNumber { get; set; }

        public string BuildLx()
        {
            var buildLx = new StringBuilder();

            buildLx.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(ClaimSequenceNumber)
                .Append(SegmentTerminator);

            return buildLx.ToString();
        }
    }
}
