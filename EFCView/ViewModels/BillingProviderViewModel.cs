using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using EraFileCreator.Service.Messaging;
using EraFileCreator.Services;
using PatientManagement.DAL;

namespace EraFileCreator.ViewModels
{
    public class BillingProviderViewModel : INotifyPropertyChanged
    {
        private Provider _billingProvider;

        public BillingProviderViewModel()
        {
            this.BillingProvider = ProviderService.LoadBillingProvider();
            ProviderService.SaveBillingProvider(this.BillingProvider);
            Messenger.Default.Register<UpdateRepositoriesMessage>(this, this.OnUpdateRepositoriesMessage);
            Messenger.Default.Register<SettingsSavedMessage>(this, this.OnSettingsSavedMessage, "UpdateSettings");
            this.UpdateRenderingProviderCommand = new Command(this.UpdateRenderingProvider, this.CanUpdateRenderingProvider);
        }

        public ICommand UpdateRenderingProviderCommand { get; }

        public Provider BillingProvider
        {
            get => this._billingProvider;
            set
            {
                if (value == this._billingProvider) return;
                this._billingProvider = value;
                this.RaisePropertyChanged("BillingProvider");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void UpdateRenderingProvider(object obj)
        {
            Messenger.Default.Send(this.BillingProvider, "BillingProvider");
        }

        private bool CanUpdateRenderingProvider(object obj)
        {
            return !string.IsNullOrEmpty(this.BillingProvider.FirstName) && !string.IsNullOrEmpty(this.BillingProvider.LastName);
        }

        private void OnSettingsSavedMessage(SettingsSavedMessage obj)
        {
            ProviderService.SaveBillingProvider(this.BillingProvider);
        }

        private void OnUpdateRepositoriesMessage(UpdateRepositoriesMessage obj)
        {
           ProviderService.BillingProviderRepository.AddBillingProvider(this.BillingProvider);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}