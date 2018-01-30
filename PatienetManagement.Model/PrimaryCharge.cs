using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PatientManagement.Model
{
    [Serializable]
    public class PrimaryCharge : Charge
    {
        private string billId;
        private decimal copay;
        private DateTime dateOfService;
        private bool formatClassicBillId;
        private Guid patientId;

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
            FormatClassicBillId = charge.formatClassicBillId;
            BillId = charge.BillId;
            Copay = charge.Copay;
            ProcedureCode = charge.ProcedureCode;
            ChargeCost = charge.ChargeCost;
            PaymentAmount = charge.PaymentAmount;
            PlaceOfService = new PlaceOfService(charge.PlaceOfService);
            Modifier = new Modifier(charge.Modifier);
            DateOfService = charge.DateOfService;

            AddonChargeList = new ObservableCollection<AddonCharge>();
            AdjustmentList = new ObservableCollection<Adjustment>();
            Id = Guid.NewGuid();
        }


        public PlaceOfService PlaceOfService { get; set; }

        public DateTime DateOfService
        {
            get => dateOfService;
            set
            {
                if (value != dateOfService)
                {
                    dateOfService = value;
                    RaisePropertyChanged("DateOfService");
                }
            }
        }

        public ObservableCollection<AddonCharge> AddonChargeList { get; set; }

        public string ReferenceId { get; set; }

        public bool FormatClassicBillId
        {
            get => formatClassicBillId;
            set
            {
                if (formatClassicBillId == value) return;
                formatClassicBillId = value;
                RaisePropertyChanged("FormatClassicBillId");
            }
        }

        public string BillId
        {
            get => billId;
            set
            {
                if (value != billId)
                {
                    billId = value;
                    RaisePropertyChanged("BillId");
                }
            }
        }


        public decimal Copay
        {
            get => copay;
            set
            {
                if (value != copay)
                {
                    copay = value;
                    RaisePropertyChanged("Copay");
                    RaisePropertyChanged("AllowedAmount");
                }
            }
        }

        public override decimal AllowedAmount => PaymentAmount + Copay;

        private decimal TotalCostofAddonCharge
        {
            get
            {
                var totalCostOfAddon = AddonChargeList.Sum(addon => addon.ChargeCost);
                return totalCostOfAddon;
            }
        }

        private decimal TotalAddonChargesPaid
        {
            get
            {
                var totalChargesPaid = AddonChargeList.Sum(addon => addon.PaymentAmount);
                return totalChargesPaid;
            }
        }


        public decimal SumOfChargePaid
        {
            get
            {
                var total = PaymentAmount + TotalAddonChargesPaid;
                return total;
            }
        }

        public decimal SumOfChargeCost
        {
            get
            {
                var totalCost = ChargeCost + TotalCostofAddonCharge;
                return totalCost;
            }
        }

        public Guid PatientId
        {
            get => patientId;
            set => patientId = value;
        }
    }
}