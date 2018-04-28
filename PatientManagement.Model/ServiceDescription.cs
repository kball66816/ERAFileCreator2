using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace PatientManagement.Model
{
    public class ServiceDescription
    {
        private string _billId;
        private decimal _copay;
        private DateTime _dateOfService;

        public ServiceDescription()
        {
            this.AdditionalServiceDescriptions = new ObservableCollection<ServiceDescription>();
            this.Adjustments = new ObservableCollection<Adjustment>();
            this.Modifier = new Modifier();
            this.DateOfService = DateTime.Today;
            this.PlaceOfService = new PlaceOfService();
        }

        public ServiceDescription(ServiceDescription charge)
        {
            this.BillId = charge.BillId;
            this.Copay = charge.Copay;
            this.ProcedureCode = charge.ProcedureCode;
            this.ChargeCost = charge.ChargeCost;
            this.PaymentAmount = charge.PaymentAmount;
            this.PlaceOfService = new PlaceOfService(charge.PlaceOfService);
            this.Modifier = new Modifier(charge.Modifier);
            this.DateOfService = charge.DateOfService;
            this.AdditionalServiceDescriptions = new ObservableCollection<ServiceDescription>();
            this.Adjustments = new ObservableCollection<Adjustment>();
            this.Id = Guid.NewGuid();
        }

        private ObservableCollection<Adjustment> adjustmentList;


        private decimal chargeCost;

        private decimal paymentAmount;


        private string procedureCode;

        public ObservableCollection<Adjustment> Adjustments
        {
            get => adjustmentList;
            set
            {
                if (value != adjustmentList)
                {
                    adjustmentList = value;
                    RaisePropertyChanged("AdjustmentList");
                }
            }
        }

        public Modifier Modifier { get; set; }

        public Guid Id { get; set; }

        public decimal ChargeCost
        {
            get => chargeCost;
            set
            {
                if (value != chargeCost)
                {
                    chargeCost = value;
                    RaisePropertyChanged("ChargeCost");
                }
            }
        }

        public decimal PaymentAmount
        {
            get => paymentAmount;
            set
            {
                if (value != paymentAmount)
                {
                    paymentAmount = value;
                    RaisePropertyChanged("PaymentAmount");
                    RaisePropertyChanged("CheckAmount");
                    RaisePropertyChanged("AllowedAmount");
                }
            }
        }

        public string ProcedureCode
        {
            get => procedureCode;
            set
            {
                if (value != procedureCode)
                {
                    procedureCode = value;
                    RaisePropertyChanged("ProcedureCode");
                }
            }
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

        public ObservableCollection<ServiceDescription> AdditionalServiceDescriptions { get; }

        public string ReferenceId
        {
            get => BillId + "-" + ReferenceIdCounter;
        }

        public int ReferenceIdCounter;
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

        public decimal AllowedAmount => this.PaymentAmount + this.Copay;

        private decimal TotalCostofAddonCharge
        {
            get => this.AdditionalServiceDescriptions.Sum(addon => addon.ChargeCost);
        }

        private decimal TotalAddonChargesPaid
        {
            get => this.AdditionalServiceDescriptions.Sum(addon => addon.PaymentAmount);
        }


        public decimal SumOfChargePaid
        {
            get => this.PaymentAmount + this.TotalAddonChargesPaid;
        }

        public decimal SumOfChargeCost
        {
            get => this.ChargeCost + this.TotalCostofAddonCharge;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null) this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}