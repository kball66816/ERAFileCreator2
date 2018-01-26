using System;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;

namespace PatientManagement.ViewModel
{
    public class AddonChargeViewModel : INotifyPropertyChanged
    {
        public AddonChargeViewModel()
        {
            SelectedAddonCharge = new AddonCharge();
            AddAddonCommand = new Command(AddAddon, CanAddAddon);
            ChargeRepository = new AddonChargeRepository();
            Messenger.Default.Register<InitializationCompleteMessage>(this, OnInitializationCompleteMessage);
            Messenger.Default.Register<SendGuidService>(this, OnChargeIdReceived, "ChargeIdSent");
        }

        private void OnInitializationCompleteMessage(InitializationCompleteMessage sent)
        {
            SendChargeId();
        }

        private void SendChargeId()
        {
            Messenger.Default.Send(new SendGuidService(SelectedAddonCharge.Id), "AddonChargeIdSent");
        }

        private IAddonChargeRepository ChargeRepository { get; set; }

        private Guid currentChargeGuid;

        private void OnChargeIdReceived(SendGuidService sent)
        {
            SelectedAddonCharge.PrimaryChargeId = sent.Id;
            currentChargeGuid = sent.Id;
            ChargeRepository.Add(SelectedAddonCharge);
        }

        private void AddAddon(object obj)
        {
            Messenger.Default.Send(new UpdateCalculations());
            if (SettingsService.ReuseSameAddonEnabled)
            {
                GetNewAddonDependentOnUserPromptPreference();
            }
            else
            {
                SelectedAddonCharge = new AddonCharge();
            }

            SelectedAddonCharge.PrimaryChargeId = currentChargeGuid;
            SendChargeId();
            ChargeRepository.Add(SelectedAddonCharge);
            RaisePropertyChanged("SelectedAddonCharge");
            RaisePropertyChanged("CheckAmount");
        }
       
        public ICommand AddAddonCommand { get; private set; }

        private AddonCharge selectedAddonCharge;

        public AddonCharge SelectedAddonCharge
        {
            get { return selectedAddonCharge; }
            set
            {
                if (value == selectedAddonCharge) return;
                selectedAddonCharge = value;
                RaisePropertyChanged("Addon");
            }
        }

        private bool SupressAddonDialog = false;


        private void CloneLastAddon()
        {
            SelectedAddonCharge = new AddonCharge(SelectedAddonCharge);
            RaisePropertyChanged("SelectedAddonCharge");
        }

        private void GetNewAddonDependentOnUserPromptPreference()
        {
            if (SettingsService.AddonPromptEnabled)
            {
                if (SupressAddonDialog == false)
                {
                    PromptTypeOfNewAddon();
                }

                else
                {
                    return;
                }
            }

            else if (SettingsService.AddonPromptEnabled == false)
            {
                CloneLastAddon();
            }
        }

        private void PromptTypeOfNewAddon()
        {
            var dialogPrompt = new DialogService(SelectedAddonCharge);

            if (dialogPrompt.ShowDialog())
            {
                CloneLastAddon();
            }

            else
            {
                SelectedAddonCharge = new AddonCharge();
            }

            return;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
