using System;
using System.Collections.Generic;

namespace PatientManagement.Model
{
    [Serializable]
    public class AddonCharge:Charge, ICloneable
    {
        public AddonCharge()
        {
            Id = Guid.NewGuid();
            AdjustmentList = new List<Adjustment>();
            Modifier = new Modifier();
            new Adjustment();
        }
    }
}
