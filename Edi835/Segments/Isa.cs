using System.Text;

namespace Edi835.Segments
{
    public class Isa : SegmentBase
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

        private string AuthorizationInfoQualifier { get; }
        private string AuthorizationInformation { get; }
        private string SecurityInfoQualifier { get; }
        private string SecurityInformation { get; }
        private string InterchangeIdQualifierSender { get; }
        private string InterchangeSenderId { get; }
        private string InterchangeIdQualifierReceiver { get; }
        private string InterchangeReceiverId { get; }
        private string InterchangeDate { get; }
        private string InterchangeTime { get; }
        private string ReptitionSeperator { get; }
        private string InterchangeControlVersionNumber { get; }
        private string InterchangeControlNumber { get; }
        private string AcknowledgementRequested { get; }
        private string UsageIndicator { get; }
        private string ComponentElementSeperator { get; }


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