using System.Text;

namespace EFC.BL.EDI_Segments
{
    class Svc
    {
        public string BuildSvc(Charge charge)
        {
            var buildSvc = new StringBuilder();

            buildSvc.Append("SVC" + "*");
            buildSvc.Append("HC" + ":"); //SVC01-1 Service Type Code
            buildSvc.Append(charge.ProcedureCode); //SVC01-2 Service Code

            //SVC 01-03 through 01-07 contingent on modifiers and description
            if (!string.IsNullOrEmpty(charge.Modifier.ModifierOne))
            {
                buildSvc.Append(":");
                buildSvc.Append(charge.Modifier.ModifierOne);
            }
            if (!string.IsNullOrEmpty(charge.Modifier.ModifierTwo))
            {
                buildSvc.Append(":");
                buildSvc.Append(charge.Modifier.ModifierTwo);
            }
            if (!string.IsNullOrEmpty(charge.Modifier.ModifierThree))
            {
                buildSvc.Append(":");
                buildSvc.Append(charge.Modifier.ModifierThree);
            }
            if (!string.IsNullOrEmpty(charge.Modifier.ModifierFour))
            {
                buildSvc.Append(":");
                buildSvc.Append(charge.Modifier.ModifierFour);
            }

            //svc0107 Procedure Code Description
            buildSvc.Append("*");
            buildSvc.Append(charge.ChargeCost + "*"); //SVC02 Monetary Amount
            buildSvc.Append(charge.PaymentAmount + "*"); //SVC03 Monetary Amount
            buildSvc.Append("*"); //SVC04 NUBC Revenue Code
            buildSvc.Append("1"); //SVC05 Units of Service Paid Count 
            buildSvc.Append("~");
            return buildSvc.ToString();
        }
        public string BuildSvc(AddonCharge addon)
        {
            var buildSvc = new StringBuilder();

            buildSvc.Append("SVC" + "*");
            buildSvc.Append("HC" + ":");
            buildSvc.Append(addon.ProcedureCode);
            if (!string.IsNullOrEmpty(addon.Modifier.ModifierOne))
            {
                buildSvc.Append(":");
                buildSvc.Append(addon.Modifier.ModifierOne);
            }
            if (!string.IsNullOrEmpty(addon.Modifier.ModifierTwo))
            {
                buildSvc.Append(":");
                buildSvc.Append(addon.Modifier.ModifierTwo);
            }
            if (!string.IsNullOrEmpty(addon.Modifier.ModifierThree))
            {
                buildSvc.Append(":");
                buildSvc.Append(addon.Modifier.ModifierThree);
            }
            if (!string.IsNullOrEmpty(addon.Modifier.ModifierFour))
            {
                buildSvc.Append(":");
                buildSvc.Append(addon.Modifier.ModifierFour);
            }
            buildSvc.Append("*");
            buildSvc.Append(addon.ChargeCost + "*");
            buildSvc.Append(addon.PaymentAmount + "*");
            buildSvc.Append("*");//SVC04 NUBC Revenue Code
            buildSvc.Append("1"); //SV05 Unit of Service Paid Count
            buildSvc.Append("~");
            return buildSvc.ToString();
        }
    }
}
