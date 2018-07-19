using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace PatientManagement.DAL
{
    public class ServiceDescription
    {
        private string _billId;
        private decimal _copay;
        private DateTime _dateOfService;

        public ServiceDescription()
        {
            this._additionalServiceDescriptions = new ObservableCollection<ServiceDescription>();
            this.Adjustments = new ObservableCollection<Adjustment>();
            this.Modifier = new Modifier();
            this.DateOfService = DateTime.Today;
            this.PlaceOfService = new PlaceOfService();
            this.ClaimStatus = new ClaimStatusCode();
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
            this._additionalServiceDescriptions = new ObservableCollection<ServiceDescription>();
            this.Adjustments = new ObservableCollection<Adjustment>();
            this.ClaimStatus = new ClaimStatusCode(charge.ClaimStatus);
        }

        private ObservableCollection<Adjustment> _adjustmentList;


        private decimal _chargeCost;

        private decimal _paymentAmount;


        private string _procedureCode;

        public ObservableCollection<Adjustment> Adjustments
        {
            get => this._adjustmentList;
            set
            {
                if (value != this._adjustmentList)
                {
                    this._adjustmentList = value;
                    this.RaisePropertyChanged("AdjustmentList");
                }
            }
        }

        public Modifier Modifier { get; set; }

        public decimal ChargeCost
        {
            get => this._chargeCost;
            set
            {
                if (value != this._chargeCost)
                {
                    this._chargeCost = value;
                    this.RaisePropertyChanged("ChargeCost");
                }
            }
        }

        public decimal PaymentAmount
        {
            get => this._paymentAmount;
            set
            {
                if (value != this._paymentAmount)
                {
                    this._paymentAmount = value;
                    this.RaisePropertyChanged("PaymentAmount");
                    this.RaisePropertyChanged("CheckAmount");
                    this.RaisePropertyChanged("AllowedAmount");
                }
            }
        }

        public string ProcedureCode
        {
            get => this._procedureCode;
            set
            {
                if (value != this._procedureCode)
                {
                    this._procedureCode = value;
                    this.RaisePropertyChanged("ProcedureCode");
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

        public ObservableCollection<ServiceDescription> AdditionalServiceDescriptions
        {
            get
            {
                return this._additionalServiceDescriptions;
            }

            set
            {
                if (value != this._additionalServiceDescriptions)
                {
                    this._additionalServiceDescriptions = value;
                    this.RaisePropertyChanged("AdditionalServiceDescriptions");
                }
            }
        }

        public string ReferenceId
        {
            get => this.BillId + "-" + this.ReferenceIdCounter;
        }

        public int ReferenceIdCounter;
        private ObservableCollection<ServiceDescription> _additionalServiceDescriptions;

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

        public ClaimStatusCode ClaimStatus { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}