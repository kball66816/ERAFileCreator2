using Common.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PatientManagement.Model;

namespace EFC.BL.EDI_Segments
{
    class Dtm
    {
        DateConversion dateConversion = new DateConversion();

        public string BuildDtm(List<Patient> retrievePatients)
        {
            var buildDtm = new StringBuilder();

            buildDtm.Append("DTM" + "*");
            buildDtm.Append("405" + "*");

            var dateConversion = new DateConversion();
            buildDtm.Append(dateConversion.ConvertedDate(retrievePatients.First().Charge.DateOfService));
            buildDtm.Append("~");

            return buildDtm.ToString();
        }
            
        public string BuildDtm(Patient patient)
        {
            var buildDtm = new StringBuilder();
            buildDtm.Append("DTM*");
            buildDtm.Append("232*"); //DTM01 Date Time Qualifier
            buildDtm.Append(dateConversion.ConvertedDate(patient.Charge.DateOfService)); //DTM02 Start Date
            buildDtm.Append("~");

            //DTM Coverage Expiration Date 2100
            buildDtm.Append("DTM*");
            buildDtm.Append("233*"); //DTM01 Date Time Qualifier
            buildDtm.Append(dateConversion.ConvertedDate(patient.Charge.DateOfService)); //DTM02 Expiration Date
            buildDtm.Append("~");

            //DTM Claim Received Date
           buildDtm.Append("DTM*");
           buildDtm.Append("050*"); //DTM01 Date Time Qualifier
           buildDtm.Append(dateConversion.ConvertedDate(patient.Charge.DateOfService)); //DTM02 Date of Service Date
           buildDtm.Append("~");

            return buildDtm.ToString();
        }
        public string BuildDtm(PrimaryCharge charge)
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
            buildDtm.Append(dateConversion.ConvertedDate(charge.DateOfService)); //DTM02 Service Date
            buildDtm.Append("~");

            return buildDtm.ToString();

        }
    }
}
