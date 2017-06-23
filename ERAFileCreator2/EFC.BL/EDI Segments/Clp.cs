using System.Text;

namespace EFC.BL.EDI_Segments
{
    class Clp
    {
        public string BuildClp(Patient patient)
        {
            var buildClp = new StringBuilder();
            buildClp.Append("CLP*");
            buildClp.Append(patient.FormattedBillId+ "*"); //CLP01 Claim Submitter Identifier
            buildClp.Append("1" + "*");//CLP02 Claim Status Code 
            buildClp.Append(patient.Charge.SumOfChargeCost+ "*"); //CLP03 Total Claim Charge Amount
            buildClp.Append(patient.Charge.SumOfChargePaid + "*");//CLP04 Total Claim Payment Amount

            if(patient.Charge.Copay!=0)
            {
                buildClp.Append(patient.Charge.Copay + "*");//CLP05 Patient Responsibility Amount
            }
            buildClp.Append("12" + "*");//CLP06 Claim Filing Indicator Code
            buildClp.Append("EMC5841338" + "*");//CLP07 Payer Claim Control Number
            buildClp.Append(patient.Charge.PlaceOfService.ServiceLocation);//CLP08 Facility Type Code
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
