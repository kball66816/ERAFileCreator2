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
          
        }

        public override decimal AllowedAmount
        {
            get { return PaymentAmount; }
        }
    }
}
