using PatientManagement.DAL;
using PatientManagement.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using WPFERA.Services;

namespace WPFERA.ViewModel
{
    public class PrimaryChargeViewModel
    {
        private ObservableCollection<PrimaryCharge> unaddedCharges;
        private IPrimaryChargeRepository chargeRepository;

        /// <summary>
        /// Used to manage charges in the repository
        /// </summary>
        public PrimaryChargeViewModel()
        {

            ChargeRepository = new PrimaryChargeRepository();
            UnaddedCharges = ChargeRepository.GetAllCharges();
        }


        public ObservableCollection<PrimaryCharge> UnaddedCharges

        {
            get { return unaddedCharges; }
            set
            {
                if (value != unaddedCharges)
                {
                    unaddedCharges = value;
                    RaisePropertyChanged("UnaddedCharges");
                }

            }
        }

        public IPrimaryChargeRepository ChargeRepository

        {
            get { return chargeRepository; }
            set
            {
                if (chargeRepository != value)
                {
                    chargeRepository = value;
                    RaisePropertyChanged("ChargeRepository");
                }

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
