using System.Text;
using PatientManagement.Model;

namespace EFC.BL.EDI_Segments
{
    class N4
    {
        public string BuildN4(InsuranceCompany insurance)
        {
            var buildN4 = new StringBuilder();

           buildN4.Append("N4" + "*");
           buildN4.Append(insurance.Address.City + "*"); //N401 Payer City Line
           buildN4.Append(insurance.Address.State + "*");     //N402 Payer City Name
           buildN4.Append(insurance.Address.ZipCode); // N403 Payer City Zip
           buildN4.Append("~");

            return buildN4.ToString();
        }
        public string BuildN4(Provider billingProvider)
        {
            var buildNFour = new StringBuilder();

            buildNFour.Append("N4" + "*");
            buildNFour.Append(billingProvider.Address.City + "*"); //N401 Payee City Name
            buildNFour.Append(billingProvider.Address.State + "*"); //N402 Payee State Name
            buildNFour.Append(billingProvider.Address.ZipCode); //N403 Payee Payee Postal Zone or Zip Code
            buildNFour.Append("~");

            return buildNFour.ToString();
        }
    }
}
