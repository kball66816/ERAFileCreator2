using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Windows.Input;
using Common.Common.Services;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

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
            SelectedCharge.AddChargeToRepository();
            SendChargeId();
        }


        private void SendChargeId()
        {
            Messenger.Default.Send(new SendGuidService(SelectedCharge.Id), "ChargeIdSent");
        }

        private void AddNewCharge(object obj)
        {
            StartTimerForTextConfirmation();
            RaisePropertyChanged("TextConfirmed");
            SelectedCharge = ChargeService.SetNewOrClonedChargeByUserSettings(SelectedCharge);
            RaisePropertyChanged("SelectedCharge");
            SelectedCharge.PatientId = currentAssociatedPatientGuid;
            SelectedCharge.AddChargeToRepository();
            SendChargeId();
        }

        private void StartTimerForTextConfirmation()
        {
            var confirm = new ConfirmationService();

            ChargeService.ChargeDisplayTimer.Elapsed += OnTimeElapsed;
            ChargeService.ChargeDisplayTimer.Start();

            TextConfirmed = confirm.ChargeAddedTextConfirmation();
        }

        private void OnTimeElapsed(object source, ElapsedEventArgs e)
        {
            ChargeService.ChargeDisplayTimer.Stop();
            TextConfirmed = string.Empty;
            RaisePropertyChanged("TextConfirmed");
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