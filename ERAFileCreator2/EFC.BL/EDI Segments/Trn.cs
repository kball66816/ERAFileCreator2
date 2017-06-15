using System;
using System.Text;

namespace EFC.BL.EDI_Segments
{
    public class Trn
    {
        public string BuildTrn(InsuranceCompany insurance)
        {
            var buildTrn = new StringBuilder();

           buildTrn.Append("TRN" + "*");
           buildTrn.Append("1" + "*"); // TRN01 Trace Type Code
           buildTrn.Append(Today() + "*"); //TRN02 Reference Identification(Check Number)
           buildTrn.Append("1"+insurance.TaxId + "*"); //TRN03 Originating Company Identifier
           buildTrn.Append("13551"); //TRN04 Originating Company Supplemental Code
           buildTrn.Append("~");

            return buildTrn.ToString();
        }
        private string Today()
        {
            string today = DateTime.Now.ToString("yyyyMMddhhmmssff");
            return today;
        }
    }
}
