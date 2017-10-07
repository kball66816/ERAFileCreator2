using PatientManagement.Model;
using System.Text;
using Edi835.Segments;

namespace EDI835.Segments
{
   public class Nm1 : SegmentBase
    {
        public Nm1(Patient patient)
        {
            SegmentIdentifier = "NM1";
            EntityTypeQualifier = "QC";
            LastNameOrOrganizationName = patient.LastName;
            FirstName = patient.FirstName;
            MiddleName = patient.MiddleInitial;
            Suffix = patient.Suffix;
            Prefix = patient.Prefix;
            IdCodeQualifier = "MI";
            IdCode = patient.MemberId;

        }

        public Nm1(Provider renderingProvider)
        {
            SegmentIdentifier = "NM1";
            EntityTypeQualifier = "82";
            LastNameOrOrganizationName = renderingProvider.LastName;
            FirstName = renderingProvider.FirstName;
            MiddleName = renderingProvider.MiddleInitial;
            Suffix = renderingProvider.Suffix;
            Prefix = renderingProvider.Prefix;
            IdCodeQualifier = "XX";
            IdCode = renderingProvider.Npi;
        }

        public Nm1(Subscriber subscriber)
        {
            SegmentIdentifier = "NM1";
            EntityTypeQualifier = "IL";
            LastNameOrOrganizationName = subscriber.LastName;
            FirstName = subscriber.FirstName;
            MiddleName = subscriber.MiddleInitial;
            Suffix = subscriber.Suffix;
            Prefix = subscriber.Prefix;
            IdCodeQualifier = "MI";
            IdCode = subscriber.MemberId;
        }

        private string EntityIdCode { get; set; }
        private string EntityTypeQualifier { get; set; }
        private string LastNameOrOrganizationName { get; set; }
        private string FirstName { get; set; }
        private string MiddleName { get; set; }
        private string Suffix { get; set; }
        private string Prefix { get; set; }
        private string IdCodeQualifier { get; set; }
        private string IdCode { get; set; }

        public string BuildNm1()
        {
            var buildNm1 = new StringBuilder();

            buildNm1.Append(SegmentIdentifier)
                    .Append(DataElementTerminator)
                    .Append(EntityIdCode)
                    .Append(DataElementTerminator)
                    .Append(EntityTypeQualifier)
                    .Append(DataElementTerminator)
                    .Append(LastNameOrOrganizationName)
                    .Append(DataElementTerminator)
                    .Append(FirstName)
                    .Append(DataElementTerminator)
                    .Append(MiddleName)
                    .Append(DataElementTerminator)
                    .Append(Prefix)
                    .Append(DataElementTerminator)
                    .Append(Suffix);
            AppendIfIdCodeOrQualifierExists(buildNm1);
            buildNm1.Append(SegmentTerminator);
            return buildNm1.ToString();
        }

        private void AppendIfIdCodeOrQualifierExists(StringBuilder buildNm1)
        {
            if (!string.IsNullOrEmpty(IdCodeQualifier) || (string.IsNullOrEmpty(IdCode)))
            {
                buildNm1.Append(DataElementTerminator)
                    .Append(IdCodeQualifier)
                    .Append(DataElementTerminator)
                    .Append(IdCode);
            }
        }
    }
}
