using PatientManagement.Model;
using System.Text;

namespace EDI835.Segments
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

        private Provider Provider { get; set; }
        private InsuranceCompany Insurance { get; set; }
        private string AddressLineOne { get; set; }
        private string AddressLineTwo { get; set; }


        public string BuildN3()
        {
            var buildN3 = new StringBuilder();

            buildN3.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(AddressLineOne);

                if(!string.IsNullOrEmpty(AddressLineTwo))
            {
                buildN3.Append(DataElementTerminator)
                .Append(AddressLineTwo);
            }

                buildN3.Append(SegmentTerminator);

            return buildN3.ToString();
        }
    }
}
