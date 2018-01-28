using System.Text;
using PatientManagement.Model;

namespace Edi835.Segments
{
    public class N1:SegmentBase
    {

        public N1(InsuranceCompany insurance)
        {
            SegmentIdentifier = "N1";
            EntityIdCode = "PR";
            Insurance = insurance;
            Name = insurance.Name;
            IdCodeQualifier = "XV";
            IdCode = "20123";
        }

        public N1(Provider billingProvider)
        {
            SegmentIdentifier = "N1";
            EntityIdCode = "PE";
            Provider = billingProvider;
            Name = billingProvider.BusinessName;
            IdCodeQualifier = "XX";
            IdCode = billingProvider.Npi;
        }

        private InsuranceCompany Insurance { get; set; }
        private Provider Provider { get; set; }
        private string EntityIdCode { get; set; }
        private string Name { get; set; }
        private string IdCodeQualifier { get; set; }
        private string IdCode { get; set; }

        public string BuildN1()
        {
            var buildN1 = new StringBuilder();

            buildN1.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(EntityIdCode)
                .Append(DataElementTerminator)
                .Append(Name)
                .Append(DataElementTerminator)
                .Append(IdCodeQualifier)
                .Append(DataElementTerminator)
                .Append(IdCode)
                .Append(SegmentTerminator);

            return buildN1.ToString();

        }
    }
}
