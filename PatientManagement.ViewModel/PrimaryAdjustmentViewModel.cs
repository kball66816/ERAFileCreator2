using System;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using EFC.BL;
using PatientManagement.DAL;

namespace PatientManagement.ViewModel
{
    public class PrimaryAdjustmentViewModel:INotifyPropertyChanged
    {
        public PrimaryAdjustmentViewModel()
        {
            SelectedAdjustment = new Adjustment();
            PrimaryAdjustmentReasonCodes = selectedAdjustment.AdjustmentReasonCodes;
            PrimaryAdjustmentType = SelectedAdjustment.AdjustmentTypes;
            AddChargeAdjustmentCommand = new Command(AddAdjustmentCommand, CanAddAdjustment);
            Messenger.Default.Register<SendGuidService>(this,OnChargeIdReceived,"ChargeIdSent");
            adjustmentRepository = new AdjustmentRepository();
        }

        private bool initializationComplete;
        private readonly IAdjustmentRepository adjustmentRepository;

        private Guid currentChargeGuid;

        private void OnChargeIdReceived(SendGuidService sent)
        {
            if (initializationComplete)
            {
                SelectedAdjustment = new Adjustment();
            }

            initializationComplete = true;
            SelectedAdjustment.ChargeId = sent.Id;
            currentChargeGuid = sent.Id;
            adjustmentRepository.Add(SelectedAdjustment);
        }

        private void AddAdjustmentCommand(object obj)
        {
            SelectedAdjustment = new Adjustment {ChargeId = currentChargeGuid};
            adjustmentRepository.Add(SelectedAdjustment);
        }
        private Adjustment selectedAdjustment;

        public Adjustment SelectedAdjustment
        {
            get { return selectedAdjustment; }
            set
            {
                if (value == selectedAdjustment) return;
                selectedAdjustment = value;
                RaisePropertyChanged("SelectedAdjustment");
            }
        }
        public Dictionary<string, string> PrimaryAdjustmentType { get; set; }

        public Dictionary<string, string> PrimaryAdjustmentReasonCodes { get; set; }

        public ICommand AddChargeAdjustmentCommand { get; private set; }

        private bool CanAddAdjustment(object obj)
        {
            return !string.IsNullOrEmpty(SelectedAdjustment.AdjustmentReasonCode) &&
                   SelectedAdjustment.AdjustmentAmount > 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
