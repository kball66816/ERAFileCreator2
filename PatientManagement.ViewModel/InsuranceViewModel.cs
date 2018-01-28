using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Common.Common.Services;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class InsuranceViewModel : INotifyPropertyChanged
    {
        private InsuranceCompany insurance;

        public InsuranceViewModel()
        {
            LoadInsuranceCompany();
            Messenger.Default.Register<UpdateCalculations>(this, OnUpdateCalculation);
            Messenger.Default.Register<UpdateRepositoriesMessage>(this, OnUpdateRepositoriesMessageReceieved);
            Messenger.Default.Register<SettingsSavedMessage>(this, OnSettingsSaved, "UpdateSettings");
            Messenger.Default.Register<SaveFileMessage>(this, OnSaveFileReceived, "SaveTextFiletoSelectedDirectory");
            insurance.CheckAmount = CalculateCheckAmount();
        }

        public Dictionary<string, string> InsuranceStates { get; set; }

        public InsuranceCompany Insurance
        {
            get => insurance;
            set
            {
                if (value == insurance) return;
                insurance = value;
                RaisePropertyChanged("Insurance");
            }
        }

        public Dictionary<string, string> PaymentTypes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnSaveFileReceived(SaveFileMessage obj)
        {
            SaveSettings();
            insurance = new InsuranceCompany(insurance);
            RaisePropertyChanged("Insurance");
            SaveInsuranceToRepository();
        }

        private void OnSettingsSaved(SettingsSavedMessage obj)
        {
            SaveSettings();
        }

        private void OnUpdateRepositoriesMessageReceieved(UpdateRepositoriesMessage obj)
        {
            SaveInsuranceToRepository();
        }

        private void OnUpdateCalculation(UpdateCalculations obj)
        {
            CalculateCheckAmount();
            RaisePropertyChanged("CheckAmount");
        }

        private decimal CalculateCheckAmount()
        {
            IPrimaryChargeRepository pcr = new PrimaryChargeRepository();
            var chargesPaidAmount = pcr.GetAllCharges().Sum(c => c.PaymentAmount);

            IAddonChargeRepository acr = new AddonChargeRepository();
            var addonsPaidAmount = acr.GetAllCharges().Sum(a => a.PaymentAmount);

            return Insurance.CheckAmount = chargesPaidAmount + addonsPaidAmount;
        }

        private void LoadInsuranceCompany()
        {
            Insurance = new InsuranceCompany();
            Insurance = SettingsService.PullDefaultInsurance(Insurance);
            PaymentTypes = Insurance.PaymentTypes;
            InsuranceStates = Insurance.Address.States;
            SaveInsuranceToRepository();
            RaisePropertyChanged("Insurance");
        }


        private void SaveInsuranceToRepository()
        {
            IInsurance saveInsurance = new InsuranceRepository();
            saveInsurance.AddInsurance(insurance);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveSettings()
        {
            SettingsService.SetDefaultInsurance(insurance);
        }
    }
}