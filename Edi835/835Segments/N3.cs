using System.Text;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class N3 : SegmentBase
    {
        public N3(InsuranceCompany insurance)
        {
            this.SegmentIdentifier = "N3";
            this.AddressLineOne = insurance.Address.StreetOne;
            this.AddressLineTwo = insurance.Address.StreetTwo;
        }

        public N3(Provider billingProvider)
        {
            this.SegmentIdentifier = "N3";
            this.AddressLineOne = billingProvider.Address.StreetOne;
            this.AddressLineTwo = billingProvider.Address.StreetTwo;
        }

        private string AddressLineOne { get; }
        private string AddressLineTwo { get; }


        public string BuildN3()
        {
            var buildN3 = new StringBuilder();

            buildN3.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.AddressLineOne);

            if (!string.IsNullOrEmpty(this.AddressLineTwo))
                buildN3.Append(this.DataElementTerminator)
                    .Append(this.AddressLineTwo);

            buildN3.Append(this.SegmentTerminator);

            return buildN3.ToString();
        }
    }
}