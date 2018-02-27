using System.ComponentModel;
using Common.Common.Services;
using PatientManagement.Model;
using PatientManagement.ViewModel.Service.Messaging;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class RenderingProviderViewModel : INotifyPropertyChanged
    {
        private Provider renderingProvider;

        public RenderingProviderViewModel()
        {
            RenderingProvider = new Provider();
            LoadSettings();
            Messenger.Default.Register<Provider>(this, OnReceiptOfBillingProvider, "BillingProvider");
            Messenger.Default.Register<Patient>(this, OnReceiptOfPatient, "AddRenderingProvider");
            Messenger.Default.Register<SettingsSavedMessage>(this, OnSettingsSaved, "UpdateSettings");
            Messenger.Default.Register<Patient>(this, OnPatientChanged, "GiveSelectedPatientProvider");
            Messenger.Default.Send(RenderingProvider, "RenderingProvider");
        }

        public Provider RenderingProvider
        {
            get => renderingProvider;
            set
            {
                if (renderingProvider == value) return;
                renderingProvider = value;
                RaisePropertyChanged("RenderingProvider");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPatientChanged(Patient patient)
        {
            RenderingProvider = patient.RenderingProvider;
        }

        private void OnSettingsSaved(SettingsSavedMessage obj)
        {
            SaveSettings();
        }


        private void LoadSettings()
        {
            RenderingProvider = SettingsService.PullDefaultRenderingProvider(RenderingProvider);
        }

        private void OnReceiptOfPatient(Patient patient)
        {
            patient.RenderingProvider = RenderingProvider;
            SaveSettings();
            RaisePropertyChanged("SelectedPatient");
            RenderingProvider = new Provider();
            LoadSettings();
            RaisePropertyChanged("RenderingProvider");
        }

        private void SaveSettings()
        {
            SettingsService.SetDefaultRenderingProvider(RenderingProvider);
        }

        private void OnReceiptOfBillingProvider(Provider billingProvider)
        {
            if (billingProvider.IsIndividual)
            {
                RenderingProvider.FirstName = billingProvider.FirstName;
                RenderingProvider.LastName = billingProvider.LastName;
                RenderingProvider.Npi = billingProvider.Npi;
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}