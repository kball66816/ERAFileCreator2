using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class PrimaryChargeViewModel:INotifyPropertyChanged
    {
        public PrimaryChargeViewModel()
        {
            Settings = new SettingsService();
            SelectedCharge = new PrimaryCharge();
            PlacesOfService = selectedCharge.PlaceOfService.PlacesOfService;
            Messenger.Default.Register<Adjustment>(this, OnAdjustmentReceived, "PrimaryCharge");
            AddChargeToPatientCommand = new Command(AddChargeToPatientV2, CanAddChargeToPatient);



        }

        public ICommand AddChargeToPatientCommand { get; set; }

        private SettingsService Settings { get; set; }

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

        private void OnAdjustmentReceived(Adjustment adjustment)
        {
            IAdjustmentRepository ar = new AdjustmentRepository(SelectedCharge);
            ar.Add(adjustment);
            RaisePropertyChanged("SelectedCharge");
        }

        private void AddChargeToPatientV2(object obj)
        {
            Messenger.Default.Send<PrimaryCharge>(SelectedCharge, "Patient");

            //var chargeRepository = new PrimaryChargeRepository(selectedPatient);
            //chargeRepository.Add(SelectedCharge);
            ReturnNewCharge();

            Messenger.Default.Send(new UpdateCalculations());
            RaisePropertyChanged("SelectedCharge");
            RaisePropertyChanged("Charges");
        }

        private bool CanAddChargeToPatient(object obj)
        {
            bool b = true;
            //if (!editModeEnabled)
            //{
            //    b = SelectedCharge.ChargeCost > 0 && !string.IsNullOrEmpty(SelectedCharge.ProcedureCode);

            //}

            return b;
        }

        private void ReturnNewCharge()
        {
            SelectedCharge = Settings.ReuseChargeForNextPatient
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
