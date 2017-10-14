using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PatientManagement.Model
{
    [Serializable]
    public class PrimaryCharge : Charge
    {
        public PrimaryCharge()
        {
            Id = Guid.NewGuid();
            AddonChargeList = new ObservableCollection<AddonCharge>();
            AdjustmentList = new ObservableCollection<Adjustment>();
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

            AddonChargeList = new ObservableCollection<AddonCharge>();
            AdjustmentList = new ObservableCollection<Adjustment>();
            Id = Guid.NewGuid();
        }


        public PlaceOfService PlaceOfService { get; set; }

        public DateTime DateOfService { get; set; }

        public ObservableCollection<AddonCharge> AddonChargeList { get; set; }

        private string billId;

        public string BillId
        {
            get { return billId; }
            set
            {
                if (value != billId)
                {
                    billId = value;
                    RaisePropertyChanged("BillId");
                }
            }
        }
        private decimal copay;


        public decimal Copay
        {
            get { return copay; }
            set
            {
                if (value != copay)
                {
                    copay = value;
                    RaisePropertyChanged("Copay");
                }

            }
        }

        public override decimal AllowedAmount
        {
            get
            {
                return PaymentAmount+Copay;
            }
        }

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
                decimal total = PaymentAmount+ TotalAddonChargesPaid;
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
