using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EraView.Services;
using PatientManagement.Model;

namespace EraView.ViewModel
{
    public class PatientViewModel : INotifyPropertyChanged
    {
        public PatientViewModel()
        {
            Settings = new SettingsService();
            LoadInitialPatient();
            LoadBillingProvider();
            Addon = new AddonCharge();          
            Adjustment = new Adjustment();
            AddonAdjustment = new Adjustment();
            SelectedCharge = new PrimaryCharge();
            LoadInsuranceCompany();
            Pcvm = new PrimaryChargeViewModel();
            patientRepository.Add(SelectedPatient);
            Patients = patientRepository.GetAllPatients();
            Charges = selectedPatient.Charges;
            PlacesOfService = SelectedCharge.PlaceOfService.PlacesOfService;
            PrimaryAdjustmentReasonCodes = Adjustment.AdjustmentReasonCodes;
            AddonAdjustmentReasonCodes = AddonAdjustment.AdjustmentReasonCodes;
            PrimaryAdjustmentType = Adjustment.AdjustmentTypes;
            AddonAdjustmentType = addonAdjustment.AdjustmentTypes;
            LoadCommands();
            RefreshAllCounters();
        }



        private void LoadInsuranceCompany()
        {
            Insurance = new InsuranceCompany();
            Insurance = Settings.PullDefaultInsurance(Insurance);
            PaymentTypes = Insurance.PaymentTypes;
            InsuranceStates = Insurance.Address.States;
        }

        private void LoadBillingProvider()
        {
            BillingProvider = new Provider();
            BillingProvider = Settings.PullDefaultBillingProvider(BillingProvider);
            ProviderStates = BillingProvider.Address.States;
        }

        private void LoadInitialPatient()
        {
            SelectedPatient = new Patient();
            SelectedPatient = Settings.PullDefaultPatient();
        }

        private readonly PatientRepository patientRepository = new PatientRepository();

        private bool SupressAddonDialog { get; set; }

        private SettingsService Settings { get; set; }

        public Dictionary<string, string> InsuranceStates { get; set; }

        public Dictionary<string, string> PaymentTypes { get; set; }

        public Dictionary<string, string> PlacesOfService { get; set; }

        public Dictionary<string, string> PrimaryAdjustmentType { get; set; }

        public Dictionary<string, string> PrimaryAdjustmentReasonCodes { get; set; }

        public Dictionary<string, string> AddonAdjustmentType { get; set; }

        public Dictionary<string, string> AddonAdjustmentReasonCodes { get; set; }

        public ObservableCollection<PrimaryCharge> Charges

        {
            get { return charges; }
            set
            {
                if (value != charges)
                {
                    charges = value;
                    RaisePropertyChanged("Charges");
                }

            }
        }

        private InsuranceCompany insurance;

        public InsuranceCompany Insurance
        {
            get { return insurance; }
            set
            {
                if (value != insurance)
                {
                    insurance = value;
                    RaisePropertyChanged("Insurance");
                }
            }
        }

        public Dictionary<string, string> ProviderStates { get; set; }

        private Provider billingProvider;

        public Provider BillingProvider
        {
            get { return billingProvider; }
            set
            {
                if (value != billingProvider)
                {
                    billingProvider = value;
                    RaisePropertyChanged("BillingProvider");
                }
            }
        }

