using System.Text;
using PatientManagement.Model;

namespace EFC.BL.EDI_Segments
{
    class Amt
    {
        public string BuildAmt(Charge charge)
        {
            var buildAmt = new StringBuilder();

            //AMT Segment Service Supplemental Amount
            buildAmt.Append("AMT" + "*");
            buildAmt.Append("B6" + "*"); //Amount Qualifier Code Allowed-Actual

            buildAmt.Append(charge.AllowedAmount);//AMT02 Service Line Allowed Amount
            buildAmt.Append("~");
            return buildAmt.ToString();
        }
    }
}
