﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Common.Common.Services;
using EFC.BL;
using EraFileCreator.Service.Messaging;
using EraFileCreator.Services;
using PatientManagement.DAL;

namespace EraFileCreator.ViewModels
{
    public class InsuranceViewModel : BaseViewModel
    {
        private InsuranceCompany _insurance;

        public InsuranceViewModel()
        {
            this._settingsService = new SettingsService();
            this.Payment = new Payment();
            this.LoadInsuranceCompany();
            Messenger.Default.Register<UpdateCalculations>(this, this.OnUpdateCalculation);
            Messenger.Default.Register<UpdateRepositoriesMessage>(this, this.OnUpdateRepositoriesMessageReceieved, "UpdateRepositories");
            Messenger.Default.Register<SettingsSavedMessage>(this, this.OnSettingsSaved, "UpdateSettings");
            Messenger.Default.Register<SaveFileMessage>(this, this.OnSaveFileReceived, "CreationCompleted");
            Messenger.Default.Register<UpdateInsuranceCompaniesMessage>(this, this.OnUpdateReceived,"NewInsuranceSaved");
            this.Payment.Amount = this.CalculateCheckAmount();
            this.OpenEditWindowCommand = new Command(this.OpenEditWindow);
        }

        private void OnUpdateReceived(UpdateInsuranceCompaniesMessage obj)
        {
            this.InsuranceCompanies = new ObservableCollection<InsuranceCompany>(this._settingsService.GetInsuranceCompanies());
        }

        public ObservableCollection<InsuranceCompany> InsuranceCompanies
        {
            get => this._insuranceCompanies;
            set
            {
                this._insuranceCompanies = value;
                this.RaisePropertyChanged("InsuranceCompanies");
            }
        }

        public Payment Payment
        {
            get => this._payment;
            set
            {
                this._payment = value;
                this.RaisePropertyChanged("Payment");
            }
        }

        private void OpenEditWindow(object obj)
        {
            var window = new ViewFactory();
            window.ShowUpdateInsuranceCompaniesWindow();
        }

        private ISettingsService _settingsService;

        public ICommand OpenEditWindowCommand { get; set; }
        private Payment _payment;
        private ObservableCollection<InsuranceCompany> _insuranceCompanies;

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

        private void OnSaveFileReceived(SaveFileMessage obj)
        {
            this.SaveSettings();
            this.Insurance = new InsuranceCompany(this.Insurance);
            this.Payment = new Payment();
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

            this.Payment.Amount = chargesPaidAmount + addonsPaidAmount;
            return this.Payment.Amount;
        }

        private void LoadInsuranceCompany()
        {
            this.Insurance = new InsuranceCompany();
            this.PaymentTypes = this.Payment.Types;
            this.InsuranceCompanies = new ObservableCollection<InsuranceCompany>(this._settingsService.GetInsuranceCompanies());
            this.SaveInsuranceToRepository();
            this.RaisePropertyChanged("Insurance");
        }

        private void SaveInsuranceToRepository()
        {
            var insuranceWithPaymentRepository = new InsurancePaymentRepository();
            insuranceWithPaymentRepository.AddInsurancePayment(this.Insurance, this.Payment);
        }

        private void SaveSettings()
        {
            this._settingsService.SaveInsuranceCompanies(this._insuranceCompanies.ToList());
        }
    }
}