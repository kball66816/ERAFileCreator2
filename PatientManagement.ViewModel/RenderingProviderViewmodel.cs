using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.ComponentModel;
using Common.Common.Services;

namespace PatientManagement.ViewModel
{
    public class RenderingProviderViewModel:INotifyPropertyChanged
    {
        public RenderingProviderViewModel()
        {
            RenderingProvider = new Provider();
            LoadSettings();
            Messenger.Default.Register<Provider>(this, OnReceiptOfBillingProvider, "BillingProvider");
            Messenger.Default.Register<Patient>(this, OnReceiptOfPatient, "AddRenderingProvider");
            Messenger.Default.Register<SettingsSavedMessage>(this,OnSettingsSaved, "UpdateSettings");
            Messenger.Default.Register<Patient>(this,OnPatientChanged,"GiveSelectedPatientProvider");
            Messenger.Default.Send(RenderingProvider, "RenderingProvider");
        }

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

        private Provider renderingProvider;

        public Provider RenderingProvider
        {
            get { return renderingProvider; }
            set
            {
                if (renderingProvider == value) return;
                renderingProvider = value;
                RaisePropertyChanged("RenderingProvider");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
