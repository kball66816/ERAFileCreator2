using System.Collections.Generic;
using System.Windows.Input;
using Common.Common.Services;
using EraFileCreator.Services;
using PatientManagement.DAL;

namespace EraFileCreator.ViewModels
{
    public class PrimaryAdjustmentViewModel : BaseViewModel
    {
        private Adjustment _selectedAdjustment;

        public PrimaryAdjustmentViewModel()
        {
            this.SelectedAdjustment = AdjustmentService.GetNewAdjustment();
            this.PrimaryAdjustmentReasonCodes = this._selectedAdjustment.AdjustmentReasonCodes;
            this.PrimaryAdjustmentType = this.SelectedAdjustment.AdjustmentTypes;
            this.AddChargeAdjustmentCommand = new Command(this.AddAdjustmentCommand, this.CanAddAdjustment);
        }

        public Adjustment SelectedAdjustment
        {
            get => this._selectedAdjustment;
            set
            {
                if (value == this._selectedAdjustment) return;
                this._selectedAdjustment = value;
                this.RaisePropertyChanged("SelectedAdjustment");
            }
        }

        public Dictionary<string, string> PrimaryAdjustmentType { get; set; }

        public Dictionary<string, string> PrimaryAdjustmentReasonCodes { get; set; }

        public ICommand AddChargeAdjustmentCommand { get; }

        private void AddAdjustmentCommand(object obj)
        {
            Messenger.Default.Send(this.SelectedAdjustment, "PrimaryAdjustment");
            this.SelectedAdjustment = AdjustmentService.GetNewAdjustment();
        }

        private bool CanAddAdjustment(object obj)
        {
            return !string.IsNullOrEmpty(this.SelectedAdjustment.AdjustmentReasonCode) &&
                   this.SelectedAdjustment.AdjustmentAmount != 0;
        }
    }
}