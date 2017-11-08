using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class PrimaryChargeViewModel : INotifyPropertyChanged
    {
        public PrimaryChargeViewModel()
        {
            SelectedCharge = new PrimaryCharge();
            PlacesOfService = selectedCharge.PlaceOfService.PlacesOfService;
            Messenger.Default.Register<Adjustment>(this, OnAdjustmentReceived, "PrimaryChargeAdjustment");
            Messenger.Default.Register<AddonCharge>(this, OnAddonChargeReceived, "AddonCharge");
            Messenger.Default.Register<ObservableCollection<PrimaryCharge>>(this, OnChargeCollectionReceived, "UpdateChargesList");
            AddChargeToPatientCommand = new Command(AddChargeToPatientV2, CanAddChargeToPatient);
            DeleteSelectedChargeCommand = new Command(DeleteSelectedCharge, CanEditOrDeleteSelectedCharge);
            EditSelectedChargeCommand = new Command(EditSelectedCharge, CanEditOrDeleteSelectedCharge);
        }

        private void SendAddonChargeList()
        {
            Messenger.Default.Send(SelectedCharge.AddonChargeList, "AddonList");
        }

        private void SendAdjustmentsList()
        {
            Messenger.Default.Send(SelectedCharge.AdjustmentList, "PrimaryChargeAdjustments");
        }

        private void OnAddonChargeReceived(AddonCharge charge)
        {
            IAddonChargeRepository acr = new AddonChargeRepository(SelectedCharge);
            acr.Add(charge);
            RaisePropertyChanged("SelectedCharge");
            SendAddonChargeList();
        }

        private void OnChargeCollectionReceived(ObservableCollection<PrimaryCharge> chargesList)
        {
            Charges = chargesList;
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

        public PrimaryCharge SelectedListChargeIndex { get; set; }

        public Dictionary<string, string> PlacesOfService { get; set; }

        private void OnAdjustmentReceived(Adjustment adjustment)
        {
            IAdjustmentRepository ar = new AdjustmentRepository(SelectedCharge);
            ar.Add(adjustment);
            RaisePropertyChanged("SelectedCharge");
            SendAdjustmentsList();
        }
     
        private void AddChargeToPatientV2(object obj)
        {
            Messenger.Default.Send(SelectedCharge, "Patient");
            ReturnNewCharge();
            StartTimerForTextConfirmation();
            RaisePropertyChanged("TextConfirmed");



            Messenger.Default.Send(new UpdateCalculations());
            RaisePropertyChanged("SelectedCharge");
            RaisePropertyChanged("Charges");
            SendAdjustmentsList();
            SendAddonChargeList();
        }

        readonly Timer timer = new Timer { Interval = 5000 };

        private void StartTimerForTextConfirmation()
        {
            var confirm = new ConfirmationService();


            timer.Elapsed += OnTimeElapsed;
            timer.Start();

            TextConfirmed = confirm.ChargeAddedTextConfirmation();
        }

        private void OnTimeElapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            TextConfirmed = string.Empty;
            RaisePropertyChanged("TextConfirmed");

        }
        public string TextConfirmed { get; set; }


        private bool CanAddChargeToPatient(object obj)
        {
            bool b = false;
            if (!editModeEnabled)
            {
                b = SelectedCharge.ChargeCost > 0 && !string.IsNullOrEmpty(SelectedCharge.ProcedureCode);

            }

            return b;
        }
        private void DeleteSelectedCharge(object obj)
        {
            if (SelectedListChargeIndex == null) return;
            var index = Charges.IndexOf(SelectedListChargeIndex);
            if (index <= -1) return;
            Charges.RemoveAt(index);
            RaisePropertyChanged("Charges");
            if (editModeEnabled)
            {
;
                editModeEnabled = false;
            }
            ReturnNewCharge();
            SendAddonChargeList();
            SendAdjustmentsList();
        }
        public ICommand EditSelectedChargeCommand { get; private set; }

        private bool editModeEnabled;

        private void EditSelectedCharge(object obj)
        {
            if (!editModeEnabled)
            {
                SelectedCharge = SelectedListChargeIndex;
                RaisePropertyChanged("SelectedCharge");
                editModeEnabled = true;
            }
            else
            {
                ReturnNewCharge();
                editModeEnabled = false;
            }

            SendAddonChargeList();
            SendAdjustmentsList();
        }

        private bool CanEditOrDeleteSelectedCharge(object obj)
        {
            bool b = !string.IsNullOrEmpty(SelectedListChargeIndex?.ProcedureCode);

            return b;
        }

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

        public ICommand DeleteSelectedChargeCommand { get; private set; }

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
