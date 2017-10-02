using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PatientManagement.Model
{
    [Serializable]
    public abstract class Charge : INotifyPropertyChanged
    {

        public Charge()
        {
            Id = Guid.NewGuid();
         
           
        }

        public ObservableCollection<Adjustment> AdjustmentList { get; set; }

        public Modifier Modifier { get; set; }

        public Guid Id { get;  set; }


     

        private decimal chargeCost;

        public decimal ChargeCost
        {
            get { return chargeCost; }
            set { chargeCost = value; }
        }


        private decimal paymentAmount;

        public decimal PaymentAmount
        {
            get { return paymentAmount; }
            set
            {
                if (value != paymentAmount)
                {
                    paymentAmount = value;
                    RaisePropertyChanged("PaymentAmount");
                    RaisePropertyChanged("CheckAmount");
                }

            }
        }

        public string ProcedureCode { get; set; }


        public abstract decimal AllowedAmount { get;}

        public string CountAdjustments
        {
            get { return AdjustmentList.Count.ToString(); }
        }


        public object Clone()
        {
            MemoryStream m = new MemoryStream();
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(m, this);
            m.Position = 0;

            return (Charge)b.Deserialize(m);
        }

        [field:NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
