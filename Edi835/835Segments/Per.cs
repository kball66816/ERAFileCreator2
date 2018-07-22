using System.Text;

namespace Edi835._835Segments
{
    public class Per : SegmentBase
    {
        public Per()
        {
            this.SegmentIdentifier = "PER";
            this.ContactFunctionCode = "BL";
            this.Name = "EDI SUPPORT";
            this.CommunicationNumberQualifier = "TE";
            this.CommunicationNumber = "8888888888";
        }

        private string ContactFunctionCode { get; }
        private string Name { get; }
        private string CommunicationNumberQualifier { get; }
        private string CommunicationNumber { get; }

        public string BuildPer()
        {
            var buildPer = new StringBuilder();

            buildPer.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.ContactFunctionCode)
                .Append(this.DataElementTerminator)
                .Append(this.Name)
                .Append(this.DataElementTerminator)
                .Append(this.CommunicationNumberQualifier)
                .Append(this.DataElementTerminator)
                .Append(this.CommunicationNumber)
                .Append(this.SegmentTerminator);

            return buildPer.ToString();
        }
    }
}