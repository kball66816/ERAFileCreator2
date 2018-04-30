using System.Collections.Generic;
using System.ComponentModel;
using Common.Common.Services;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Service.Messaging;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class InsuranceViewModel : INotifyPropertyChanged
    {
        private InsuranceCompany _insurance;

        public InsuranceViewModel()
        {
            this._settingsService = new SettingsService();
            this.LoadInsuranceCompany();
            Messenger.Default.Register<UpdateCalculations>(this, this.OnUpdateCalculation);
            Messenger.Default.Register<UpdateRepositoriesMessage>(this, this.OnUpdateRepositoriesMessageReceieved,"UpdateRepositories");
            Messenger.Default.Register<SettingsSavedMessage>(this, this.OnSettingsSaved, "UpdateSettings");
            Messenger.Default.Register<SaveFileMessage>(this, this.OnSaveFileReceived, "SaveTextFiletoSelectedDirectory");
            this.Insurance.CheckAmount = this.CalculateCheckAmount();
        }

        private readonly ISettingsService _settingsService;

        public InsuranceCompany Insurance
        {
            get => this._insurance;
            set
            {
                if (value == this._insurance) return;
                this._insurance = value;
                this.RaisePropertyChanged("Insurance");
            }
        }

        public Dictionary<string, string> PaymentTypes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnSaveFileReceived(SaveFileMessage obj)
        {
            this.SaveSettings();
            this._insurance = new InsuranceCompany(this._insurance);
            this.RaisePropertyChanged("Insurance");
            this.SaveInsuranceToRepository();
        }

        private void OnSettingsSaved(SettingsSavedMessage obj)
        {
            this.SaveSettings();
        }

        private void OnUpdateRepositoriesMessageReceieved(UpdateRepositoriesMessage obj)
        {
            this.SaveInsuranceToRepository();
        }

        private void OnUpdateCalculation(UpdateCalculations obj)
        {
            this.CalculateCheckAmount();
            this.RaisePropertyChanged("CheckAmount");
        }

        private decimal CalculateCheckAmount()
        {
            decimal chargesPaidAmount = 0;
            decimal addonsPaidAmount = 0;
            foreach (var patient in PatientService.PatientRepository.GetAllPatients())
            foreach (var c in patient.Charges)
            {
                chargesPaidAmount += c.PaymentAmount;

                foreach (var addonCharge in c.AdditionalServiceDescriptions)
                {
                    addonsPaidAmount += addonCharge.PaymentAmount;
                }
            }

            return this.Insurance.CheckAmount = chargesPaidAmount + addonsPaidAmount;
        }

        private void LoadInsuranceCompany()
        {
            this.Insurance = new InsuranceCompany();
            this.PaymentTypes = this.Insurance.PaymentTypes;
            this.Insurance = this._settingsService.PullDefaultInsurance(this.Insurance);
            this.SaveInsuranceToRepository();
            this.RaisePropertyChanged("Insurance");
        }


        private void SaveInsuranceToRepository()
        {
            IInsurance saveInsurance = new InsuranceRepository();
            saveInsurance.AddInsurance(this._insurance);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveSettings()
        {
          this._settingsService.SetDefaultInsurance(this._insurance);
        }
    }
}