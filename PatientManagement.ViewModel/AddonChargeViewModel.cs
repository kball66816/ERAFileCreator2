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

        }

        public SettingsService Settings { get; private set; }

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

        private bool SupressAddonDialog { get; set; }

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
            return !string.IsNullOrEmpty(SelectedAddonCharge.ProcedureCode);
        }
    }
}
