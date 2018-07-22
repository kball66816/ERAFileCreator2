using System.Text;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class Trn : SegmentBase
    {
        public Trn(Payment payment, InsuranceCompany insurance)
        {
            this.SegmentIdentifier = "TRN";
            this.TraceTypeCode = "1";
            this.ReferenceIdentification = payment.Number;
            this.OriginatingCompanyIdentifier = "1" + insurance.TaxId;
            this.OriginatingCompanySupplementalCode = "13551";
        }

        private string TraceTypeCode { get; }
        private string ReferenceIdentification { get; }
        private string OriginatingCompanyIdentifier { get; }
        private string OriginatingCompanySupplementalCode { get; }

        public string BuildTrn()
        {
            var trn = new StringBuilder();

            trn.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.TraceTypeCode)
                .Append(this.DataElementTerminator)
                .Append(this.ReferenceIdentification)
                .Append(this.DataElementTerminator)
                .Append(this.OriginatingCompanyIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.OriginatingCompanySupplementalCode)
                .Append(this.SegmentTerminator);

            return trn.ToString();
        }
    }
}