        private Patient selectedPatient;

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                if (value != selectedPatient)
                {
                    selectedPatient = value;
                    RaisePropertyChanged("SelectedPatient");
                }
            }
        }

        private PrimaryCharge selectedCharge;

        public PrimaryCharge SelectedCharge
        {
            get { return selectedCharge; }
            set
            {
                if (value != selectedCharge)

                {
                    selectedCharge = value;
                    RaisePropertyChanged("SelectedCharge");
                }
            }
        }

        public ICommand AddPatientCommand { get; private set; }

        /// <summary>
        /// Adds a new patient to the list by ensuring all unmatched details
        /// in the form match to the previous patient then returning a new instance
        /// of a patient
        /// </summary>
        /// <param name="obj"></param>
        private void AddPatient(object obj)
        {
            SupressAddonDialog = true;
            MatchAdjustmentToCharge();
            // MatchUnaddedChargeToSelectedPatient();
            AddChargeToPatientEncounter();
            ReturnNewPatient();
            patientRepository.Add(SelectedPatient);
            UpdateCheckAmount();
            RaisePropertyChanged("CheckAmount");
            RefreshAllCounters();
            ReturnNewCharge();
        }

        //private void MatchUnaddedChargeToSelectedPatient()
        //{
        //    selectedPatient.Charges = Pcvm.UnaddedCharges.ToList();
        //    Pcvm = new PrimaryChargeViewModel();
        //    RaisePropertyChanged("UnaddedCharges");
        //}

        private void AddChargeToPatientEncounter()
        {
            selectedPatient.Charges.Add(selectedCharge);
        }

        private void ReturnNewCharge()
        {
            if (Settings.ReuseChargeForNextPatient)
            {
                SelectedCharge = new PrimaryCharge(SelectedCharge);
            }

            else
            {
                SelectedCharge = new PrimaryCharge();
            }
            RaisePropertyChanged("SelectedCharge");
        }

        private void MatchAdjustmentToCharge()
        {
            if (CanAddAdjustment(Adjustment))
            {
                AddChargeAdjustment(SelectedCharge);
            }

            else
            {
                return;
            }
        }


        private void ReturnNewPatient()
        {
            if (Settings.ReuseSamePatientEnabled)
            {
                GetNewPatientDependentOnUserPromptPreference();
            }

            else if (Settings.ReuseSamePatientEnabled == false)
            {
                SelectedPatient = new Patient();

            }
            RaisePropertyChanged("Patient");
        }

        private void GetNewPatientDependentOnUserPromptPreference()
        {
            if (Settings.PatientPromptEnabled)
            {
                PromptTypeOfNewPatient();
            }

            else if (Settings.PatientPromptEnabled == false)
            {
                CloneSelectedPatient();
            }
        }

        private Patient PromptTypeOfNewPatient()
        {
            var newPatientDialogResult =
                MessageBox.Show("Do you want to add additional encounters to this patient?", "Return new patient", MessageBoxButton.YesNo);
            {

                if (newPatientDialogResult == MessageBoxResult.Yes)
                {
                    CloneSelectedPatient();
                    return SelectedPatient;
                }

                else
                {
                    SelectedPatient = new Patient();
                    return selectedPatient;
                }
            }
        }

        private void CloneLastAddon()
        {
            AddonCharge clone = (AddonCharge) SelectedCharge.AddonChargeList.Last().Clone();
            Addon = clone;
            RaisePropertyChanged("Addon");
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

        private void CloneSelectedPatient()
        {

            selectedPatient = patientRepository.GetSelectedPatient(selectedPatient.Id).CopyPatient();
            RaisePropertyChanged("SelectedPatient");
        }

        private AddonCharge PromptTypeOfNewAddon()
        {

            var newAddonDialogResult = MessageBox.Show("Do you want to reuse this Addon?", "Return new Addon",
                MessageBoxButton.YesNo);
            {

                if (newAddonDialogResult == MessageBoxResult.Yes)
                {
                    CloneLastAddon();

                }

                else
                {
                    Addon = new AddonCharge();
                }

                return Addon;
            }
        }


        private ObservableCollection<Patient> patients;

        public ObservableCollection<Patient> Patients
        {
            get { return patients; }
            private set
            {
                patients = value;
                RaisePropertyChanged("Patients");
            }
        }

        private bool CanAddPatient(object obj)
        {
            if (!string.IsNullOrEmpty(SelectedPatient.FirstName) && !string.IsNullOrEmpty(selectedPatient.LastName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Adjustment adjustment;

        public Adjustment Adjustment
        {
            get { return adjustment; }
            set
            {
                if (value != adjustment)
                {
                    adjustment = value;
                    RaisePropertyChanged("Adjustment");
                }
            }
        }

        private Adjustment addonAdjustment;

        public Adjustment AddonAdjustment
        {
            get { return addonAdjustment; }
            set
            {
                if (value != addonAdjustment)
                {
                    addonAdjustment = value;
                    RaisePropertyChanged("AddonAdjustment");
                }
            }
        }

        public ICommand AddChargeAdjustmentCommand { get; private set; }

        public ICommand AddAddonChargeAdjustmentCommand { get; private set; }

        private void AddAddonAdjustment(object obj)
        {
            Addon.AdjustmentList.Add(AddonAdjustment);
            AddonAdjustment = new Adjustment();
            RaisePropertyChanged("Adjustment");
            RefreshAllCounters();
        }

        private void AddChargeAdjustment(object obj)
        {
            SelectedCharge.AdjustmentList.Add(Adjustment);
            Adjustment = new Adjustment();
            RaisePropertyChanged("Adjustment");
            RefreshAllCounters();
        }


        private bool CanAddAddonAdjustment(object obj)
        {
            if (!string.IsNullOrEmpty(addonAdjustment.AdjustmentReasonCode) &&
                !string.IsNullOrEmpty(addonAdjustment.AdjustmentType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanAddAdjustment(object obj)
        {
            if (Adjustment.AdjustmentReasonCode != null && Adjustment.AdjustmentType != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private AddonCharge addon;
        private PrimaryChargeViewModel pcvm;
        private ObservableCollection<PrimaryCharge> charges;

        public AddonCharge Addon
        {
            get { return addon; }
            set
            {
                if (value != addon)
                {
                    addon = value;
                    RaisePropertyChanged("Addon");
                }
            }
        }

        public ICommand AddAddonCommand { get; private set; }

        private void AddAddon(object obj)
        {
            SelectedCharge.AddonChargeList.Add(addon);

            if (Settings.ReuseSameAddonEnabled)
            {
                GetNewAddonDependentOnUserPromptPreference();
            }

            else if (Settings.ReuseSameAddonEnabled == false)
            {
                Addon = new AddonCharge();

            }
            RaisePropertyChanged("Addon");
            UpdateCheckAmount();
            RaisePropertyChanged("CheckAmount");
            RefreshAllCounters();
        }

        private bool CanAddAddon(object obj)
        {
            if (!string.IsNullOrEmpty(Addon.ProcedureCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ICommand SaveFileCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadCommands()
        {
            AddPatientCommand = new Command(AddPatient, CanAddPatient);
            AddChargeAdjustmentCommand = new Command(AddChargeAdjustment, CanAddAdjustment);
            AddAddonChargeAdjustmentCommand = new Command(AddAddonAdjustment, CanAddAddonAdjustment);
            AddAddonCommand = new Command(AddAddon, CanAddAddon);
            SaveFileCommand = new Command(Save, CanSave);
            UpdateRenderingProviderCommand = new Command(UpdateRenderingProvider);
            // AddChargeToRepositoryCommand = new Command(AddChargeToRepository);
            AddChargeToPatientCommand = new Command(AddChargeToPatient, CanAddChargeToPatient);
        }

        public ICommand AddChargeToPatientCommand { get; set; }
        //public ICommand AddChargeToRepositoryCommand { get; set; }

        public ICommand UpdateRenderingProviderCommand { get; private set; }

        private void UpdateRenderingProvider(object obj)
        {
            if (BillingProvider.IsAlsoRendering)
            {
                selectedPatient.RenderingProvider.FirstName = BillingProvider.FirstName;
                selectedPatient.RenderingProvider.LastName = BillingProvider.LastName;
                selectedPatient.RenderingProvider.Npi = BillingProvider.Npi;
                RaisePropertyChanged("Patient");
            }

            else if (billingProvider.IsAlsoRendering == false)
            {
                return;
            }
        }

        private bool CanSave(object obj)
        {
            return true;
        }

        private void SaveSettings()
        {
            Settings.SetDefaultBillingProvider(billingProvider);
            Settings.SetDefaultInsurance(insurance);
            Settings.SetDefaultPatient(selectedPatient);
        }

        private void Save(object obj)
        {
            SupressAddonDialog = true;
            MatchAdjustmentToCharge();
            //   MatchAddonToCharge();
            //   MatchChargeToPatient();
            UpdateCheckAmount();
            SaveSettings();
            //var edi = new ElectronicDataInterchange();

            //var save = new SaveToFile();
            // save.SaveFile(edi.BuildEdi(patients.ToList(), insurance, billingProvider));
        }




        private void UpdateCheckAmount()
        {

            decimal chargesPaidAmount = 0;
            decimal addonsPaidAmount = 0;
            foreach (Patient patient in patientRepository.GetAllPatients())
            {
                foreach (PrimaryCharge charge in patient.Charges)
                {
                    chargesPaidAmount += charge.PaymentAmount;
                    addonsPaidAmount += charge.AddonChargeList.Sum(p => p.PaymentAmount);
                    insurance.CheckAmount = chargesPaidAmount + addonsPaidAmount;
                }

            }

        }

        public decimal PatientCount { get; private set; }

        private void RefreshAllCounters()
        {
            UpdatePatientCount();
            UpdateAddonCount();
            UpdateChargeAdjustmentCount();
            UpdateAddonAdjustmentCount();
        }

        private void UpdatePatientCount()
        {
            PatientCount = Patients.Count();
            RaisePropertyChanged("PatientCount");
        }

        public decimal AddonChargeCount { get; private set; }

        private void UpdateAddonAdjustmentCount()
        {
            if (SelectedCharge.AddonChargeList.Count == 0)
            {
                AddonChargeAdjustmentCount = 0;
            }

            else if (SelectedCharge.AddonChargeList == null)
            {
                AddonChargeAdjustmentCount = 0;
            }

            else if (SelectedCharge.AddonChargeList.Last().AdjustmentList == null)
            {
                AddonChargeAdjustmentCount = 0;
            }

            else
            {
                AddonChargeAdjustmentCount = SelectedCharge.AddonChargeList.Last().AdjustmentList.Count();
                RaisePropertyChanged("AddonChargeAdjustmentCount");
            }
        }

        public int ChargeAdjustmentCount { get; private set; }

        private void UpdateChargeAdjustmentCount()
        {
            ChargeAdjustmentCount = SelectedCharge.AdjustmentList.Count;
            RaisePropertyChanged("ChargeAdjustmentCount");
        }

        public int AddonChargeAdjustmentCount { get; private set; }

        private void UpdateAddonCount()
        {
            AddonChargeCount = SelectedCharge.AddonChargeList.Count;
            RaisePropertyChanged("AddonChargeCount");
        }

        public PrimaryChargeViewModel Pcvm

        {
            get { return pcvm; }
            set
            {
                if (value != pcvm)
                {
                    pcvm = value;
                    RaisePropertyChanged("Pcvm");
                }
            }
        }

        //private void AddChargeToRepository(object obj)
        //{
        //    Pcvm.ChargeRepository.Add(selectedCharge);
        //    selectedCharge = new PrimaryCharge();
        //    RaisePropertyChanged("SelectedCharge");
        //}

        private void AddChargeToPatient(object obj)
        {
            selectedPatient.Charges.Add(selectedCharge);
            selectedCharge = new PrimaryCharge();
            RaisePropertyChanged("SelectedCharge");
            RaisePropertyChanged("Charges");
        }

        private bool CanAddChargeToPatient(object obj)
        {
            bool b = selectedCharge.ChargeCost > 0 && !string.IsNullOrEmpty(selectedCharge.ProcedureCode);

            return b;
        }
    }
}
