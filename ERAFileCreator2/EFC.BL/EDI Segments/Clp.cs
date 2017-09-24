using System.Text;
using PatientManagement.Model;

namespace EFC.BL.EDI_Segments
{
    class Clp
    {
        public string BuildClp(Patient patient, PrimaryCharge charge)
        {
            var buildClp = new StringBuilder();
            buildClp.Append("CLP*");
            buildClp.Append(patient.FormattedBillId+ "*"); //CLP01 Claim Submitter Identifier
            buildClp.Append("1" + "*");//CLP02 Claim Status Code 
            buildClp.Append(charge.SumOfChargeCost+ "*"); //CLP03 Total Claim Charges Amount
            buildClp.Append(charge.SumOfChargePaid + "*");//CLP04 Total Claim Payment Amount

            if(charge.Copay!=0)
            {
                buildClp.Append(charge.Copay + "*");//CLP05 Patient Responsibility Amount
            }
            buildClp.Append("12" + "*");//CLP06 Claim Filing Indicator Code
            buildClp.Append("EMC5841338" + "*");//CLP07 Payer Claim Control Number
            buildClp.Append(charge.PlaceOfService.ServiceLocation);//CLP08 Facility Type Code
                                  //CLP09 CLaim Frequency Code
                                  //CLP10 Patient Status Code
                                  //CLP11 Diagnosis Related Group (DRG) Code
                                  //CLP12 DRG Weight
                                  //CLP13 Percent Discharge Fraction
            buildClp.Append("~");

            return buildClp.ToString();
        }
    }
}
