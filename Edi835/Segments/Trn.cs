using PatientManagement.Model;
using System.Text;
using Edi835.Segments;

namespace EDI835.Segments
{
    public class Trn:SegmentBase
    {
        public Trn(InsuranceCompany insurance)
        {
            SegmentIdentifier = "TRN";
            TraceTypeCode = "1";
            ReferenceIdentification = insurance.CheckNumber;
            OriginatingCompanyIdentifier = "1" + insurance.TaxId;
            OriginatingCompanySupplementalCode = "13551";
        }

        private string TraceTypeCode { get; set; }
        private string ReferenceIdentification { get; set; }
        private string OriginatingCompanyIdentifier { get; set; }
        private string OriginatingCompanySupplementalCode { get; set; }

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
