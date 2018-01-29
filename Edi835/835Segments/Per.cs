using System.Text;

namespace Edi835._835Segments
{
    public class Per : SegmentBase
    {
        public Per()
        {
            SegmentIdentifier = "PER";
            ContactFunctionCode = "BL";
            Name = "EDI SUPPORT";
            CommunicationNumberQualifier = "TE";
            CommunicationNumber = "8888888888";
        }

        private string ContactFunctionCode { get; }
        private string Name { get; }
        private string CommunicationNumberQualifier { get; }
        private string CommunicationNumber { get; }

        public string BuildPer()
        {
            var buildPer = new StringBuilder();

            buildPer.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(ContactFunctionCode)
                .Append(DataElementTerminator)
                .Append(Name)
                .Append(DataElementTerminator)
                .Append(CommunicationNumberQualifier)
                .Append(DataElementTerminator)
                .Append(CommunicationNumber)
                .Append(SegmentTerminator);

            return buildPer.ToString();
        }
    }
}