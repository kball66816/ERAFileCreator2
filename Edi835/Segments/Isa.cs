using System.Text;

namespace EDI835.Segments
{
    public class Isa:SegmentBase
    {
        public Isa()
        {
            SegmentIdentifier = "ISA";
            AuthorizationInfoQualifier = "00";
            AuthorizationInformation = "          ";
            SecurityInfoQualifier = "00";
            SecurityInformation = "          ";
            InterchangeIdQualifierSender = "30";
            InterchangeSenderId = "166055205UPSERB";
            InterchangeIdQualifierReceiver = "30";
            InterchangeReceiverId = "074056672XPCMXJ";
            InterchangeDate = "160101";
            InterchangeTime = "1200";
            ReptitionSeperator = "^";
            InterchangeControlVersionNumber = "00501";
            InterchangeControlNumber = "201541257";
            AcknowledgementRequested = "0";
            UsageIndicator = "P";
            ComponentElementSeperator = ":";

        }

        private string AuthorizationInfoQualifier { get; set; }
        private string AuthorizationInformation { get; set; }
        private string SecurityInfoQualifier { get; set; }
        private string SecurityInformation { get; set; }
        private string InterchangeIdQualifierSender { get; set; }
        private string InterchangeSenderId { get; set; }
        private string InterchangeIdQualifierReceiver { get; set; }
        private string InterchangeReceiverId { get; set; }
        private string InterchangeDate { get; set; }
        private string InterchangeTime { get; set; }
        private string ReptitionSeperator { get; set; }
        private string InterchangeControlVersionNumber { get; set; }
        private string InterchangeControlNumber { get; set; }
        private string AcknowledgementRequested { get; set; }
        private string UsageIndicator { get; set; }
        private string ComponentElementSeperator { get; set; }


        public string BuildIsa()
        {
            var buildIsa = new StringBuilder();

            buildIsa.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(AuthorizationInfoQualifier)
                .Append(DataElementTerminator)
                .Append(AuthorizationInformation)
                .Append(DataElementTerminator)
                .Append(SecurityInfoQualifier)
                .Append(DataElementTerminator)
                .Append(SecurityInformation)
                .Append(DataElementTerminator)
                .Append(InterchangeIdQualifierSender)
                .Append(DataElementTerminator)
                .Append(InterchangeSenderId)
                .Append(DataElementTerminator)
                .Append(InterchangeIdQualifierReceiver)
                .Append(DataElementTerminator)
                .Append(InterchangeReceiverId)
                .Append(DataElementTerminator)
                .Append(InterchangeDate)
                .Append(DataElementTerminator)
                .Append(InterchangeTime)
                .Append(DataElementTerminator)
                .Append(ReptitionSeperator)
                .Append(DataElementTerminator)
                .Append(InterchangeControlVersionNumber)
                .Append(DataElementTerminator)
                .Append(InterchangeControlNumber)
                .Append(DataElementTerminator)
                .Append(AcknowledgementRequested)
                .Append(DataElementTerminator)
                .Append(UsageIndicator)
                .Append(DataElementTerminator)
                .Append(ComponentElementSeperator)
                .Append(SegmentTerminator);

            return buildIsa.ToString();
        }
    }

}