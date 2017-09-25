using System.Text;

namespace EDI835.Segments
{
    public class Per:SegmentBase
    {

        public Per()
        {
            SegmentIdentifier = "PER";
            ContactFunctionCode = "BL";
            Name = "EDI SUPPORT";
            CommunicationNumberQualifier = "TE";
            CommunicationNumber = "8888888888";
        }

        private string ContactFunctionCode { get; set; }
        private string Name { get; set; }
        private string CommunicationNumberQualifier { get; set; }
        private string CommunicationNumber { get; set; }

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
