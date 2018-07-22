using System.Text;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class N4 : SegmentBase
    {
        public N4(InsuranceCompany insurance)
        {
            this.SegmentIdentifier = "N4";
            this.City = insurance.Address.City;
            this.State = insurance.Address.State;
            this.ZipCode = insurance.Address.ZipCode;
        }

        public N4(Provider billingProvider)
        {
            this.SegmentIdentifier = "N4";
            this.City = billingProvider.Address.City;
            this.State = billingProvider.Address.State;
            this.ZipCode = billingProvider.Address.ZipCode;
        }

        private string City { get; }
        private string State { get; }
        private string ZipCode { get; }

        public string BuildN4()
        {
            var buildN4 = new StringBuilder();

            buildN4.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.City)
                .Append(this.DataElementTerminator)
                .Append(this.State)
                .Append(this.DataElementTerminator)
                .Append(this.ZipCode)
                .Append(this.SegmentTerminator);

            return buildN4.ToString();
        }
    }
}