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

        public override decimal AllowedAmount
        {
            get { return PaymentAmount; }
        }
    }
}
