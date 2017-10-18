using System.Collections.Generic;
using System.ComponentModel;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class BillingProviderViewModel:INotifyPropertyChanged
    {
        public BillingProviderViewModel()
        {
            Settings = new SettingsService();
            LoadBillingProvider();
            Messenger.Default.Register<Provider>(this, x);
            Messenger.Default.Register<UpdateRepositoriesMessage>(this,OnUpdateRepositoriesMessage);
            Messenger.Default.Register<SettingsSavedMessage>(this,OnSettingsSavedMessage);


        }


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

        private SettingsService Settings { get; set; }

        private void LoadBillingProvider()
        {
            BillingProvider = new Provider();
            BillingProvider = Settings.PullDefaultBillingProvider(BillingProvider);
            ProviderStates = BillingProvider.Address.States;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SaveSettings()
        {
            Settings.SetDefaultBillingProvider(billingProvider);
          
        }

        private void SaveProviderToRepository()
        {
            IProvider saveProvider = new BillingProviderRepository();
            saveProvider.AddBillingProvider(billingProvider);
        }

        private void x(Provider renderingProvider)
        {
            if (BillingProvider.IsAlsoRendering)
            {
                renderingProvider.FirstName = BillingProvider.FirstName;
                renderingProvider.LastName = BillingProvider.LastName;
                renderingProvider.Npi = BillingProvider.Npi;
                RaisePropertyChanged("Patient");
            }

            else if (billingProvider.IsAlsoRendering == false)
            {
                return;
            }
        }

        private void x()
        {
            
          
        }
    }
}
