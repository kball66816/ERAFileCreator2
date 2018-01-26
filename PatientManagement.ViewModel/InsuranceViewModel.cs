using System;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Common.Common.Services;


namespace PatientManagement.ViewModel
{
    public class InsuranceViewModel: INotifyPropertyChanged
    {
        public InsuranceViewModel()
        {
            LoadInsuranceCompany();
            Messenger.Default.Register<UpdateCalculations>(this, OnUpdateCalculation);
            Messenger.Default.Register<UpdateRepositoriesMessage>(this,OnUpdateRepositoriesMessageReceieved);
            Messenger.Default.Register<SettingsSavedMessage>(this, OnSettingsSaved,"UpdateSettings");
            Messenger.Default.Register<SaveFileMessage>(this,OnSaveFileReceived,"SaveTextFiletoSelectedDirectory");
        }

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
            CalculateCheckAmount();
            RaisePropertyChanged("CheckAmount");
        }

        private void CalculateCheckAmount()
        {
            IPrimaryChargeRepository pcr = new PrimaryChargeRepository();
            decimal chargesPaidAmount = pcr.GetAllCharges().Sum(c => c.PaymentAmount);

            IAddonChargeRepository acr = new AddonChargeRepository();
            decimal addonsPaidAmount = acr.GetAllCharges().Sum(a => a.PaymentAmount);

            Insurance.CheckAmount = chargesPaidAmount + addonsPaidAmount;
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
            SettingsService.SetDefaultInsurance(insurance);
        }
    }
}
