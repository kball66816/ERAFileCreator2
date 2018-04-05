using Common.Common.Services;
using PatientManagement.Model;
using PatientManagement.ViewModel.Service.Messaging;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class PrimaryChargeViewModel : INotifyPropertyChanged
    {
        public PrimaryChargeViewModel()
        {
            this.SelectedCharge = ChargeService.GetNewCharge();
            this.PlacesOfService = this._selectedCharge.PlaceOfService.PlacesOfService;
            Messenger.Default.Register<Adjustment>(this, this.OnAdjustmentReceived, "PrimaryAdjustment");
            Messenger.Default.Register<AddonCharge>(this, this.OnAddonReceived);
            this.AddChargeToPatientCommand = new Command(this.AddNewCharge, this.CanAddChargeToPatient);
        }

        private void OnAddonReceived(AddonCharge addon)
        {
            ChargeService.AssociateAddonWithCharge(this.SelectedCharge, addon);
        }

        private void OnAdjustmentReceived(Adjustment adjustment)
        {
            ChargeService.AssociateAdjustmentWithCharge(this.SelectedCharge, adjustment);
        }

        private PrimaryCharge _selectedCharge;

        public ICommand AddChargeToPatientCommand { get; set; }

        public PrimaryCharge SelectedCharge
        {
            get => this._selectedCharge;
            set
            {
                if (value == this._selectedCharge) return;
                this._selectedCharge = value;
                this.RaisePropertyChanged("SelectedCharge");
            }
        }

        public Dictionary<string, string> PlacesOfService { get; set; }

        public string TextConfirmed { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void AddNewCharge(object obj)
        {
            Messenger.Default.Send(this.SelectedCharge);
            this.SelectedCharge = ChargeService.SetNewOrClonedChargeByUserSettings(this.SelectedCharge);
            this.RaisePropertyChanged("SelectedCharge");
            this.RaisePropertyChanged("Count");
            Messenger.Default.Send(new UpdateCalculations());
        }

        private bool CanAddChargeToPatient(object obj)
        {
            return this.SelectedCharge.ChargeCost != 0 && !string.IsNullOrEmpty(this.SelectedCharge.ProcedureCode);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}