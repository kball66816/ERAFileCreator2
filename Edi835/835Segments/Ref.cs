using System.Text;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class Ref : SegmentBase
    {
        public Ref(Provider billingProvider)
        {
            this.SegmentIdentifier = "REF";
            this.ReferenceIdQualifier = "TJ";
            this.ReferenceIdentification = "1440054";
        }

        public Ref(Provider billingProvider, bool isIndividual)
        {
            this.SegmentIdentifier = "REF";
            this.ReferenceIdQualifier = "TJ";
            this.ReferenceIdentification = "123456789";
        }

        public Ref(InsuranceCompany insurance)
        {
            this.SegmentIdentifier = "REF";
            this.ReferenceIdQualifier = "2U";
            this.ReferenceIdentification = "20123";
        }

        public Ref(ServiceDescription charge)
        {
            this.SegmentIdentifier = "REF";
            this.ReferenceIdQualifier = "6R";
            this.ReferenceIdentification = charge.ReferenceId;
        }

        private string ReferenceIdQualifier { get; }
        private string ReferenceIdentification { get; }

        public Ref(string authorizationNumber)
        {
            this.SegmentIdentifier = "REF";
            this.ReferenceIdQualifier = "G1";
            this.ReferenceIdentification = authorizationNumber;
        }
        public string BuildRef()
        {
            var buildRef = new StringBuilder();

            buildRef.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.ReferenceIdQualifier)
                .Append(this.DataElementTerminator)
                .Append(this.ReferenceIdentification)
                .Append(this.SegmentTerminator);

            return buildRef.ToString();
        }
    }
}