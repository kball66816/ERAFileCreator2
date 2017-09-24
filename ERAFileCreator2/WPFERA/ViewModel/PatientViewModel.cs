using Common.Common;
using EFC.BL;
using PatientManagement.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPFERA.Services;

namespace WPFERA.ViewModel
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
            Charge = new PrimaryCharge();
            LoadInsuranceCompany();

            patientRepository.Add(SelectedPatient);
            Patients = patientRepository.GetAllPatients();
            PlacesOfService = Charge.PlaceOfService.PlacesOfService;
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

        public PatientRepository patientRepository = new PatientRepository();

        public bool SupressAddonDialog { get; private set; }

        SettingsService Settings { get; set; }

        public Dictionary<string, string> InsuranceStates { get; set; }

        public Dictionary<string, string> PaymentTypes { get; set; }

        public Dictionary<string, string> PlacesOfService { get; set; }

        public Dictionary<string,string> PrimaryAdjustmentType { get; set; }

        public Dictionary<string, string> PrimaryAdjustmentReasonCodes { get; set; }


        public Dictionary<string,string> AddonAdjustmentType { get; set; }

        public Dictionary<string, string> AddonAdjustmentReasonCodes { get; set; }

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

        private PrimaryCharge charge;

        public PrimaryCharge Charge
        {
            get { return charge; }
            set
            {
                if (value != charge)

                {
                    charge = value;
                    RaisePropertyChanged("Charge");
                }
            }
        }

        public ICommand ReturnSelectedPatientChargeCommand { get; private set; }

        private void ReturnSelectedPatientCharge(object obj)
        {
            Charge = SelectedPatient.Charge;
            UpdateCheckAmount();
            RaisePropertyChanged("Charge");
        }

        private void MatchChargeToPatient()
        {
            if (IsChargeMatched(Charge))
            {
                return;
            }
            else
            {
                selectedPatient.Charge = Charge;
                MatchAddonToCharge();
            }
        }

        private bool IsChargeMatched(PrimaryCharge charge)
        {
            if (selectedPatient.Charge.Id == charge.Id)
            {
                return true;
            }

            else
            {
                return false;
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
            MatchAddonToCharge();
            MatchChargeToPatient();
            ReturnNewPatient();
            patientRepository.Add(SelectedPatient);
            UpdateCheckAmount();
            RaisePropertyChanged("CheckAmount");
            RefreshAllCounters();
            ReturnNewCharge();
        }

        private void ReturnNewCharge()
        {
            if (Settings.ReuseChargeForNextPatient)
            {
                charge = new PrimaryCharge(charge);
            }

            else
            {
                Charge = new PrimaryCharge();
            }
            RaisePropertyChanged("Charge");
        }

        private void MatchAdjustmentToCharge()
        {
            if (CanAddAdjustment(Adjustment))
            {
                AddChargeAdjustment(Charge);
            }

            else
            {
                return;
            }
        }

        private bool IsAddonMatched(AddonCharge Addon)
        {
            bool isMatchedAddon = false;

            foreach (AddonCharge addon in SelectedPatient.Charge.AddonChargeList)
            {
                if (addon.Id == this.Addon.Id)
                {
                    isMatchedAddon = true;
                }

            }
            return isMatchedAddon;
        }
        private void MatchAddonToCharge()
        {
            if (CanAddAddon(Addon))
            {
                if (IsAddonMatched(Addon))
                {
                    return;
                }

                else
                {
                    MatchAddonAdjustmentToAddon();
                    AddAddon(Charge);
                }
            }

            else
            {
                return;
            }
        }

        private void MatchAddonAdjustmentToAddon()
        {
            if (CanAddAddonAdjustment(AddonAdjustment))
            {
                AddAddonAdjustment(Addon);
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
                MessageBox.Show("Do you want to reuse this Patient?", "Return new patient", MessageBoxButton.YesNo);
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
            AddonCharge clone = (AddonCharge)Charge.AddonChargeList.Last().Clone();
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
            SelectedPatient = patientRepository.GetSelectedPatient(selectedPatient.BillId).CopyPatient();
            RaisePropertyChanged("SelectedPatient");
        }

        private AddonCharge PromptTypeOfNewAddon()
        {

            var newAddonDialogResult = MessageBox.Show("Do you want to reuse this Addon?", "Return new Addon", MessageBoxButton.YesNo);
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
            if (!string.IsNullOrEmpty(SelectedPatient.FirstName) && !string.IsNullOrEmpty(selectedPatient.BillId))
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
            { if (value != adjustment) { adjustment = value; RaisePropertyChanged("Adjustment"); } }
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
            Charge.AdjustmentList.Add(Adjustment);
            Adjustment = new Adjustment();
            RaisePropertyChanged("Adjustment");
            RefreshAllCounters();
        }

        private bool CheckIfAddonIsNull()
        {
            bool checkIfAddonIsNull = false;
            if (Charge.AddonChargeList.Count > 0)
            {
                checkIfAddonIsNull = CheckifLastAddonIsNull(checkIfAddonIsNull);
            }
            return checkIfAddonIsNull;
        }

        private bool CheckifLastAddonIsNull(bool checkIfAddonIsNull)
        {
            if (Charge.AddonChargeList.Last() != null)
            {
                checkIfAddonIsNull = true;
            }

            return checkIfAddonIsNull;
        }


        private bool CanAddAddonAdjustment(object obj)
        {
            if (!string.IsNullOrEmpty(addonAdjustment.AdjustmentReasonCode) && !string.IsNullOrEmpty(addonAdjustment.AdjustmentType))
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

        public AddonCharge Addon
        {
            get { return addon; }
            set
            {
                if (value != addon)
                {
                    addon = value; RaisePropertyChanged("Addon");
                }
            }
        }

        public ICommand AddAddonCommand { get; private set; }

        private void AddAddon(object obj)
        {
            Charge.AddonChargeList.Add(addon);

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

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void LoadCommands()
        {
            AddPatientCommand = new Command(AddPatient, CanAddPatient);
            AddChargeAdjustmentCommand = new Command(AddChargeAdjustment, CanAddAdjustment);
            AddAddonChargeAdjustmentCommand = new Command(AddAddonAdjustment, CanAddAddonAdjustment);
            AddAddonCommand = new Command(AddAddon, CanAddAddon);
            SaveFileCommand = new Command(Save, CanSave);
            UpdateRenderingProviderCommand = new Command(UpdateRenderingProvider);
            ReturnSelectedPatientChargeCommand = new Command(ReturnSelectedPatientCharge);
        }

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
            MatchAddonToCharge();
            MatchChargeToPatient();
            UpdateCheckAmount();
            SaveSettings();
            var edi = new ElectronicDataInterchange();

            var save = new SaveToFile();
            save.SaveFile(edi.BuildEdi(patients.ToList(), insurance, billingProvider));
        }




        private void UpdateCheckAmount()
        {

            decimal chargepaid = 0;
            decimal calc = 0;
            foreach (Patient patient in patientRepository.GetAllPatients())
            {
                chargepaid += patient.Charge.PaymentAmount;
                calc += patient.Charge.AddonChargeList.Sum(p => p.PaymentAmount);
                insurance.CheckAmount = chargepaid + calc;
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
            if (Charge.AddonChargeList.Count == 0)
            {
                AddonChargeAdjustmentCount = 0;
            }

            else if (Charge.AddonChargeList == null)
            {
                AddonChargeAdjustmentCount = 0;
            }

            else if (Charge.AddonChargeList.Last().AdjustmentList == null)
            {
                AddonChargeAdjustmentCount = 0;
            }

            else
            {
                AddonChargeAdjustmentCount = Charge.AddonChargeList.Last().AdjustmentList.Count();
                RaisePropertyChanged("AddonChargeAdjustmentCount");
            }
        }

        public int ChargeAdjustmentCount { get; private set; }

        private void UpdateChargeAdjustmentCount()
        {
            ChargeAdjustmentCount = Charge.AdjustmentList.Count;
            RaisePropertyChanged("ChargeAdjustmentCount");
        }

        public int AddonChargeAdjustmentCount { get; private set; }

        private void UpdateAddonCount()
        {
            AddonChargeCount = Charge.AddonChargeList.Count;
            RaisePropertyChanged("AddonChargeCount");
        }
    }
}
