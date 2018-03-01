using System;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class AddonChargeViewModel : INotifyPropertyChanged
    {
        private Guid currentChargeGuid;

        private AddonCharge selectedAddonCharge;

        public AddonChargeViewModel()
        {
            SelectedAddonCharge = AddonChargeService.GetNewAddonCharge();
            AddAddonCommand = new Command(AddAddon, CanAddAddon);
            Messenger.Default.Register<InitializationCompleteMessage>(this, OnInitializationCompleteMessage);
            Messenger.Default.Register<SendGuidService>(this, OnChargeIdReceived, "ChargeIdSent");
        }

        public ICommand AddAddonCommand { get; }

        public AddonCharge SelectedAddonCharge
        {
            get => selectedAddonCharge;
            set
            {
                if (value == selectedAddonCharge) return;
                selectedAddonCharge = value;
                RaisePropertyChanged("Addon");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnInitializationCompleteMessage(InitializationCompleteMessage sent)
        {
            SendChargeId();
        }

        private void SendChargeId()
        {
            Messenger.Default.Send(new SendGuidService(SelectedAddonCharge.Id), "AddonChargeIdSent");
        }

        private void OnChargeIdReceived(SendGuidService sent)
        {
            if (StarterService.InitializationComplete)
            {
                SelectedAddonCharge = AddonChargeService.GetNewAddonCharge();
            }

            SelectedAddonCharge.AssociateChargeId(sent.Id);
            currentChargeGuid = sent.Id;
            RaisePropertyChanged("SelectedAddonCharge");
        }

        private void AddAddon(object obj)
        {
            if (SettingsService.ReuseSameAddonEnabled)
            {
                GetNewAddonDependentOnUserPromptPreference();
            }

            else
            {
                SelectedAddonCharge = AddonChargeService.GetNewAddonCharge();
            }


            SelectedAddonCharge.PrimaryChargeId = currentChargeGuid;
            SendChargeId();
            SelectedAddonCharge.AddToRepository();
            RaisePropertyChanged("SelectedAddonCharge");
            RaisePropertyChanged("CheckAmount");
        }

        private void GetNewAddonDependentOnUserPromptPreference()
        {
            if (SettingsService.AddonPromptEnabled)
            {
                PromptTypeOfNewAddon();
            }

            else
            {
                SelectedAddonCharge.Clone();
            }
        }

        private void PromptTypeOfNewAddon()
        {
            var dialogPrompt = new MessageBoxService(SelectedAddonCharge);

            SelectedAddonCharge = dialogPrompt.ShowDialog() ?
                AddonChargeService.Clone(SelectedAddonCharge) : AddonChargeService.GetNewAddonCharge();
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanAddAddon(object obj)
        {
            return !string.IsNullOrEmpty(SelectedAddonCharge.ProcedureCode);
        }
    }
}