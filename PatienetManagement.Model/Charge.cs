using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PatientManagement.Model
{
    [Serializable]
    public abstract class Charge : INotifyPropertyChanged
    {
        private ObservableCollection<Adjustment> adjustmentList;


        private decimal chargeCost;

        private decimal paymentAmount;


        private string procedureCode;

        public ObservableCollection<Adjustment> AdjustmentList
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


        public abstract decimal AllowedAmount { get; }

        public string CountAdjustments => AdjustmentList.Count.ToString();

        [field: NonSerialized] public event PropertyChangedEventHandler PropertyChanged;


        public object Clone()
        {
            var m = new MemoryStream();
            var b = new BinaryFormatter();
            b.Serialize(m, this);
            m.Position = 0;

            return (Charge) b.Deserialize(m);
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}