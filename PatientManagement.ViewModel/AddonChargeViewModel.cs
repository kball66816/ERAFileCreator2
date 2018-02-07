using System;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class AddonChargeViewModel : INotifyPropertyChanged
    {
        private Guid currentChargeGuid;

        private bool initializationComplete;

        private AddonCharge selectedAddonCharge;

        private readonly bool SupressAddonDialog = false;

        public AddonChargeViewModel()
        {
            SelectedAddonCharge = new AddonCharge();
            AddAddonCommand = new Command(AddAddon, CanAddAddon);
            ChargeRepository = new AddonChargeRepository();
            Messenger.Default.Register<InitializationCompleteMessage>(this, OnInitializationCompleteMessage);
            Messenger.Default.Register<SendGuidService>(this, OnChargeIdReceived, "ChargeIdSent");
        }

        private IAddonChargeRepository ChargeRepository { get; }

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
            if (initializationComplete) SelectedAddonCharge = new AddonCharge();
            SelectedAddonCharge.PrimaryChargeId = sent.Id;
            currentChargeGuid = sent.Id;
            ChargeRepository.Add(SelectedAddonCharge);
            RaisePropertyChanged("SelectedAddonCharge");
            initializationComplete = true;
        }

        private void AddAddon(object obj)
        {
            if (SettingsService.ReuseSameAddonEnabled)
                GetNewAddonDependentOnUserPromptPreference();
            else
                SelectedAddonCharge = new AddonCharge();

            SelectedAddonCharge.PrimaryChargeId = currentChargeGuid;
            SendChargeId();
            ChargeRepository.Add(SelectedAddonCharge);
            RaisePropertyChanged("SelectedAddonCharge");
            RaisePropertyChanged("CheckAmount");
        }


        private void CloneLastAddon()
        {
            SelectedAddonCharge = new AddonCharge(SelectedAddonCharge);
            RaisePropertyChanged("SelectedAddonCharge");
        }

        private void GetNewAddonDependentOnUserPromptPreference()
        {
            if (SettingsService.AddonPromptEnabled)
                if (SupressAddonDialog == false)
                    PromptTypeOfNewAddon();

                else
                    return;

            else if (SettingsService.AddonPromptEnabled == false)
                CloneLastAddon();
        }

        private void PromptTypeOfNewAddon()
        {
            var dialogPrompt = new MessageBoxService(SelectedAddonCharge);

            if (dialogPrompt.ShowDialog())
                CloneLastAddon();

            else
                SelectedAddonCharge = new AddonCharge();

            return;
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