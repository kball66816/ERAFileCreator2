using System;
using System.ComponentModel;

namespace PatientManagement.Model
{
    [Serializable]
    public class Adjustment:INotifyPropertyChanged
    {
        private string adjustmentType;

        public string AdjustmentType
        {
            get { return adjustmentType; }
            set { if (value != AdjustmentType) { adjustmentType = value; }RaisePropertyChanged("AdjustmentType"); }
        }


        private string adjustmentReasonCode;

        public string AdjustmentReasonCode
        {
            get { return adjustmentReasonCode; }
            set { if (value != adjustmentReasonCode) { adjustmentReasonCode = value; }RaisePropertyChanged("AdjustmentReasonCode"); }
        }


        public decimal AdjustmentAmount { get; set; }

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
