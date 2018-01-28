using System.Text;

namespace Edi835.Segments
{
    public class Gs : SegmentBase
    {
        public Gs()
        {
            SegmentIdentifier = "GS";
            FunctionalIdentifierCode = "HP";
            ApplicationSendersCode = "7802840731";
            ApplicationReceiversCode = "7234068";
            Date = "20160101";
            Time = "1200";
            GroupControlNumber = "201541257";
            ResponsibleAgencyCode = "X";
            IndustryIdentifierCode = "005010X221A1";
        }

        private string FunctionalIdentifierCode { get; set; }
        private string ApplicationSendersCode { get; set; }
        private string ApplicationReceiversCode { get; set; }
        private string Date { get; set; }
        private string Time { get; set; }
        private string GroupControlNumber { get; set; }
        private string ResponsibleAgencyCode { get; set; }
        private string IndustryIdentifierCode { get; set; }

        public string BuildGs()
        {
            var buildGs = new StringBuilder();

            buildGs.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(FunctionalIdentifierCode)
                .Append(DataElementTerminator)
                .Append(ApplicationSendersCode)
                .Append(DataElementTerminator)
                .Append(ApplicationReceiversCode)
                .Append(DataElementTerminator)
                .Append(Date)
                .Append(DataElementTerminator)
                .Append(Time)
                .Append(DataElementTerminator)
                .Append(GroupControlNumber)
                .Append(DataElementTerminator)
                .Append(ResponsibleAgencyCode)
                .Append(DataElementTerminator)
                .Append(IndustryIdentifierCode)
                .Append(SegmentTerminator);

            return buildGs.ToString();

        }


    }
}
