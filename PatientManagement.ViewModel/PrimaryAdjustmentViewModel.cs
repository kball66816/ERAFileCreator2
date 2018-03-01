using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class PrimaryAdjustmentViewModel : INotifyPropertyChanged
    {
        private Guid currentChargeGuid;

        private bool initializationComplete;

        private Adjustment selectedAdjustment;

        public PrimaryAdjustmentViewModel()
        {
            SelectedAdjustment = AdjustmentService.GetNewAdjustment();
            PrimaryAdjustmentReasonCodes = selectedAdjustment.AdjustmentReasonCodes;
            PrimaryAdjustmentType = SelectedAdjustment.AdjustmentTypes;
            AddChargeAdjustmentCommand = new Command(AddAdjustmentCommand, CanAddAdjustment);
            Messenger.Default.Register<SendGuidService>(this, OnChargeIdReceived, "ChargeIdSent");
        }

        public Adjustment SelectedAdjustment
        {
            get => selectedAdjustment;
            set
            {
                if (value == selectedAdjustment) return;
                selectedAdjustment = value;
                RaisePropertyChanged("SelectedAdjustment");
            }
        }

        public Dictionary<string, string> PrimaryAdjustmentType { get; set; }

        public Dictionary<string, string> PrimaryAdjustmentReasonCodes { get; set; }

        public ICommand AddChargeAdjustmentCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChargeIdReceived(SendGuidService sent)
        {
            if (initializationComplete)
            {
                SelectedAdjustment = AdjustmentService.GetNewAdjustment();
            }
            initializationComplete = true;
            AdjustmentService.AssociateChargeId(SelectedAdjustment, sent.Id);
            currentChargeGuid = sent.Id;
            AdjustmentService.Add(SelectedAdjustment);
        }

        private void AddAdjustmentCommand(object obj)
        {
            SelectedAdjustment = AdjustmentService.GetNewAdjustment();
            AdjustmentService.AssociateChargeId(SelectedAdjustment, currentChargeGuid);
            AdjustmentService.Add(SelectedAdjustment);
        }

        private bool CanAddAdjustment(object obj)
        {
            return !string.IsNullOrEmpty(SelectedAdjustment.AdjustmentReasonCode) &&
                   SelectedAdjustment.AdjustmentAmount > 0;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}