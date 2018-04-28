using System;
using System.Linq;
using System.Text;
using Common.Common.Extensions;
using PatientManagement.Model;

namespace Edi835._835Segments
{
    public class Dtm : SegmentBase
    {
        private StringBuilder buildDtm;

        public Dtm()
        {
            SegmentIdentifier = "DTM";
            Date = DateTime.Today.DateToYearFirstShortString();
            DateTimeQualifier = "405";
        }

        public Dtm(Patient patient)
        {
            Charge = patient.Charges.FirstOrDefault();
            SegmentIdentifier = "DTM";
            ServiceStartDateDetails();
        }

        public Dtm(ServiceDescription charge)
        {
            Charge = charge;
            SegmentIdentifier = "DTM";
            DateTimeQualifier = "472";
            Date = charge.DateOfService.DateToYearFirstShortString();
        }

        private ServiceDescription Charge { get; }
        private string DateTimeQualifier { get; set; }
        private string Date { get; set; }


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