using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class AddonAdjustmentViewModel : INotifyPropertyChanged
    {

        private Adjustment addonAdjustment;
        private Guid currentAddonId;

        public AddonAdjustmentViewModel()
        {
            AddonAdjustment = AdjustmentService.GetNewAdjustment();
            AddonAdjustmentReasonCodes = AddonAdjustment.AdjustmentReasonCodes;
            AddonAdjustmentType = addonAdjustment.AdjustmentTypes;
            Messenger.Default.Register<SendGuidService>(this, OnAddonIdReceived);
            AddAddonChargeAdjustmentCommand = new Command(AddAdjustment, CanAddAddonAdjustment);

        }

        public ICommand AddAddonChargeAdjustmentCommand { get; }

        public Adjustment AddonAdjustment
        {
            get => addonAdjustment;
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

        private void OnAddonIdReceived(SendGuidService sent)
        {
            AddonAdjustment = AdjustmentService.GetNewAdjustment();
            AdjustmentService.AssociateChargeId(AddonAdjustment, sent.Id);
            currentAddonId = sent.Id;
            AdjustmentService.Add(AddonAdjustment);
        }

        private void AddAdjustment(object obj)
        {
            AddonAdjustment = AdjustmentService.GetNewAdjustment();
            AdjustmentService.AssociateChargeId(AddonAdjustment, currentAddonId);
            AdjustmentService.Add(AddonAdjustment);
        }

        private bool CanAddAddonAdjustment(object obj)
        {
            return !string.IsNullOrEmpty(AddonAdjustment.AdjustmentReasonCode) &&
                   AddonAdjustment.AdjustmentAmount > 0;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}