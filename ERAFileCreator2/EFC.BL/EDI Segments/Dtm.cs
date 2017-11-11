using Common.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Common.Converters;
using Common.Common.Extensions;
using PatientManagement.Model;

namespace EFC.BL.EDI_Segments
{
    class Dtm
    {

        public string BuildDtm(List<Patient> retrievePatients)
        {
            var dateOfService = retrievePatients.First().Charges.First().DateOfService;
            var buildDtm = new StringBuilder();

            buildDtm.Append("DTM" + "*");
            buildDtm.Append("405" + "*");
            buildDtm.Append(dateOfService.DateToYearFirstShortString());
            buildDtm.Append("~");

            return buildDtm.ToString();
        }
            
        public string BuildDtm(PrimaryCharge charge)
        {
            var buildDtm = new StringBuilder();
            buildDtm.Append("DTM*");
            buildDtm.Append("232*"); //DTM01 Date Time Qualifier
            buildDtm.Append(charge.DateOfService.DateToYearFirstShortString()); //DTM02 Start Date
            buildDtm.Append("~");

            //DTM Coverage Expiration Date 2100
            buildDtm.Append("DTM*");
            buildDtm.Append("233*"); //DTM01 Date Time Qualifier
            buildDtm.Append(charge.DateOfService.DateToYearFirstShortString()); //DTM02 Expiration Date
            buildDtm.Append("~");

            //DTM Claim Received Date
           buildDtm.Append("DTM*");
           buildDtm.Append("050*"); //DTM01 Date Time Qualifier
           buildDtm.Append(charge.DateOfService.DateToYearFirstShortString()); //DTM02 Date of Service Date
           buildDtm.Append("~");

            return buildDtm.ToString();
        }
        public string BuildDtm2(PrimaryCharge charge)
        {
            //DTM Service Start Date 2110
            //DTM01 Date Time QUalifier
            //DTM02 Service Date

            //DTM Service End Date 2110
            //DTM01 Date Time Qualifier
            //DTM02 Service Date
            //DTM Service Date 2110
            var buildDtm = new StringBuilder();

            buildDtm.Append("DTM" + "*");
            buildDtm.Append("472" + "*"); //DTM01 Date Time Qualifier
            buildDtm.Append(charge.DateOfService.DateToYearFirstShortString()); //DTM02 Service Date
            buildDtm.Append("~");

            return buildDtm.ToString();

        }
    }
}
