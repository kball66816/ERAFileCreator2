using System;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Configuration;
using System.Windows.Input;
using Common.Common.Services;

namespace PatientManagement.ViewModel
{
    public class AddonChargeViewModel : INotifyPropertyChanged
    {
        public AddonChargeViewModel()
        {
            SelectedAddonCharge = new AddonCharge();
            AddAddonCommand = new Command(AddAddonToCharge, CanAddAddon);
            Messenger.Default.Register<SendGuidService>(this, OnChargeIdReceived, "ChargeIdSent");
            Messenger.Default.Register<Adjustment>(this, OnAddonAdjustmentReceieved, "AddonChargeAdjustment");
            Messenger.Default.Register<ObservableCollection<AddonCharge>>(this, OnChargeCollectionReceived, "AddonList");
            DeleteSelectedAddonCommand = new Command(DeleteSelectedCharge, CanEditOrDeleteSelectedCharge);
            EditSelectedAddonCommand = new Command(EditSelectedCharge, CanEditOrDeleteSelectedCharge);
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

        private void EditSelectedCharge(object obj)
        {

            if (!editModeEnabled)
            {
                SelectedAddonCharge = SelectedAddonChargeIndex;
                RaisePropertyChanged("SelectedCharge");
                editModeEnabled = true;
            }
            else
            {
                GetNewAddonDependentOnUserPromptPreference();
                editModeEnabled = false;
            }
            SendAdjustmentList();
        }

        private bool CanEditOrDeleteSelectedCharge(object obj)
        {
            bool canEditOrDelete = (!string.IsNullOrEmpty(SelectedAddonChargeIndex?.ProcedureCode));
            return canEditOrDelete;


        }

        private void DeleteSelectedCharge(object obj)
        {
            if (SelectedAddonChargeIndex == null) return;
            var index = Charges.IndexOf(SelectedAddonChargeIndex);
            if (index <= -1) return;
            Charges.RemoveAt(index);
            RaisePropertyChanged("Charges");
            if (!editModeEnabled) return;
            editModeEnabled = false;
            SendAdjustmentList();
        }

        private ObservableCollection<AddonCharge> charges;

        public ObservableCollection<AddonCharge> Charges

        {
            get { return charges; }
            set
            {
                if (value == charges) return;
                charges = value;
                RaisePropertyChanged("Charges");
            }
        }
        private void OnChargeCollectionReceived(ObservableCollection<AddonCharge> chargesList)
        {
            Charges = chargesList;
            RaisePropertyChanged("Charges");
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


        private void AddAddonToCharge(object obj)
        {
            Messenger.Default.Send(SelectedAddonCharge, "AddonCharge");

            if (SettingsService.ReuseSameAddonEnabled)
            {
                GetNewAddonDependentOnUserPromptPreference();
            }
            else
            {
                SelectedAddonCharge = new AddonCharge();
            }

            SendAdjustmentList();

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

        private AddonCharge PromptTypeOfNewAddon()
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
            return SelectedAddonCharge;

        }

        private void OnAddonAdjustmentReceieved(Adjustment adjustment)
        {
            IAdjustmentRepository ar = new AdjustmentRepository(selectedAddonCharge);
            ar.Add(adjustment);
            RaisePropertyChanged("SelectedAddonCharge");
            SendAdjustmentList();

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
