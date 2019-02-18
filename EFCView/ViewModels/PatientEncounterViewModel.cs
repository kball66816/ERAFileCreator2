using System.Collections.Generic;
using System.Windows.Input;
using Common.Common.Services;
using EraFileCreator.Service.Messaging;
using EraFileCreator.Services;
using PatientManagement.DAL;

namespace EraFileCreator.ViewModels
{
    public class PatientEncounterViewModel : BaseViewModel
    {
        private ServiceDescription _additionalServiceDescription;
        private Dictionary<string, string> _claimStatusCodes;

        private ServiceDescription _primaryServiceDescription;

        public PatientEncounterViewModel()
        {
            this.PrimaryServiceDescription = ChargeService.GetNewCharge();
            this.AdditionalServiceDescription = ChargeService.GetNewCharge();
            this.PlacesOfService = this._primaryServiceDescription.PlaceOfService.PlacesOfService;
            this.ClaimStatusCodes = this.PrimaryServiceDescription.ClaimStatus.Codes;
            Messenger.Default.Register<Adjustment>(this, this.OnPrimaryAdjustmentReceived, "PrimaryAdjustment");
            Messenger.Default.Register<Adjustment>(this, this.OnAdditionalAdjustmentReceived,
                "AdditionalServiceDescriptionAdjustment");
            this.AddChargeToPatientCommand = new Command(this.AddNewCharge, CanAddChargeToPatient);
            this.AddAddonCommand = new Command(this.AddAdditionalServiceDescription, CanAddChargeToPatient);
        }

        public Dictionary<string, string> ClaimStatusCodes
        {
            get => this._claimStatusCodes;
            set
            {
                if (value != this._claimStatusCodes)
                {
                    this._claimStatusCodes = value;
                    this.RaisePropertyChanged("ClaimStatusCodes");
                }
            }
        }

        public ICommand AddChargeToPatientCommand { get; set; }

        public ICommand AddAddonCommand { get; }

        public Dictionary<string, string> PlacesOfService { get; set; }

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

        private void OnAdditionalAdjustmentReceived(Adjustment adjustment)
        {
            ChargeService.AssociateAdjustmentWithCharge(this.AdditionalServiceDescription, adjustment);
        }

        private void OnPrimaryAdjustmentReceived(Adjustment adjustment)
        {
            ChargeService.AssociateAdjustmentWithCharge(this.PrimaryServiceDescription, adjustment);
        }

        private void AddAdditionalServiceDescription(object description)
        {
            ChargeService.AssociateAdditionalServiceDescription(this.PrimaryServiceDescription,
                description as ServiceDescription);
            this.AdditionalServiceDescription =
                ChargeService.SetNewOrClonedChargeByUserSettings(this.AdditionalServiceDescription);
            this.RaisePropertyChanged("SelectedAddonCharge");
            this.RaisePropertyChanged("CheckAmount");
            Messenger.Default.Send(new UpdateCalculations());
        }

        private void AddNewCharge(object description)
        {
            Messenger.Default.Send(description as ServiceDescription);
            this.PrimaryServiceDescription =
                ChargeService.SetNewOrClonedChargeByUserSettings(description as ServiceDescription);
            this.AdditionalServiceDescription = ChargeService.GetNewCharge();
            this.RaisePropertyChanged("PrimaryServiceDescription");
            this.RaisePropertyChanged("AdditionalServiceDescription");
            Messenger.Default.Send(new UpdateCalculations());
        }

        private static bool CanAddChargeToPatient(object obj)
        {
            var canAdd = false;
            if (obj is ServiceDescription s)
                canAdd = s.ChargeCost != 0
                         && s.UnitCount > 0
                         && !string.IsNullOrEmpty(s.ProcedureCode);

            return canAdd;
        }
    }
}