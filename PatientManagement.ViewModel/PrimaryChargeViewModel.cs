using System;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Windows.Input;
using Common.Common.Services;

namespace PatientManagement.ViewModel
{
    public class PrimaryChargeViewModel : INotifyPropertyChanged
    {
        public PrimaryChargeViewModel()
        {
            SelectedCharge = new PrimaryCharge();
            PlacesOfService = selectedCharge.PlaceOfService.PlacesOfService;
            Messenger.Default.Register<InitializationCompleteMessage>(this, OnInitializationCompleteMessage);
            Messenger.Default.Register<SendGuidService>(this, OnPatientIdReceived,"PatientIdSent");
            AddChargeToPatientCommand = new Command(AddNewCharge, CanAddChargeToPatient);
            chargeRepository = new PrimaryChargeRepository();
        }

        private bool initializationComplete;

        private void OnInitializationCompleteMessage(InitializationCompleteMessage obj)
        {
            SendChargeId();
        }

        private readonly IPrimaryChargeRepository chargeRepository;


        private Guid currentAssociatedPatientGuid;

        private void OnPatientIdReceived(SendGuidService sent)
        {
            if (initializationComplete)
            {
                ReturnNewCharge();
            }
            initializationComplete = true;
            SelectedCharge.PatientId = sent.Id;
            currentAssociatedPatientGuid = sent.Id;
            chargeRepository.Add(SelectedCharge);
            SendChargeId();
        }


        private void SendChargeId()
        {
            Messenger.Default.Send(new SendGuidService(SelectedCharge.Id), "ChargeIdSent");
        }

        private void AddNewCharge(object obj)
        {
            Messenger.Default.Send(new UpdateCalculations());
            StartTimerForTextConfirmation();
            RaisePropertyChanged("TextConfirmed");
            ReturnNewCharge();
            SelectedCharge.PatientId = currentAssociatedPatientGuid;
            chargeRepository.Add(selectedCharge);
            SendChargeId();
            RaisePropertyChanged("Charges");
        }

        public ICommand AddChargeToPatientCommand { get; set; }

        private PrimaryCharge selectedCharge;

        public PrimaryCharge SelectedCharge
        {
            get { return selectedCharge; }
            set
            {
                if (value == selectedCharge) return;
                selectedCharge = value;
                RaisePropertyChanged("SelectedCharge");
            }
        }

        public Dictionary<string, string> PlacesOfService { get; set; }

        readonly Timer timer = new Timer { Interval = 5000 };

        private void StartTimerForTextConfirmation()
        {
            var confirm = new ConfirmationService();

            timer.Elapsed += OnTimeElapsed;
            timer.Start();

            TextConfirmed = confirm.ChargeAddedTextConfirmation();
        }

        private void OnTimeElapsed(object source, ElapsedEventArgs e)
        {
            timer.Stop();
            TextConfirmed = string.Empty;
            RaisePropertyChanged("TextConfirmed");
        }

        public string TextConfirmed { get; set; }

        private bool CanAddChargeToPatient(object obj)
        {
            return selectedCharge.ChargeCost > 0 && !string.IsNullOrEmpty(SelectedCharge.ProcedureCode);
        }

        private void ReturnNewCharge()
        {
            SelectedCharge = SettingsService.ReuseChargeForNextPatient
                ? new PrimaryCharge(SelectedCharge)
                : new PrimaryCharge();
            RaisePropertyChanged("SelectedCharge");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
