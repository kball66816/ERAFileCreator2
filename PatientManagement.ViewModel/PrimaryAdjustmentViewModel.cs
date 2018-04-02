using Common.Common.Services;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class PrimaryAdjustmentViewModel : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void AddAdjustmentCommand(object obj)
        {
            Messenger.Default.Send(SelectedAdjustment, "PrimaryAdjustment");
            this.SelectedAdjustment = AdjustmentService.GetNewAdjustment();
        }

        private bool CanAddAdjustment(object obj)
        {
            return !string.IsNullOrEmpty(this.SelectedAdjustment.AdjustmentReasonCode) && this.SelectedAdjustment.AdjustmentAmount != 0;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}