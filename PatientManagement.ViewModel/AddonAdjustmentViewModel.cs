using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;

namespace PatientManagement.ViewModel
{
    public class AddonAdjustmentViewModel:INotifyPropertyChanged
    {
        public AddonAdjustmentViewModel()
        {
            AddonAdjustment = new Adjustment();
            AddonAdjustmentReasonCodes = AddonAdjustment.AdjustmentReasonCodes;
            AddonAdjustmentType = addonAdjustment.AdjustmentTypes;
            AddAddonChargeAdjustmentCommand = new Command(AddAddonAdjustment, CanAddAddonAdjustment);
            EditSelectedAdjustmentCommand = new Command(EditSelectedAdjustment, CanEditOrDeleteSelectedAdjustment);
            DeleteSelectedAdjustmentCommand = new Command(DeleteSelectedAdjustment, CanEditOrDeleteSelectedAdjustment);
            Messenger.Default.Register<ObservableCollection<Adjustment>>(this, OnAdjustmentCollectionReceived,"AddonAdjustmentList");
        }

        


        private void OnAdjustmentCollectionReceived(ObservableCollection<Adjustment> adjustmentList)
        {
            Adjustments = adjustmentList;
            RaisePropertyChanged("Adjustments");
        }


        public Adjustment SelectedAddonAdjustmentIndex { get; set; }

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
        public ICommand DeleteSelectedAdjustmentCommand { get; set; }

        private void DeleteSelectedAdjustment(object obj)
        {
            if (SelectedAddonAdjustmentIndex == null) return;
            var index = Adjustments.IndexOf(SelectedAddonAdjustmentIndex);
            if (index <= -1) return;
            Adjustments.RemoveAt(index);
            RaisePropertyChanged("Adjustments");
            if (!editModeEnabled) return;
            editModeEnabled = false;
        }
        public ICommand EditSelectedAdjustmentCommand { get; set; }

        private bool CanEditOrDeleteSelectedAdjustment(object obj)
        {
            bool canEditOrDelete = (!string.IsNullOrEmpty(SelectedAddonAdjustmentIndex?.AdjustmentType));
            return canEditOrDelete;
        }

        private bool editModeEnabled;
        private void EditSelectedAdjustment(object obj)
        {
            if (!editModeEnabled)
            {
                AddonAdjustment = SelectedAddonAdjustmentIndex;
                RaisePropertyChanged("AddonAdjustment");
                editModeEnabled = true;
            }

            else
            {
                addonAdjustment = new Adjustment();
                RaisePropertyChanged("AddonAdjustment");
            }
        }

        public ICommand AddAddonChargeAdjustmentCommand { get; private set; }

        private void AddAddonAdjustment(object obj)
        {
            Messenger.Default.Send(AddonAdjustment, "AddonCharge");
            AddonAdjustment = new Adjustment();
            RaisePropertyChanged("SelectedAdjustment");

        }

        private bool CanAddAddonAdjustment(object obj)
        {
            return !string.IsNullOrEmpty(addonAdjustment.AdjustmentReasonCode) &&
                   !string.IsNullOrEmpty(addonAdjustment.AdjustmentType);
        }

        private Adjustment addonAdjustment;

        public Adjustment AddonAdjustment
        {
            get { return addonAdjustment; }
            set
            {
                if (value == addonAdjustment) return;
                addonAdjustment = value;
                RaisePropertyChanged("AddonAdjustment");
            }
        }    

        public Dictionary<string, string> AddonAdjustmentType { get; set; }

        public Dictionary<string, string> AddonAdjustmentReasonCodes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
