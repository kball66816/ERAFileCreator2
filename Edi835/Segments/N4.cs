using PatientManagement.Model;
using System.Text;
using Edi835.Segments;

namespace EDI835.Segments
{
    public class N4:SegmentBase
    {
        public N4(InsuranceCompany insurance)
        {
            SegmentIdentifier = "N4";
            City = insurance.Address.City;
            State = insurance.Address.State;
            ZipCode = insurance.Address.ZipCode;
        }

        public N4(Provider billingProvider)
        {
            SegmentIdentifier = "N4";
            City = billingProvider.Address.City;
            State = billingProvider.Address.State;
            ZipCode = billingProvider.Address.ZipCode;
        }

        private string City { get; set; }
        private string State { get; set; }
        private string ZipCode { get; set; }

        public string BuildN4()
        {
            var buildN4 = new StringBuilder();

            buildN4.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(City)
                .Append(DataElementTerminator)
                .Append(State)
                .Append(DataElementTerminator)
                .Append(ZipCode)
                .Append(SegmentTerminator);

            return buildN4.ToString();
        }
    }
}
