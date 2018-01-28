using System;
using System.Text;
using Common.Common.Extensions;
using PatientManagement.Model;

namespace Edi835.Segments
{
    public class Dtm : SegmentBase
    {
        public Dtm()
        {
            SegmentIdentifier = "DTM";
            Date = DateTime.Today.DateToYearFirstShortString();
            DateTimeQualifier = "405";
        }

        public Dtm(Patient patient, PrimaryCharge charge)
        {
            Charge = charge;
            SegmentIdentifier = "DTM";
            ServiceStartDateDetails();
        }

        public Dtm(PrimaryCharge charge)
        {
            Charge = charge;
            SegmentIdentifier = "DTM";
            DateTimeQualifier = "472";
            Date = charge.DateOfService.DateToYearFirstShortString();
        }

        private PrimaryCharge Charge { get; set; }
        private string DateTimeQualifier { get; set; }
        private string Date { get; set; }
        private StringBuilder buildDtm;


        public string BuildDtm()
        {
            buildDtm = new StringBuilder();

            buildDtm.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(DateTimeQualifier)
                .Append(DataElementTerminator)
                .Append(Date)
                .Append(SegmentTerminator);
            if (DateTimeQualifier == "232")
            {
                AttachServiceExpirationDate();
                AttachClaimReceivedDateDetails();
            }

            return buildDtm.ToString();
        }


        private void ServiceStartDateDetails()
        {
            DateTimeQualifier = "232";
            Date = Charge.DateOfService.DateToYearFirstShortString();
        }

        private void ServiceExpirationDateDetails()
        {
            DateTimeQualifier = "233";
            Date = Charge.DateOfService.DateToYearFirstShortString();
        }


        private void AttachServiceExpirationDate()
        {

            ServiceExpirationDateDetails();
            buildDtm.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(DateTimeQualifier)
                .Append(DataElementTerminator)
                .Append(Date)
                .Append(SegmentTerminator);

        }


        private void ClaimReceivedDateDetails()
        {
            DateTimeQualifier = "050";
            Date = Charge.DateOfService.DateToYearFirstShortString();
        }

        private void AttachClaimReceivedDateDetails()
        {

            ClaimReceivedDateDetails();
            buildDtm.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(DateTimeQualifier)
                .Append(DataElementTerminator)
                .Append(Date)
                .Append(SegmentTerminator);

        }



    }
}
