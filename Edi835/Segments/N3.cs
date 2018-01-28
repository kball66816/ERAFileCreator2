using System.Text;
using PatientManagement.Model;

namespace Edi835.Segments
{
    public class N3 : SegmentBase
    {
        public N3(InsuranceCompany insurance)
        {
            SegmentIdentifier = "N3";

            Insurance = insurance;
            AddressLineOne = insurance.Address.StreetOne;
            AddressLineTwo = insurance.Address.StreetTwo;
        }

        public N3(Provider billingProvider)
        {
            SegmentIdentifier = "N3";
            Provider = billingProvider;
            AddressLineOne = Provider.Address.StreetOne;
            AddressLineTwo = Provider.Address.StreetTwo;
        }

        private Provider Provider { get; }
        private InsuranceCompany Insurance { get; }
        private string AddressLineOne { get; }
        private string AddressLineTwo { get; }


        public string BuildN3()
        {
            var buildN3 = new StringBuilder();

            buildN3.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(AddressLineOne);

            if (!string.IsNullOrEmpty(AddressLineTwo))
                buildN3.Append(DataElementTerminator)
                    .Append(AddressLineTwo);

            buildN3.Append(SegmentTerminator);

            return buildN3.ToString();
        }
    }
}