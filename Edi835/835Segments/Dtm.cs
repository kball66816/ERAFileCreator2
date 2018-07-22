using System;
using System.Linq;
using System.Text;
using Common.Common.Extensions;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class Dtm : SegmentBase
    {
        private StringBuilder buildDtm;

        public Dtm()
        {
            this.SegmentIdentifier = "DTM";
            this.Date = DateTime.Today.DateToYearFirstShortString();
            this.DateTimeQualifier = "405";
        }

        public Dtm(Patient patient)
        {
            this.Charge = patient.Charges.FirstOrDefault();
            this.SegmentIdentifier = "DTM";
            this.ServiceStartDateDetails();
        }

        public Dtm(ServiceDescription charge)
        {
            this.Charge = charge;
            this.SegmentIdentifier = "DTM";
            this.DateTimeQualifier = "472";
            this.Date = charge.DateOfService.DateToYearFirstShortString();
        }

        private ServiceDescription Charge { get; }
        private string DateTimeQualifier { get; set; }
        private string Date { get; set; }


        public string BuildDtm()
        {
            this.buildDtm = new StringBuilder();

            this.buildDtm.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.DateTimeQualifier)
                .Append(this.DataElementTerminator)
                .Append(this.Date)
                .Append(this.SegmentTerminator);
            if (this.DateTimeQualifier == "232")
            {
                this.AttachServiceExpirationDate();
                this.AttachClaimReceivedDateDetails();
            }

            return this.buildDtm.ToString();
        }


        private void ServiceStartDateDetails()
        {
            this.DateTimeQualifier = "232";
            this.Date = this.Charge.DateOfService.DateToYearFirstShortString();
        }

        private void ServiceExpirationDateDetails()
        {
            this.DateTimeQualifier = "233";
            this.Date = this.Charge.DateOfService.DateToYearFirstShortString();
        }


        private void AttachServiceExpirationDate()
        {
            this.ServiceExpirationDateDetails();
            this.buildDtm.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.DateTimeQualifier)
                .Append(this.DataElementTerminator)
                .Append(this.Date)
                .Append(this.SegmentTerminator);
        }


        private void ClaimReceivedDateDetails()
        {
            this.DateTimeQualifier = "050";
            this.Date = this.Charge.DateOfService.DateToYearFirstShortString();
        }

        private void AttachClaimReceivedDateDetails()
        {
            this.ClaimReceivedDateDetails();
            this.buildDtm.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.DateTimeQualifier)
                .Append(this.DataElementTerminator)
                .Append(this.Date)
                .Append(this.SegmentTerminator);
        }
    }
}