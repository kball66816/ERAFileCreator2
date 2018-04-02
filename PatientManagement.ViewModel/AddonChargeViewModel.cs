using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using PatientManagement.Model;
using PatientManagement.ViewModel.Service.Messaging;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class AddonChargeViewModel : INotifyPropertyChanged
    {
        private AddonCharge _selectedAddonCharge;

        public AddonChargeViewModel()
        {
            this.SelectedAddonCharge = AddonChargeService.GetNewAddonCharge();
            this.AddAddonCommand = new Command(this.AddAddon, this.CanAddAddon);
            Messenger.Default.Register<Adjustment>(this, this.OnAdjustmentReceived, "AddonAdjustment");
        }

        private void OnAdjustmentReceived(Adjustment adjustment)
        {
            AddonChargeService.AssociateAdjustmentToCharge(this.SelectedAddonCharge, adjustment);
        }

        public ICommand AddAddonCommand { get; }

        public AddonCharge SelectedAddonCharge
        {
            get => this._selectedAddonCharge;
            set
            {
                if (value == this._selectedAddonCharge) return;
                this._selectedAddonCharge = value;
                this.RaisePropertyChanged("Addon");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void AddAddon(object obj)
        {
            AddonChargeService.SendAddonMessage(this.SelectedAddonCharge);
            AddonChargeService.GetNewAddonSettingsBased(this.SelectedAddonCharge);
            this.RaisePropertyChanged("SelectedAddonCharge");
            this.RaisePropertyChanged("CheckAmount");
            this.RaisePropertyChanged("Count");
            Messenger.Default.Send(new UpdateCalculations());
        }

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanAddAddon(object obj)
        {
            return !string.IsNullOrEmpty(this.SelectedAddonCharge.ProcedureCode)
            && (this.SelectedAddonCharge.ChargeCost > 0);
        }
    }
}