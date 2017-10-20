using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class PrimaryAdjustmentViewModel:INotifyPropertyChanged
    {
        public PrimaryAdjustmentViewModel()
        {
            SelectedAdjustment = new Adjustment();
            PrimaryAdjustmentReasonCodes = selectedAdjustment.AdjustmentReasonCodes;
            PrimaryAdjustmentType = SelectedAdjustment.AdjustmentTypes;
            AddChargeAdjustmentCommand = new Command(AddAdjustmentToCharge, CanAddAdjustment);
            Messenger.Default.Register<ObservableCollection<Adjustment>>(this, OnAdjustmentReceived, "PrimaryChargeAdjustments");
        }

        private void OnAdjustmentReceived(ObservableCollection<Adjustment> adjustmentList)
        {
            Adjustments = adjustmentList;
        }

        private ObservableCollection<Adjustment> adjustments;

        public ObservableCollection<Adjustment> Adjustments
        {
            get { return adjustments; }
            set
            {
                if (adjustments == value) return;
                adjustments = value;
                RaisePropertyChanged("Adjustments");
            }
        }


        private Adjustment selectedAdjustment;

        public Adjustment SelectedAdjustment
        {
            get { return selectedAdjustment; }
            set
            {
                if (value == selectedAdjustment) return;
                selectedAdjustment = value;
                RaisePropertyChanged("SelectedAdjustment");
            }
        }
        public Dictionary<string, string> PrimaryAdjustmentType { get; set; }

        public Dictionary<string, string> PrimaryAdjustmentReasonCodes { get; set; }

        public ICommand AddChargeAdjustmentCommand { get; private set; }

        private bool CanAddAdjustment(object obj)
        {
            return !string.IsNullOrEmpty(SelectedAdjustment.AdjustmentReasonCode) &&
                   SelectedAdjustment.AdjustmentAmount > 0;
        }

        private void AddAdjustmentToCharge(object obj)
        {
            Messenger.Default.Send(selectedAdjustment,"PrimaryChargeAdjustment");
            SelectedAdjustment = new Adjustment();
            RaisePropertyChanged("SelectedAdjustment");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
