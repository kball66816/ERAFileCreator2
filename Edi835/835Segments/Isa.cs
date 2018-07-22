using System.Text;

namespace Edi835._835Segments
{
    public class Isa : SegmentBase
    {
        public Isa()
        {
            this.SegmentIdentifier = "ISA";
            this.AuthorizationInfoQualifier = "00";
            this.AuthorizationInformation = "          ";
            this.SecurityInfoQualifier = "00";
            this.SecurityInformation = "          ";
            this.InterchangeIdQualifierSender = "30";
            this.InterchangeSenderId = "166055205UPSERB";
            this.InterchangeIdQualifierReceiver = "30";
            this.InterchangeReceiverId = "074056672XPCMXJ";
            this.InterchangeDate = "160101";
            this.InterchangeTime = "1200";
            this.ReptitionSeperator = "^";
            this.InterchangeControlVersionNumber = "00501";
            this.InterchangeControlNumber = "201541257";
            this.AcknowledgementRequested = "0";
            this.UsageIndicator = "P";
            this.ComponentElementSeperator = ":";
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

            buildIsa.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.AuthorizationInfoQualifier)
                .Append(this.DataElementTerminator)
                .Append(this.AuthorizationInformation)
                .Append(this.DataElementTerminator)
                .Append(this.SecurityInfoQualifier)
                .Append(this.DataElementTerminator)
                .Append(this.SecurityInformation)
                .Append(this.DataElementTerminator)
                .Append(this.InterchangeIdQualifierSender)
                .Append(this.DataElementTerminator)
                .Append(this.InterchangeSenderId)
                .Append(this.DataElementTerminator)
                .Append(this.InterchangeIdQualifierReceiver)
                .Append(this.DataElementTerminator)
                .Append(this.InterchangeReceiverId)
                .Append(this.DataElementTerminator)
                .Append(this.InterchangeDate)
                .Append(this.DataElementTerminator)
                .Append(this.InterchangeTime)
                .Append(this.DataElementTerminator)
                .Append(this.ReptitionSeperator)
                .Append(this.DataElementTerminator)
                .Append(this.InterchangeControlVersionNumber)
                .Append(this.DataElementTerminator)
                .Append(this.InterchangeControlNumber)
                .Append(this.DataElementTerminator)
                .Append(this.AcknowledgementRequested)
                .Append(this.DataElementTerminator)
                .Append(this.UsageIndicator)
                .Append(this.DataElementTerminator)
                .Append(this.ComponentElementSeperator)
                .Append(this.SegmentTerminator);

            return buildIsa.ToString();
        }
    }
}