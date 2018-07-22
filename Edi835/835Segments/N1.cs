using System.Text;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class N1 : SegmentBase
    {
        public N1(InsuranceCompany insurance)
        {
            this.SegmentIdentifier = "N1";
            this.EntityIdCode = "PR";
            this.Insurance = insurance;
            this.Name = insurance.Name;
            this.IdCodeQualifier = "XV";
            this.IdCode = "20123";
        }

        public N1(Provider billingProvider)
        {
            this.SegmentIdentifier = "N1";
            this.EntityIdCode = "PE";
            this.Provider = billingProvider;
            this.Name = billingProvider.BusinessName;
            this.IdCodeQualifier = "XX";
            this.IdCode = billingProvider.Npi;
        }

        private InsuranceCompany Insurance { get; }
        private Provider Provider { get; }
        private string EntityIdCode { get; }
        private string Name { get; }
        private string IdCodeQualifier { get; }
        private string IdCode { get; }

        public string BuildN1()
        {
            var buildN1 = new StringBuilder();

            buildN1.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.EntityIdCode)
                .Append(this.DataElementTerminator)
                .Append(this.Name)
                .Append(this.DataElementTerminator)
                .Append(this.IdCodeQualifier)
                .Append(this.DataElementTerminator)
                .Append(this.IdCode)
                .Append(this.SegmentTerminator);

            return buildN1.ToString();
        }
    }
}