using Common.Common;
using System.Text;
using PatientManagement.Model;

namespace EFC.BL.EDI_Segments
{
    public class Bpr
    {
        public string BuildBpr(InsuranceCompany insurance)
        {
            var buildBpr = new StringBuilder();

            //Begin BPR          
            buildBpr.Append("BPR" + "*");
            buildBpr.Append("I" + "*"); // BPR01 Transaction Handling Code Remittance Information Only
            buildBpr.Append(insurance.CheckAmount + "*"); // BPR02 Monetary Amount
            buildBpr.Append("C" + "*"); //BPR03 Credit/Debit Flag
            buildBpr.Append(insurance.PaymentType + "*"); //BPR04 Payment Method Code
            buildBpr.Append("CCP" + "*"); //BPR05 Payment Format Code, Cash Concentration plus addenda
            buildBpr.Append("01" + "*"); //BPR06 DFI ID Number Qualifier
            buildBpr.Append("043000096" + "*"); //BPR07 ABA Sender Transit Routing Number
            buildBpr.Append("DA" + "*"); //BPR08 Account Number Qualifier
            buildBpr.Append("0" + "*"); //BPR09 Sender Bank Account NUmber
            buildBpr.Append("5135511997" + "*"); //BPR10 Originating Company Identifier
            buildBpr.Append("01" + "*"); //BPR12 ABA Receiver Transit Routing Number
            buildBpr.Append("*");
            buildBpr.Append("GAPFILL" + "*");
            buildBpr.Append("DA" + "*"); //BPR013 Account Number Qualifier
            buildBpr.Append("0" + "*"); //BPR015 Receiver or Provider Account Number
            buildBpr.Append(insurance.CheckDate.ConvertedDate()); //BPR016 Check Issue/Effective date
            buildBpr.Append("~");

            return buildBpr.ToString();

        }
    }
}
