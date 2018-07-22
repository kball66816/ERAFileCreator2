using System.Text;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class Nm1 : SegmentBase
    {
        public Nm1(Patient patient)
        {
            this.SegmentIdentifier = "NM1";
            this.EntityTypeQualifier = "QC";
            this.EntityIdCode = "1";
            this.LastNameOrOrganizationName = patient.LastName;
            this.FirstName = patient.FirstName;
            this.MiddleName = patient.MiddleInitial;
            this.Suffix = patient.Suffix;
            this.Prefix = patient.Prefix;
            this.IdCodeQualifier = "MI";
            this.IdCode = patient.MemberId;
        }

        public Nm1(Provider renderingProvider)
        {
            this.SegmentIdentifier = "NM1";
            this.EntityTypeQualifier = "82";
            this.EntityIdCode = "1";
            this.LastNameOrOrganizationName = renderingProvider.LastName;
            this.FirstName = renderingProvider.FirstName;
            this.MiddleName = renderingProvider.MiddleInitial;
            this.Suffix = renderingProvider.Suffix;
            this.Prefix = renderingProvider.Prefix;
            this.IdCodeQualifier = "XX";
            this.IdCode = renderingProvider.Npi;
        }

        public Nm1(Subscriber subscriber)
        {
            this.SegmentIdentifier = "NM1";
            this.EntityTypeQualifier = "IL";
            this.LastNameOrOrganizationName = subscriber.LastName;
            this.FirstName = subscriber.FirstName;
            this.MiddleName = subscriber.MiddleInitial;
            this.Suffix = subscriber.Suffix;
            this.Prefix = subscriber.Prefix;
            this.IdCodeQualifier = "MI";
            this.IdCode = subscriber.MemberId;
        }

        private string EntityIdCode { get; }
        private string EntityTypeQualifier { get; }
        private string LastNameOrOrganizationName { get; }
        private string FirstName { get; }
        private string MiddleName { get; }
        private string Suffix { get; }
        private string Prefix { get; }
        private string IdCodeQualifier { get; }
        private string IdCode { get; }

        public string BuildNm1()
        {
            var buildNm1 = new StringBuilder();

            buildNm1.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.EntityTypeQualifier)
                .Append(this.DataElementTerminator)
                .Append(this.EntityIdCode)
                .Append(this.DataElementTerminator)
                .Append(this.LastNameOrOrganizationName)
                .Append(this.DataElementTerminator)
                .Append(this.FirstName)
                .Append(this.DataElementTerminator)
                .Append(this.MiddleName)
                .Append(this.DataElementTerminator)
                .Append(this.Prefix)
                .Append(this.DataElementTerminator)
                .Append(this.Suffix);
            this.AppendIfIdCodeOrQualifierExists(buildNm1);
            buildNm1.Append(this.SegmentTerminator);
            return buildNm1.ToString();
        }

        private void AppendIfIdCodeOrQualifierExists(StringBuilder buildNm1)
        {
            if (!string.IsNullOrEmpty(this.IdCodeQualifier) || string.IsNullOrEmpty(this.IdCode))
                buildNm1.Append(this.DataElementTerminator)
                    .Append(this.IdCodeQualifier)
                    .Append(this.DataElementTerminator)
                    .Append(this.IdCode);
        }
    }
}