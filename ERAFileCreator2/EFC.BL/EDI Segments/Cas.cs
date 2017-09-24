using System.Collections.Generic;
using System.Text;
using PatientManagement.Model;

namespace EFC.BL.EDI_Segments
{
    class Cas
    {

        public string BuildCas(Charge charge)
        {
            var buildCas = new StringBuilder();
            {
                //Since Copay is common it has it's own
                //hardcoded implementation

                //Cas Segment Patient Adjustment
                buildCas.Append("CAS" + "*");
                buildCas.Append("PR" + "*"); //Cas01 Claim Adjustment Group Code
                buildCas.Append("3" + "*"); //Cas02 Adjustment Reason Code
                buildCas.Append(charge.Copay); //Cas03 Adjustment Quantity
                buildCas.Append("~");
            }
            return buildCas.ToString();
        }

        /// <summary>
        /// Cas should be supressed if the dollar amount is 0
        /// </summary>
        /// <param name="adjustmentList"></param>
        /// <returns></returns>
        public string BuildCas(List<Adjustment> adjustmentList)
        {
            var buildCas = new StringBuilder();
            foreach (Adjustment adjustment in adjustmentList)
            {
                if(adjustment.AdjustmentAmount!=0)
                {
                    buildCas.Append("CAS" + "*");
                    buildCas.Append(adjustment.AdjustmentType + "*"); //CAS01 Claim Adjustment Group Code
                    buildCas.Append(adjustment.AdjustmentReasonCode + "*"); //Cas02 Adjustment Reason Code
                    buildCas.Append(adjustment.AdjustmentAmount); //Cas03 Adjustment Quantity
                    buildCas.Append("~");
                }

            }
            return buildCas.ToString();

            //Cas05-19 Repeat of reason code, Amount, and Quantity up to 5 times
        }
    }
}
