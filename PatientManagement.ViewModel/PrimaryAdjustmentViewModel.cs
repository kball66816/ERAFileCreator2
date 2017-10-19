using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class PrimaryAdjustmentViewModel:INotifyPropertyChanged
    {
        public PrimaryAdjustmentViewModel()
        {
            SelectedAdjustment = new Adjustment();
            PrimaryAdjustmentReasonCodes = selectedAdjustment.AdjustmentReasonCodes;
            PrimaryAdjustmentType = SelectedAdjustment.AdjustmentTypes;
            AddChargeAdjustmentCommand = new Command(AddAdjustmentToCharge, CanAddAdjustment);


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

        private void AddAdjustmentToCharge(object obj)
        {
            Messenger.Default.Send<Adjustment>(selectedAdjustment,"PrimaryCharge");

            // IAdjustmentRepository adjustmentRepository = new AdjustmentRepository(SelectedCharge);
            //  adjustmentRepository.Add(SelectedAdjustment);
            SelectedAdjustment = new Adjustment();
            RaisePropertyChanged("SelectedAdjustment");
           // RefreshAllCounters();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
