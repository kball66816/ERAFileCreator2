﻿using System.Windows.Input;
using Common.Common.Services;
using EraFileCreator.Service.Messaging;
using EraFileCreator.Services;
using PatientManagement.DAL;

namespace EraFileCreator.ViewModels
{
    public class PatientViewModel : BaseViewModel
    {
        private Patient _selectedPatient;

        public PatientViewModel()
        {
            this.SelectedPatient = PatientService.LoadInitialPatient();
            PatientService.SettingsService.PullDefaultRenderingProvider(this.SelectedPatient.RenderingProvider);
            PatientService.PatientRepository.Add(this.SelectedPatient);
            Messenger.Default.Register<ServiceDescription>(this, this.OnChargeReceived);
            Messenger.Default.Register<Provider>(this, this.OnProviderReceived, "BillingProvider");
            Messenger.Default.Register<SaveFileMessage>(this, this.OnSaveFileMessage,
                "SaveTextFiletoSelectedDirectory");
            Messenger.Default.Register<ListClearedMessage>(this, this.OnListClearedMessageReceived,
                "Patient List Cleared");
            this.AddPatientCommand = new Command(this.AddPatient, this.CanAddPatient);
        }

        public Patient SelectedPatient
        {
            get => this._selectedPatient;
            set
            {
                if (value == this._selectedPatient) return;
                this._selectedPatient = value;
                this.RaisePropertyChanged("SelectedPatient");
            }
        }

        public ICommand AddPatientCommand { get; }

        private void OnListClearedMessageReceived(ListClearedMessage obj)
        {
            this.SelectedPatient = PatientService.LoadInitialPatient();
            PatientService.PatientRepository.Add(this.SelectedPatient);
        }

        private void OnChargeReceived(ServiceDescription charge)
        {
            this.SelectedPatient.Charges.Add(charge);
        }

        private void OnSaveFileMessage(SaveFileMessage obj)
        {
            PatientService.SaveSettings(this.SelectedPatient);
        }

        private void OnProviderReceived(Provider provider)
        {
            this.SelectedPatient.RenderingProvider = provider.CopyProvider();
            this.RaisePropertyChanged("SelectedPatient");
        }

        private void AddPatient(object obj)
        {
            Messenger.Default.Send(this.SelectedPatient, "AddRenderingProvider");
            this.SelectedPatient = PatientService.GetNewPatientBasedOnSettings(this.SelectedPatient);
            PatientService.PatientRepository.Add(this.SelectedPatient);
            this.RaisePropertyChanged("CheckAmount");
        }

        private bool CanAddPatient(object obj)
        {
            return !string.IsNullOrEmpty(this.SelectedPatient.FirstName) &&
                   !string.IsNullOrEmpty(this.SelectedPatient.LastName)
                   && this.SelectedPatient.Charges.Count > 0;
        }
    }
}