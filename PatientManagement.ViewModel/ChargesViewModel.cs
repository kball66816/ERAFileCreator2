using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    class ChargesViewModel
    {
        public ChargesViewModel()
        {
            DeleteSelectedChargeCommand = new Command(DeleteSelectedCharge, CanEditOrDeleteSelectedCharge);
            EditSelectedChargeCommand = new Command(EditSelectedCharge, CanEditOrDeleteSelectedCharge);

        }
        public PrimaryCharge SelectedListChargeIndex { get; set; }

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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
        public ICommand DeleteSelectedChargeCommand { get; private set; }


        private void DeleteSelectedCharge(object obj)
        {
            if (SelectedListChargeIndex == null) return;
            var index = selectedPatient.Charges.IndexOf(SelectedListChargeIndex);
            if (index <= -1) return;
            IPrimaryChargeRepository cp = new PrimaryChargeRepository(SelectedPatient);
            cp.Delete(SelectedListChargeIndex);
            if (editModeEnabled)
            {
                editModeEnabled = false;
            }
        }

        private bool CanEditOrDeleteSelectedCharge(object obj)
        {
            bool b = !string.IsNullOrEmpty(SelectedListChargeIndex?.ProcedureCode);

            return b;
        }

      
    }
}
