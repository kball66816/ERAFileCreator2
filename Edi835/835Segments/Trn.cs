using System.Text;
using PatientManagement.Model;

namespace Edi835._835Segments
{
    public class Trn : SegmentBase
    {
        public Trn(InsuranceCompany insurance)
        {
            SegmentIdentifier = "TRN";
            TraceTypeCode = "1";
            ReferenceIdentification = insurance.CheckNumber;
            OriginatingCompanyIdentifier = "1" + insurance.TaxId;
            OriginatingCompanySupplementalCode = "13551";
        }

        private string TraceTypeCode { get; }
        private string ReferenceIdentification { get; }
        private string OriginatingCompanyIdentifier { get; }
        private string OriginatingCompanySupplementalCode { get; }

        public string BuildTrn()
        {
            var trn = new StringBuilder();

            trn.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(TraceTypeCode)
                .Append(DataElementTerminator)
                .Append(ReferenceIdentification)
                .Append(DataElementTerminator)
                .Append(OriginatingCompanyIdentifier)
                .Append(DataElementTerminator)
                .Append(OriginatingCompanySupplementalCode)
                .Append(SegmentTerminator);

            return trn.ToString();
        }
    }
}