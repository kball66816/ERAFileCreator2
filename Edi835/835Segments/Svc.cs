using System.Text;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class Svc : SegmentBase
    {
        public Svc(ServiceDescription charge)
        {
            this.SegmentIdentifier = "SVC";
            this.CompositeMedicalProcedureIdentifier = "HC";
            this.ServiceId = charge.ProcedureCode;
            this.ChargeAmount = charge.ChargeCost;
            this.PaymentAmount = charge.PaymentAmount;
            this.RevenueCode = string.Empty;
            this.Quanitity = 1;
            this.ProcedureModifier = charge.Modifier;
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

            svc.Append(this.SegmentIdentifier)
                .Append(this.DataElementTerminator)
                .Append(this.CompositeMedicalProcedureIdentifier)
                .Append(this.ComponentIdentifier)
                .Append(this.ServiceId);

            if (this.ProcedureModifier != null)
            {
                this.AppendModifierOne(svc);
                this.AppendModifierTwo(svc);
                this.AppendModifierThree(svc);
                this.AppendModifierFour(svc);
            }


            svc.Append(this.DataElementTerminator)
                .Append(this.ChargeAmount)
                .Append(this.DataElementTerminator)
                .Append(this.PaymentAmount)
                .Append(this.DataElementTerminator)
                .Append(this.RevenueCode)
                .Append(this.DataElementTerminator)
                .Append(this.Quanitity)
                .Append(this.SegmentTerminator);

            return svc.ToString();
        }

        private void AppendModifierFour(StringBuilder svc)
        {
            if (!string.IsNullOrEmpty(this.ProcedureModifier.ModifierFour))
                svc.Append(this.ComponentIdentifier)
                    .Append(this.ProcedureModifier.ModifierFour);
        }

        private void AppendModifierThree(StringBuilder svc)
        {
            if (!string.IsNullOrEmpty(this.ProcedureModifier.ModifierThree))
                svc.Append(this.ComponentIdentifier)
                    .Append(this.ProcedureModifier.ModifierThree);
        }

        private void AppendModifierTwo(StringBuilder svc)
        {
            if (!string.IsNullOrEmpty(this.ProcedureModifier.ModifierTwo))
                svc.Append(this.ComponentIdentifier)
                    .Append(this.ProcedureModifier.ModifierTwo);
        }

        private void AppendModifierOne(StringBuilder svc)
        {
            if (!string.IsNullOrEmpty(this.ProcedureModifier.ModifierOne))
                svc.Append(this.ComponentIdentifier)
                    .Append(this.ProcedureModifier.ModifierOne);
        }
    }
}