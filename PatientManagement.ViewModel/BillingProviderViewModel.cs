using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;

namespace PatientManagement.ViewModel
{
    public class BillingProviderViewModel : INotifyPropertyChanged
    {
        public BillingProviderViewModel()
        {
            LoadBillingProvider();
            SaveProviderToRepository();
            Messenger.Default.Register<UpdateRepositoriesMessage>(this, OnUpdateRepositoriesMessage);
            Messenger.Default.Register<SettingsSavedMessage>(this, OnSettingsSavedMessage,"UpdateSettings");
            UpdateRenderingProviderCommand = new Command(UpdateRenderingProvider, CanUpdateRenderingProvider);
            UpdateRenderingProvider(null);
        }

        private void UpdateRenderingProvider(object obj)
        {
            DetermineBusinessName();
            Messenger.Default.Send(BillingProvider, "BillingProvider");
        }

        private void DetermineBusinessName()
        {
            BillingProvider.BusinessName = BillingProvider.IsIndividual ? BillingProvider.FullName : BillingProvider.BusinessName;
        }

        private bool CanUpdateRenderingProvider(object obj)
        {
            return true;
        }

        public ICommand UpdateRenderingProviderCommand { get; private set; }

        private void OnSettingsSavedMessage(SettingsSavedMessage obj)
        {
            SaveSettings();
        }

        private void OnUpdateRepositoriesMessage(UpdateRepositoriesMessage obj)
        {
            SaveProviderToRepository();
        }

        private Provider billingProvider;

        public Provider BillingProvider
        {
            get { return billingProvider; }
            set
            {
                if (value == billingProvider) return;
                billingProvider = value;
                RaisePropertyChanged("BillingProvider");
            }
        }
        public Dictionary<string, string> ProviderStates { get; set; }


        private void LoadBillingProvider()
        {
            BillingProvider = new Provider();
            BillingProvider = SettingsService.PullDefaultBillingProvider(BillingProvider);
            ProviderStates = BillingProvider.Address.States;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
