using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PatientManagement.Model
{
    [Serializable]
    public class AddonCharge:Charge, ICloneable
    {
        public AddonCharge()
        {
            Id = Guid.NewGuid();
            AdjustmentList = new ObservableCollection<Adjustment>();
            Modifier = new Modifier();
        }

        public AddonCharge(AddonCharge addon)
        {
            Id = Guid.NewGuid();
            AdjustmentList = new ObservableCollection<Adjustment>();
            Modifier = new Modifier(addon.Modifier);
            ChargeCost = addon.ChargeCost;
            PaymentAmount = addon.PaymentAmount;
            ProcedureCode = addon.ProcedureCode;

        }
        public override decimal AllowedAmount
        {
            get { return PaymentAmount; }
        }
    }
}
