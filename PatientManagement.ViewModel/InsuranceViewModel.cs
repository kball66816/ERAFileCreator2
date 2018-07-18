using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using EFC.BL;
using Newtonsoft.Json;
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
            Payment = new Payment();
            this.LoadInsuranceCompany();
            Messenger.Default.Register<UpdateCalculations>(this, this.OnUpdateCalculation);
            Messenger.Default.Register<UpdateRepositoriesMessage>(this, this.OnUpdateRepositoriesMessageReceieved, "UpdateRepositories");
            Messenger.Default.Register<SettingsSavedMessage>(this, this.OnSettingsSaved, "UpdateSettings");
            Messenger.Default.Register<SaveFileMessage>(this, this.OnSaveFileReceived, "CreationCompleted");
            Payment.Amount = this.CalculateCheckAmount();
            this.CreateJsonCommand = new Command(CreateJson);
        }

        public Payment Payment
        {
            get => this._payment;
            set
            {
                this._payment = value;
                RaisePropertyChanged("Payment");
            }
        }

        private void CreateJson(object obj)
        {
            var insuranceCompany = obj as InsuranceCompany;
            insuranceCompany.Address.StreetOne = "Street One";
            insuranceCompany.Address.StreetTwo = "Street Two";
            insuranceCompany.Address.City = "Birmingham";
            insuranceCompany.Address.State = "AL";
            insuranceCompany.Address.ZipCode = "12345";
            this.test = JsonConvert.SerializeObject(insuranceCompany, Formatting.Indented);
        }

        private ISettingsService _settingsService;

        public ICommand CreateJsonCommand { get; set; }
        private string _test;
        private Payment _payment;

        public string test
        {
            get => this._test;
            set
            {
                this._test = value;
                RaisePropertyChanged("test");
            }
        }

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

            Payment.Amount = chargesPaidAmount + addonsPaidAmount;
            return Payment.Amount;
        }

        private void LoadInsuranceCompany()
        {
            this.Insurance = new InsuranceCompany();
            this.PaymentTypes = Payment.Types;
            this.Insurance = this._settingsService.PullDefaultInsurance(this.Insurance);
            this.SaveInsuranceToRepository();
            this.RaisePropertyChanged("Insurance");
        }


        private void SaveInsuranceToRepository()
        {
            var insuranceWithPaymentRepository = new InsurancePaymentRepository();
            insuranceWithPaymentRepository.AddInsurancePayment(this.Insurance, this.Payment);
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