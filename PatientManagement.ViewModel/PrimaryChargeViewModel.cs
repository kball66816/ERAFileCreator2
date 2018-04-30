using Common.Common.Services;
using PatientManagement.Model;
using PatientManagement.ViewModel.Service.Messaging;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class PrimaryChargeViewModel : INotifyPropertyChanged
    {
        public PrimaryChargeViewModel()
        {
            this.PrimaryServiceDescription = ChargeService.GetNewCharge();
            this.AdditionalServiceDescription = ChargeService.GetNewCharge();
            this.PlacesOfService = this._primaryServiceDescription.PlaceOfService.PlacesOfService;
            Messenger.Default.Register<Adjustment>(this, this.OnPrimaryAdjustmentReceived, "PrimaryAdjustment");
            Messenger.Default.Register<Adjustment>(this, this.OnAdditionalAdjustmentReceived, "AdditionalServiceDescriptionAdjustment");
            this.AddChargeToPatientCommand = new Command(this.AddNewCharge, CanAddChargeToPatient);
            this.AddAddonCommand = new Command(this.AddAdditionalServiceDescription, CanAddChargeToPatient);
        }

        private void OnAdditionalAdjustmentReceived(Adjustment adjustment)
        {
            ChargeService.AssociateAdjustmentWithCharge(this.AdditionalServiceDescription, adjustment);
        }

        public ICommand AddChargeToPatientCommand { get; set; }

        public ICommand AddAddonCommand { get; }

        public Dictionary<string, string> PlacesOfService { get; set; }


        private void OnPrimaryAdjustmentReceived(Adjustment adjustment)
        {
            ChargeService.AssociateAdjustmentWithCharge(this.PrimaryServiceDescription, adjustment);
        }

        private ServiceDescription _primaryServiceDescription;
        private ServiceDescription _additionalServiceDescription;

        public ServiceDescription PrimaryServiceDescription
        {
            get => this._primaryServiceDescription;
            set
            {
                if (value == this._primaryServiceDescription) return;
                this._primaryServiceDescription = value;
                this.RaisePropertyChanged("PrimaryServiceDescription");
            }
        }

        public ServiceDescription AdditionalServiceDescription
        {
            get => this._additionalServiceDescription;
            set
            {
                if (value == this._additionalServiceDescription) return;
                this._additionalServiceDescription = value;
                this.RaisePropertyChanged("AdditionalServiceDescription");
            }
        }

        private void AddAdditionalServiceDescription(object description)
        {
            ChargeService.AssociateAdditionalServiceDescription(this.PrimaryServiceDescription, description as  ServiceDescription);
            this.AdditionalServiceDescription =
                ChargeService.SetNewOrClonedChargeByUserSettings(AdditionalServiceDescription);
            this.RaisePropertyChanged("SelectedAddonCharge");
            this.RaisePropertyChanged("CheckAmount");
            Messenger.Default.Send(new UpdateCalculations());
        }

        private void AddNewCharge(object description)
        {
            Messenger.Default.Send(description as ServiceDescription);
            this.PrimaryServiceDescription = ChargeService.SetNewOrClonedChargeByUserSettings(description as ServiceDescription);
            this.AdditionalServiceDescription = ChargeService.GetNewCharge();
            this.RaisePropertyChanged("PrimaryServiceDescription");
            this.RaisePropertyChanged("AdditionalServiceDescription");
            Messenger.Default.Send(new UpdateCalculations());
        }

        private static bool CanAddChargeToPatient(object obj)
        {
            var canAdd = false;
            if (obj is ServiceDescription s)
            {
                canAdd = s.ChargeCost != 0 && !string.IsNullOrEmpty(s.ProcedureCode);
            }

            return canAdd;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}