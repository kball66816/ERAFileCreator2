using System.Text;
using PatientManagement.Model;

namespace Edi835._835Segments
{
    public class N4 : SegmentBase
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

        private string City { get; }
        private string State { get; }
        private string ZipCode { get; }

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