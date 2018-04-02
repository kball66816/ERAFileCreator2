using System;
using System.Collections.ObjectModel;

namespace PatientManagement.Model
{
    [Serializable]
    public class AddonCharge : Charge, ICloneable
    {
        private Guid primaryChargeId;

        public AddonCharge()
        {
            Id = Guid.NewGuid();
            Adjustments = new ObservableCollection<Adjustment>();
            Modifier = new Modifier();
        }

        public AddonCharge(AddonCharge addon)
        {
            Id = Guid.NewGuid();
            Adjustments = new ObservableCollection<Adjustment>();
            Modifier = new Modifier(addon.Modifier);
            ChargeCost = addon.ChargeCost;
            PaymentAmount = addon.PaymentAmount;
            ProcedureCode = addon.ProcedureCode;
        }

        public override decimal AllowedAmount => PaymentAmount;

        public Guid PrimaryChargeId
        {
            get => primaryChargeId;
            set => primaryChargeId = value;
        }
    }
}