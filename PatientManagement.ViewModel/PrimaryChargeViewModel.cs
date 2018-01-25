using System;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            //DeleteSelectedChargeCommand = new Command(DeleteSelectedCharge, CanEditOrDeleteSelectedCharge);
            //EditSelectedChargeCommand = new Command(EditSelectedCharge, CanEditOrDeleteSelectedCharge);
        }

        private void OnInitializationCompleteMessage(InitializationCompleteMessage obj)
        {
            ChargeRepository = new PrimaryChargeRepository();
            SendChargeId();
        }

        private IPrimaryChargeRepository ChargeRepository { get; set; }

        private Guid currentAssociatedPatientGuid;

        private void OnPatientIdReceived(SendGuidService sent)
        {
            SelectedCharge.PatientId = sent.Id;
            currentAssociatedPatientGuid = sent.Id;
            ChargeRepository.Add(SelectedCharge);
        }

        private void SendChargeId()
        {
            Messenger.Default.Send(new SendGuidService(SelectedCharge.Id), "ChargeIdSent");
        }

        private void AddNewCharge(object obj)
        {
            StartTimerForTextConfirmation();
            RaisePropertyChanged("TextConfirmed");
            ReturnNewCharge();
            SelectedCharge.PatientId = currentAssociatedPatientGuid;
            ChargeRepository.Add(selectedCharge);
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

        //public PrimaryCharge SelectedListChargeIndex { get; set; }

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
            //bool b = false;
            //if (!editModeEnabled)
            //{
            //    b = SelectedCharge.ChargeCost > 0 && !string.IsNullOrEmpty(SelectedCharge.ProcedureCode);
            //}
            //return b;
        }

        //private void DeleteSelectedCharge(object obj)
        //{
        //    if (SelectedListChargeIndex == null) return;
        //    var index = Charges.IndexOf(SelectedListChargeIndex);
        //    if (index <= -1) return;
        //    Charges.RemoveAt(index);
        //    RaisePropertyChanged("Charges");
        //    if (editModeEnabled)
        //    {
        //        editModeEnabled = false;
        //    }
        //    ReturnNewCharge();
        //}

        //public ICommand EditSelectedChargeCommand { get; private set; }

        //private bool editModeEnabled;

        //private void EditSelectedCharge(object obj)
        //{
        //    if (!editModeEnabled)
        //    {
        //        SelectedCharge = SelectedListChargeIndex;
        //        RaisePropertyChanged("SelectedCharge");
        //        editModeEnabled = true;
        //    }
        //    else
        //    {
        //        ReturnNewCharge();
        //        editModeEnabled = false;
        //    }
        //}

        //private bool CanEditOrDeleteSelectedCharge(object obj)
        //{
        //    bool b = !string.IsNullOrEmpty(SelectedListChargeIndex?.ProcedureCode);

        //    return b;
        //}

        private ObservableCollection<PrimaryCharge> charges;

        public ObservableCollection<PrimaryCharge> Charges

        {
            get { return charges; }
            set
            {
                if (value == charges) return;
                charges = value;
                RaisePropertyChanged("Charges");
            }
        }

        //public ICommand DeleteSelectedChargeCommand { get; private set; }

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
