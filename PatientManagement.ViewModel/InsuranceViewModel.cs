using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;


namespace PatientManagement.ViewModel
{
    public class InsuranceViewModel: INotifyPropertyChanged
    {
        public InsuranceViewModel()
        {
            Settings = new SettingsService();
            LoadInsuranceCompany();
            Messenger.Default.Register<UpdateCalculations>(this, OnUpdateCalculation);
            Messenger.Default.Register<UpdateRepositoriesMessage>(this,OnUpdateRepositoriesMessageReceieved);
            Messenger.Default.Register<SettingsSavedMessage>(this, OnSettingsSaved,"UpdateSettings");
        }

        private void OnSettingsSaved(SettingsSavedMessage obj)
        {
            SaveSettings();
        }

        private void OnUpdateRepositoriesMessageReceieved(UpdateRepositoriesMessage obj)
        {
            SaveInsuranceToRepository();
        }

        private SettingsService Settings { get; set; }

        public Dictionary<string, string> InsuranceStates { get; set; }

        private InsuranceCompany insurance;

        public InsuranceCompany Insurance
        {
            get { return insurance; }
            set
            {
                if (value == insurance) return;
                insurance = value;
                RaisePropertyChanged("Insurance");
            }
        }

        private void OnUpdateCalculation(UpdateCalculations obj)
        {
            IPatientRepository patients = new PatientRepository();
            decimal addonsPaidAmount = 0;       

            foreach (var patient in patients.GetAllPatients())
            {
                var chargesPaidAmount = patient.Charges.Sum(c => c.PaymentAmount);

                foreach (var charge in patient.Charges)
                {
                    addonsPaidAmount += charge.AddonChargeList.Sum(p => p.PaymentAmount);
                    Insurance.CheckAmount = chargesPaidAmount + addonsPaidAmount;
                    RaisePropertyChanged("CheckAmount");
                }
            }

            MessageBox.Show(insurance.CheckAmount.ToString());
        }

        private void LoadInsuranceCompany()
        {
            Insurance = new InsuranceCompany();
            Insurance = Settings.PullDefaultInsurance(Insurance);
            PaymentTypes = Insurance.PaymentTypes;
            InsuranceStates = Insurance.Address.States;
            SaveInsuranceToRepository();
        }

        public Dictionary<string, string> PaymentTypes { get; set; }


        private void SaveInsuranceToRepository()
        {
            IInsurance saveInsurance = new InsuranceRepository();
            saveInsurance.AddInsurance(insurance);
        }

       public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveSettings()
        {
            Settings.SetDefaultInsurance(insurance);
        }
    }
}
