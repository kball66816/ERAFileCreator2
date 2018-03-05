using Common.Common.Services;
using PatientManagement.Model;
using PatientManagement.ViewModel.Service.Messaging;
using PatientManagement.ViewModel.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class PrimaryChargeViewModel : INotifyPropertyChanged
    {
        public PrimaryChargeViewModel()
        {
            SelectedCharge = ChargeService.GetNewCharge();
            PlacesOfService = selectedCharge.PlaceOfService.PlacesOfService;
            Messenger.Default.Register<InitializationCompleteMessage>(this, OnInitializationCompleteMessage);
            Messenger.Default.Register<SendGuidService>(this, OnPatientIdReceived, "PatientIdSent");
            AddChargeToPatientCommand = new Command(AddNewCharge, CanAddChargeToPatient);
        }

        public int Count
        {
            get => ChargeService.ChargeRepository
                .GetAllCharges().Count(c=>c.PatientId == currentAssociatedPatientGuid);
        }

        private Guid currentAssociatedPatientGuid;

        private PrimaryCharge selectedCharge;


        public ICommand AddChargeToPatientCommand { get; set; }

        public PrimaryCharge SelectedCharge
        {
            get => selectedCharge;
            set
            {
                if (value == selectedCharge) return;
                selectedCharge = value;
                RaisePropertyChanged("SelectedCharge");
            }
        }

        public Dictionary<string, string> PlacesOfService { get; set; }

        public string TextConfirmed { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnInitializationCompleteMessage(InitializationCompleteMessage obj)
        {
            SendChargeId();
        }

        private void OnPatientIdReceived(SendGuidService sent)
        {
            if (StarterService.InitializationComplete)
            {
                SelectedCharge = ChargeService.SetNewOrClonedChargeByUserSettings(SelectedCharge);
                RaisePropertyChanged("SelectedCharge");
            }
            SelectedCharge.PatientId = sent.Id;
            currentAssociatedPatientGuid = sent.Id;
            SendChargeId();
            RaisePropertyChanged("Count");

        }
        
        private void SendChargeId()
        {
            Messenger.Default.Send(new SendGuidService(SelectedCharge.Id), "ChargeIdSent");
        }

        private void AddNewCharge(object obj)
        {
            ChargeService.ChargeRepository.Add(SelectedCharge);
            SelectedCharge = ChargeService.SetNewOrClonedChargeByUserSettings(SelectedCharge);
            RaisePropertyChanged("SelectedCharge");
            SelectedCharge.PatientId = currentAssociatedPatientGuid;
            SendChargeId();
            RaisePropertyChanged("Count");
            Messenger.Default.Send(new UpdateCalculations());
        }

        private bool CanAddChargeToPatient(object obj)
        {
            return selectedCharge.ChargeCost > 0 && !string.IsNullOrEmpty(SelectedCharge.ProcedureCode);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}