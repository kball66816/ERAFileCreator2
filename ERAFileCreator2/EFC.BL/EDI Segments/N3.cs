using System.Text;
using PatientManagement.Model;

namespace EFC.BL.EDI_Segments
{
    class N3
    {
        public string BuildNThree(InsuranceCompany insurance)
        {
            var buildNThree = new StringBuilder();

            buildNThree.Append("N3" + "*");
            buildNThree.Append(insurance.Address.StreetOne+"*"); //N301 Payer Address Line
            buildNThree.Append(insurance.Address.StreetTwo);
            buildNThree.Append("~");

            return buildNThree.ToString();
        }

        public string BuildNThree(Provider billingProvider)
        {
            var buildNThree = new StringBuilder();

            buildNThree.Append("N3" + "*");
            buildNThree.Append(billingProvider.Address.StreetOne + "*"); //N301 Payee Address Line
            buildNThree.Append(billingProvider.Address.StreetTwo + "*"); //N302 Payee Address Line
            buildNThree.Append("~");

            return buildNThree.ToString();
        }


    }
}
