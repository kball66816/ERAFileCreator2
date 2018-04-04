using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PatientManagement.Model
{
    [Serializable]
    public class PrimaryCharge : Charge
    {
        private string _billId;
        private decimal _copay;
        private DateTime _dateOfService;
        private bool _formatClassicBillId;
        private Guid _patientId;

        public PrimaryCharge()
        {
            this.Id = Guid.NewGuid();
            this.AddonCharges = new ObservableCollection<AddonCharge>();
            this.Adjustments = new ObservableCollection<Adjustment>();
            this.Modifier = new Modifier();
            this.DateOfService = DateTime.Today;
            this.PlaceOfService = new PlaceOfService();
        }

        public PrimaryCharge(PrimaryCharge charge)
        {
            this.FormatClassicBillId = charge._formatClassicBillId;
            this.BillId = charge.BillId;
            this.Copay = charge.Copay;
            this.ProcedureCode = charge.ProcedureCode;
            this.ChargeCost = charge.ChargeCost;
            this.PaymentAmount = charge.PaymentAmount;
            this.PlaceOfService = new PlaceOfService(charge.PlaceOfService);
            this.Modifier = new Modifier(charge.Modifier);
            this.DateOfService = charge.DateOfService;

            this.AddonCharges = new ObservableCollection<AddonCharge>();
            this.Adjustments = new ObservableCollection<Adjustment>();
            this.Id = Guid.NewGuid();
        }


        public PlaceOfService PlaceOfService { get; set; }

        public DateTime DateOfService
        {
            get => this._dateOfService;
            set
            {
                if (value != this._dateOfService)
                {
                    this._dateOfService = value;
                    this.RaisePropertyChanged("DateOfService");
                }
            }
        }

        public ObservableCollection<AddonCharge> AddonCharges { get; set; }

        public string ReferenceId { get; set; }

        public bool FormatClassicBillId
        {
            get => this._formatClassicBillId;
            set
            {
                if (this._formatClassicBillId == value) return;
                this._formatClassicBillId = value;
                this.RaisePropertyChanged("FormatClassicBillId");
            }
        }

        public string BillId
        {
            get => this._billId;
            set
            {
                if (value != this._billId)
                {
                    this._billId = value;
                    this.RaisePropertyChanged("BillId");
                }
            }
        }


        public decimal Copay
        {
            get => this._copay;
            set
            {
                if (value != this._copay)
                {
                    this._copay = value;
                    this.RaisePropertyChanged("Copay");
                    this.RaisePropertyChanged("AllowedAmount");
                }
            }
        }

        public override decimal AllowedAmount => this.PaymentAmount + this.Copay;

        private decimal TotalCostofAddonCharge
        {
            get
            {
                var totalCostOfAddon = this.AddonCharges.Sum(addon => addon.ChargeCost);
                return totalCostOfAddon;
            }
        }

        private decimal TotalAddonChargesPaid
        {
            get
            {
                var totalChargesPaid = this.AddonCharges.Sum(addon => addon.PaymentAmount);
                return totalChargesPaid;
            }
        }


        public decimal SumOfChargePaid
        {
            get
            {
                var total = this.PaymentAmount + this.TotalAddonChargesPaid;
                return total;
            }
        }

        public decimal SumOfChargeCost
        {
            get
            {
                var totalCost = this.ChargeCost + this.TotalCostofAddonCharge;
                return totalCost;
            }
        }

        public Guid PatientId
        {
            get => this._patientId;
            set => this._patientId = value;
        }
    }
}