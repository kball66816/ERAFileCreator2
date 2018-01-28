using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class AddonAdjustmentViewModel : INotifyPropertyChanged
    {
        private readonly IAdjustmentRepository adjustmentRepository;

        private Adjustment addonAdjustment;
        private Guid currentAddonId;

        public AddonAdjustmentViewModel()
        {
            AddonAdjustment = new Adjustment();
            AddonAdjustmentReasonCodes = AddonAdjustment.AdjustmentReasonCodes;
            AddonAdjustmentType = addonAdjustment.AdjustmentTypes;
            Messenger.Default.Register<SendGuidService>(this, OnAddonIdReceived);
            AddAddonChargeAdjustmentCommand = new Command(AddAdjustment, CanAddAddonAdjustment);
            adjustmentRepository = new AdjustmentRepository();
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
            AddonAdjustment = new Adjustment {ChargeId = sent.Id};
            currentAddonId = sent.Id;
            adjustmentRepository.Add(AddonAdjustment);
        }

        private void AddAdjustment(object obj)
        {
            AddonAdjustment = new Adjustment {ChargeId = currentAddonId};
            adjustmentRepository.Add(AddonAdjustment);
        }

        private bool CanAddAddonAdjustment(object obj)
        {
            return !string.IsNullOrEmpty(addonAdjustment.AdjustmentReasonCode) &&
                   !string.IsNullOrEmpty(addonAdjustment.AdjustmentType);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}