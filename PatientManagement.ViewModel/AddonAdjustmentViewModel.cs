using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;

namespace PatientManagement.ViewModel
{
    public class AddonAdjustmentViewModel : INotifyPropertyChanged
    {

        private Adjustment _addonAdjustment;

        public AddonAdjustmentViewModel()
        {
            this.AddonAdjustment = AdjustmentService.GetNewAdjustment();
            this.AddonAdjustmentReasonCodes = this.AddonAdjustment.AdjustmentReasonCodes;
            this.AddonAdjustmentType = this._addonAdjustment.AdjustmentTypes;
            this.AddAddonChargeAdjustmentCommand = new Command(this.AddAdjustment, this.CanAddAddonAdjustment);
        }

        public ICommand AddAddonChargeAdjustmentCommand { get; }

        public Adjustment AddonAdjustment
        {
            get => this._addonAdjustment;
            set
            {
                if (value == this._addonAdjustment) return;
                this._addonAdjustment = value;
                this.RaisePropertyChanged("AddonAdjustment");
            }
        }

        public Dictionary<string, string> AddonAdjustmentType { get; set; }

        public Dictionary<string, string> AddonAdjustmentReasonCodes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void AddAdjustment(object adjustment)
        {
            Messenger.Default.Send(adjustment as Adjustment, "AdditionalServiceDescriptionAdjustment");
            this.AddonAdjustment = AdjustmentService.GetNewAdjustment();
        }

        private bool CanAddAddonAdjustment(object obj)
        {
            return !string.IsNullOrEmpty(this.AddonAdjustment.AdjustmentReasonCode) && this.AddonAdjustment.AdjustmentAmount != 0;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}