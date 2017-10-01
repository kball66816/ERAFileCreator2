using Common.Common;
using PatientManagement.Model;
using System;
using System.Text;

namespace EDI835.Segments
{
    public class Dtm : SegmentBase
    {
        public Dtm()
        {
            ProductionDate = DateTime.Today.ConvertedDate();
            DateTimeQualifier = "405";
        }

        public Dtm(Patient patient)
        {
            
            SegmentIdentifier = "DTM";
            ServiceStartDateDetails();
        }

        public Dtm(PrimaryCharge charge)
        {
            Charge = charge;
            SegmentIdentifier = "DTM";
            DateTimeQualifier = "472";
            Date = charge.DateOfService.ConvertedDate();
        }

        private PrimaryCharge Charge { get; set; }
        private string DateTimeQualifier { get; set; }
        private string ProductionDate { get; set; }
        private string ServiceDate { get; set; }
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
            Date = Charge.DateOfService.ConvertedDate();
        }

        private void ServiceExpirationDateDetails()
        {
            DateTimeQualifier = "233";
            Date = Charge.DateOfService.ConvertedDate();
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
            Date = Charge.DateOfService.ConvertedDate();
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

        private string BuildDtmProductionDate()
        {
            buildDtm = new StringBuilder();
            buildDtm.Append(SegmentIdentifier);
            buildDtm.Append(DataElementTerminator);
            buildDtm.Append(DateTimeQualifier);
            buildDtm.Append(DataElementTerminator);
            buildDtm.Append(ProductionDate);
            buildDtm.Append(SegmentTerminator);

            return buildDtm.ToString();
        }

      
      
        private string BuildDtmServiceDate()
        {
            buildDtm = new StringBuilder();
            buildDtm.Append(SegmentIdentifier);
            buildDtm.Append(DataElementTerminator);
            buildDtm.Append(DateTimeQualifier);
            buildDtm.Append(DataElementTerminator);
            buildDtm.Append(ServiceDate);
            buildDtm.Append(SegmentIdentifier);

            return buildDtm.ToString();
        }

        //public string BuildDtm(PrimaryCharge charge)
        //{
        //    //DTM Service Start Date 2110
        //    //DTM01 Date Time QUalifier
        //    //DTM02 Service Date

        //    //DTM Service End Date 2110
        //    //DTM01 Date Time Qualifier
        //    //DTM02 Service Date
        //    //DTM Service Date 2110
        //    var buildDtm = new StringBuilder();

        //    buildDtm.Append("DTM" + "*");
        //    buildDtm.Append("472" + "*"); //DTM01 Date Time Qualifier
        //    buildDtm.Append(charge.DateOfService.ConvertedDate()); //DTM02 Service Date
        //    buildDtm.Append("~");

        //    return buildDtm.ToString();

        //}
    }
}
