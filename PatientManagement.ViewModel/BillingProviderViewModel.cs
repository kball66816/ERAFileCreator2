using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class BillingProviderViewModel : INotifyPropertyChanged
    {
        private Provider billingProvider;

        public BillingProviderViewModel()
        {
            LoadBillingProvider();
            SaveProviderToRepository();
            Messenger.Default.Register<UpdateRepositoriesMessage>(this, OnUpdateRepositoriesMessage);
            Messenger.Default.Register<SettingsSavedMessage>(this, OnSettingsSavedMessage, "UpdateSettings");
            UpdateRenderingProviderCommand = new Command(UpdateRenderingProvider, CanUpdateRenderingProvider);
            UpdateRenderingProvider(null);
        }

        public ICommand UpdateRenderingProviderCommand { get; }

        public Provider BillingProvider
        {
            get => billingProvider;
            set
            {
                if (value == billingProvider) return;
                billingProvider = value;
                RaisePropertyChanged("BillingProvider");
            }
        }

        public Dictionary<string, string> ProviderStates { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void UpdateRenderingProvider(object obj)
        {
            DetermineBusinessName();
            Messenger.Default.Send(BillingProvider, "BillingProvider");
        }

        private void DetermineBusinessName()
        {
            BillingProvider.BusinessName =
                BillingProvider.IsIndividual ? BillingProvider.FullName : BillingProvider.BusinessName;
        }

        private bool CanUpdateRenderingProvider(object obj)
        {
            return true;
        }

        private void OnSettingsSavedMessage(SettingsSavedMessage obj)
        {
            SaveSettings();
        }

        private void OnUpdateRepositoriesMessage(UpdateRepositoriesMessage obj)
        {
            SaveProviderToRepository();
        }


        private void LoadBillingProvider()
        {
            BillingProvider = new Provider();
            BillingProvider = SettingsService.PullDefaultBillingProvider(BillingProvider);
            ProviderStates = BillingProvider.Address.States;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveSettings()
        {
            SettingsService.SetDefaultBillingProvider(billingProvider);
        }

        private void SaveProviderToRepository()
        {
            IProvider saveProvider = new BillingProviderRepository();
            saveProvider.AddBillingProvider(billingProvider);
        }

        //private void OnUpdateRenderingProvider(Provider renderingProvider)
        //{
        //    if (BillingProvider.IsAlsoRendering)
        //    {
        //        renderingProvider.FirstName = BillingProvider.FirstName;
        //        renderingProvider.LastName = BillingProvider.LastName;
        //        renderingProvider.Npi = BillingProvider.Npi;
        //        RaisePropertyChanged("Patient");
        //    }

        //    else if (billingProvider.IsAlsoRendering == false)
        //    {
        //        return;
        //    }
    }
}