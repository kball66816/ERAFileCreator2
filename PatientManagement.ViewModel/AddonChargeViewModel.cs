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
            Messenger.Default.Register<SendGuidService>(this, OnChargeIdReceived, "ChargeIdSent");
            ChargeRepository = new AddonChargeRepository();
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
            if (SettingsService.ReuseSameAddonEnabled)
            {
                GetNewAddonDependentOnUserPromptPreference();
            }
            else
            {
                SelectedAddonCharge = new AddonCharge();
            }
            RaisePropertyChanged("SelectedAddonCharge");
            RaisePropertyChanged("CheckAmount");
        }
        public ICommand DeleteSelectedAddonCommand { get; private set; }

        public ICommand EditSelectedAddonCommand { get; private set; }

        public AddonCharge SelectedAddonChargeIndex { get; set; }

        private bool editModeEnabled;

        private void SendAdjustmentList()
        {
            Messenger.Default.Send(SelectedAddonCharge.AdjustmentList, "AddonAdjustmentList");
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
            bool b = false;
            if (!editModeEnabled)
            {
                b = !string.IsNullOrEmpty(SelectedAddonCharge.ProcedureCode);

            }
            return b;
        }
    }
}
