using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagement.Model
{
    [Serializable]
    public class PrimaryCharge : Charge
    {
        public PrimaryCharge()
        {
            Id = Guid.NewGuid();
            AddonChargeList = new List<AddonCharge>();
            AdjustmentList = new List<Adjustment>();
            Modifier = new Modifier();
            DateOfService = DateTime.Today;
            PlaceOfService = new PlaceOfService();
        }

        public PrimaryCharge(PrimaryCharge charge)
        {
            ProcedureCode = charge.ProcedureCode;
            ChargeCost = charge.ChargeCost;
            PaymentAmount = charge.PaymentAmount;
            PlaceOfService = new PlaceOfService(charge.PlaceOfService);
            Modifier = new Modifier(charge.Modifier);
            DateOfService = DateTime.Today;

            AddonChargeList = new List<AddonCharge>();
            AdjustmentList = new List<Adjustment>();
            Id = Guid.NewGuid();
        }


        public PlaceOfService PlaceOfService { get; set; }

        public DateTime DateOfService { get; set; }

        public List<AddonCharge> AddonChargeList { get; set; }

        private decimal TotalCostofAddonCharge
        {
            get
            {
                decimal totalCostOfAddon = AddonChargeList.Sum(addon => addon.ChargeCost);
                return totalCostOfAddon;
            }
        }

        private decimal TotalAddonChargesPaid
        {
            get
            {
                decimal totalChargesPaid = AddonChargeList.Sum(addon => addon.PaymentAmount);
                return totalChargesPaid;
            }
        }

        
        public decimal SumOfChargePaid
        {
            get
            {
                decimal total = ChargeCost+ TotalAddonChargesPaid;
                return total;
            }
        }

        public decimal SumOfChargeCost
        {
            get
            {
                decimal totalCost = ChargeCost+TotalCostofAddonCharge;
                return totalCost;
            }
        }


    }
}
