using System.Collections.ObjectModel;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class AddonChargeViewModel : INotifyPropertyChanged
    {
        public AddonChargeViewModel()
        {
            SelectedAddonCharge = new AddonCharge();
            Settings = new SettingsService();
            AddAddonCommand = new Command(AddAddonToCharge, CanAddAddon);
            Messenger.Default.Register<Adjustment>(this, OnAddonAdjustmentReceieved, "AddonCharge");
            Messenger.Default.Register<ObservableCollection<AddonCharge>>(this,OnChargeCollectionReceived,"AddonList");
            DeleteSelectedAddonCommand = new Command(DeleteSelectedCharge, CanEditOrDeleteSelectedCharge);
            EditSelectedAddonCommand = new Command(EditSelectedCharge, CanEditOrDeleteSelectedCharge);

        }

        public ICommand DeleteSelectedAddonCommand { get; private set; }

        public ICommand EditSelectedAddonCommand { get; private set; }

        public AddonCharge SelectedAddonChargeIndex { get; set; }

        bool editModeEnabled;
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

        private SettingsService Settings { get; set; }

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

            if (Settings.ReuseSameAddonEnabled)
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

        private bool SupressAddonDialog = false;

        private void CloneLastAddon()
        {
            SelectedAddonCharge = new AddonCharge(SelectedAddonCharge);
            RaisePropertyChanged("SelectedAddonCharge");
        }

        private void GetNewAddonDependentOnUserPromptPreference()
        {
            if (Settings.AddonPromptEnabled)
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

            else if (Settings.AddonPromptEnabled == false)
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
                b= !string.IsNullOrEmpty(SelectedAddonCharge.ProcedureCode);

            }
            return b;
        }
    }
}
