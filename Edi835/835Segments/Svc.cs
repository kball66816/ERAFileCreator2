using System.Text;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class Svc : SegmentBase
    {
        public Svc(ServiceDescription charge)
        {
            SegmentIdentifier = "SVC";
            CompositeMedicalProcedureIdentifier = "HC";
            ServiceId = charge.ProcedureCode;
            ChargeAmount = charge.ChargeCost;
            PaymentAmount = charge.PaymentAmount;
            RevenueCode = string.Empty;
            Quanitity = 1;
            ProcedureModifier = charge.Modifier;
        }

        private string CompositeMedicalProcedureIdentifier { get; }
        private string ProductIdQualifier { get; set; }
        private string ServiceId { get; }
        private Modifier ProcedureModifier { get; }
        private string Description { get; set; }
        private decimal ChargeAmount { get; }
        private decimal PaymentAmount { get; }
        private string RevenueCode { get; }
        private int Quanitity { get; }

        public string BuildSvc()
        {
            var svc = new StringBuilder();

            svc.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(CompositeMedicalProcedureIdentifier)
                .Append(ComponentIdentifier)
                .Append(ServiceId);

            if (ProcedureModifier != null)
            {
                AppendModifierOne(svc);
                AppendModifierTwo(svc);
                AppendModifierThree(svc);
                AppendModifierFour(svc);
            }


            svc.Append(DataElementTerminator)
                .Append(ChargeAmount)
                .Append(DataElementTerminator)
                .Append(PaymentAmount)
                .Append(DataElementTerminator)
                .Append(RevenueCode)
                .Append(DataElementTerminator)
                .Append(Quanitity)
                .Append(SegmentTerminator);

            return svc.ToString();
        }

        private void AppendModifierFour(StringBuilder svc)
        {
            if (!string.IsNullOrEmpty(ProcedureModifier.ModifierFour))
                svc.Append(ComponentIdentifier)
                    .Append(ProcedureModifier.ModifierFour);
        }

        private void AppendModifierThree(StringBuilder svc)
        {
            if (!string.IsNullOrEmpty(ProcedureModifier.ModifierThree))
                svc.Append(ComponentIdentifier)
                    .Append(ProcedureModifier.ModifierThree);
        }

        private void AppendModifierTwo(StringBuilder svc)
        {
            if (!string.IsNullOrEmpty(ProcedureModifier.ModifierTwo))
                svc.Append(ComponentIdentifier)
                    .Append(ProcedureModifier.ModifierTwo);
        }

        private void AppendModifierOne(StringBuilder svc)
        {
            if (!string.IsNullOrEmpty(ProcedureModifier.ModifierOne))
                svc.Append(ComponentIdentifier)
                    .Append(ProcedureModifier.ModifierOne);
        }
    }
}