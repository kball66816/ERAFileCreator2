using System;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using EFC.BL;
using PatientManagement.DAL;

namespace PatientManagement.ViewModel
{
    public class AddonAdjustmentViewModel:INotifyPropertyChanged
    {
        public AddonAdjustmentViewModel()
        {
            AddonAdjustment = new Adjustment();
            AddonAdjustmentReasonCodes = AddonAdjustment.AdjustmentReasonCodes;
            AddonAdjustmentType = addonAdjustment.AdjustmentTypes;
            Messenger.Default.Register<SendGuidService>(this,OnAddonIdReceived);
            AddAddonChargeAdjustmentCommand = new Command(AddAdjustment, CanAddAddonAdjustment);
            adjustmentRepository = new AdjustmentRepository();
        }

        private readonly IAdjustmentRepository adjustmentRepository;
        private Guid currentAddonId;

        private void OnAddonIdReceived(SendGuidService sent)
        {
            AddonAdjustment.ChargeId = sent.Id;
            currentAddonId = sent.Id;
            adjustmentRepository.Add(AddonAdjustment);
        }

        private void AddAdjustment(object obj)
        {
            AddonAdjustment = new Adjustment();
            AddonAdjustment.ChargeId = currentAddonId;
            adjustmentRepository.Add(AddonAdjustment);
        }

        public ICommand AddAddonChargeAdjustmentCommand { get; private set; }

        private bool CanAddAddonAdjustment(object obj)
        {
            return !string.IsNullOrEmpty(addonAdjustment.AdjustmentReasonCode) &&
                   !string.IsNullOrEmpty(addonAdjustment.AdjustmentType);
        }

        private Adjustment addonAdjustment;

        public Adjustment AddonAdjustment
        {
            get { return addonAdjustment; }
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

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
