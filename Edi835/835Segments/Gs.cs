using System.Text;

namespace Edi835._835Segments
{
    public class Gs : SegmentBase
    {
        public Gs()
        {
            this.SegmentIdentifier = "GS";
            this.FunctionalIdentifierCode = "HP";
            this.ApplicationSendersCode = "7802840731";
            this.ApplicationReceiversCode = "7234068";
            this.Date = "20160101";
            this.Time = "1200";
            this.GroupControlNumber = "201541257";
            this.ResponsibleAgencyCode = "X";
            this.IndustryIdentifierCode = "005010X221A1";
        }

        private string FunctionalIdentifierCode { get; }
        private string ApplicationSendersCode { get; }
        private string ApplicationReceiversCode { get; }
        private string Date { get; }
        private string Time { get; }
        private string GroupControlNumber { get; }
        private string ResponsibleAgencyCode { get; }
        private string IndustryIdentifierCode { get; }

        public string BuildGs()
        {
            var buildGs = new StringBuilder();

            buildGs.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.FunctionalIdentifierCode)
                .Append(this.DataElementTerminator)
                .Append(this.ApplicationSendersCode)
                .Append(this.DataElementTerminator)
                .Append(this.ApplicationReceiversCode)
                .Append(this.DataElementTerminator)
                .Append(this.Date)
                .Append(this.DataElementTerminator)
                .Append(this.Time)
                .Append(this.DataElementTerminator)
                .Append(this.GroupControlNumber)
                .Append(this.DataElementTerminator)
                .Append(this.ResponsibleAgencyCode)
                .Append(this.DataElementTerminator)
                .Append(this.IndustryIdentifierCode)
                .Append(this.SegmentTerminator);

            return buildGs.ToString();
        }
    }
}