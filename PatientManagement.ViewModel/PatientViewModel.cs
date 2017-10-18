using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Common.Common;
using EFC.BL;
using PatientManagement.ViewModel.Services;
using PatientManagement.Model;
using PatientManagement.DAL;
using System;

namespace PatientManagement.ViewModel
{
    public class PatientViewModel : INotifyPropertyChanged
    {
        public PatientViewModel()
        {

            Settings = new SettingsService();
            LoadInitialPatient();
            //SelectedAddonCharge = new AddonCharge();
            SelectedAdjustment = new Adjustment();
            //AddonAdjustment = new Adjustment();
            SelectedCharge = new PrimaryCharge();

            patientRepository.Add(SelectedPatient);
            Patients = patientRepository.GetAllPatients();
            Charges = selectedPatient.Charges;
            Adjustments = selectedCharge.AdjustmentList;
            PlacesOfService = selectedCharge.PlaceOfService.PlacesOfService;
            PrimaryAdjustmentReasonCodes = selectedAdjustment.AdjustmentReasonCodes;
            //AddonAdjustmentReasonCodes = AddonAdjustment.AdjustmentReasonCodes;
            PrimaryAdjustmentType = SelectedAdjustment.AdjustmentTypes;
            //AddonAdjustmentType = addonAdjustment.AdjustmentTypes;
            LoadCommands();
            RefreshAllCounters();

            //Messenger.Default.Register<Adjustment>(this, OnAddonAdjustmentReceieved);
            Messenger.Default.Register<Patient>(this, OnPatientReceived);
        }

        //private void OnAddonAdjustmentReceieved(Adjustment adjustment)
        //{
        //    selectedAddonCharge.AdjustmentList.Add(adjustment);
        //}


        private void OnPatientReceived(Patient patient)
        {
            SelectedPatient = patient;
            RaisePropertyChanged("SelectedPatient");
        }


        private void LoadInitialPatient()
        {
            SelectedPatient = new Patient();
            SelectedPatient = Settings.PullDefaultPatient();
        }

        private IPatientRepository patientRepository = new PatientRepository();

        private bool SupressAddonDialog { get; set; }

        private SettingsService Settings { get; set; }



        public Dictionary<string, string> PlacesOfService { get; set; }

        public Dictionary<string, string> PrimaryAdjustmentType { get; set; }

        public Dictionary<string, string> PrimaryAdjustmentReasonCodes { get; set; }

        //    public Dictionary<string, string> AddonAdjustmentType { get; set; }

        //  public Dictionary<string, string> AddonAdjustmentReasonCodes { get; set; }

        public ObservableCollection<PrimaryCharge> Charges

        {
            get { return charges; }
            set
            {
                if (value == charges) return;
                charges = value;
                RaisePropertyChanged("Charges");
            }
        }

        private InsuranceCompany insuranceCompany;

        public InsuranceCompany InsuranceCompany

        {
            get { return insuranceCompany; }
            set
            {
                if (value != insuranceCompany)
                {
                    insuranceCompany = value;
                    RaisePropertyChanged("InsuranceCompany");

                }
            }
        }


        private Patient selectedPatient;

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                if (value == selectedPatient) return;
                selectedPatient = value;
                RaisePropertyChanged("SelectedPatient");
            }
        }

        private PrimaryCharge selectedCharge;

        public PrimaryCharge SelectedCharge
        {
            get { return selectedCharge; }
            set
            {
                if (value == selectedCharge) return;
                selectedCharge = value;
                RaisePropertyChanged("SelectedCharge");
            }
        }

        public PrimaryCharge SelectedListChargeIndex { get; set; }

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
            // MatchAdjustmentToCharge();
            //AddChargeToPatientEncounter();
            ReturnNewPatient();
            patientRepository.Add(SelectedPatient);
            //UpdateCheckAmount();
            RaisePropertyChanged("CheckAmount");
            RefreshAllCounters();
        }

        private void ReturnNewCharge()
        {
            SelectedCharge = Settings.ReuseChargeForNextPatient
                ? new PrimaryCharge(SelectedCharge)
                : new PrimaryCharge();
            RaisePropertyChanged("SelectedCharge");
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
                MessageBox.Show("Do you want to add additional encounters to this patient?", "Return new patient",
                    MessageBoxButton.YesNo);
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

        //private void CloneLastAddon()
        //{
        //    var clone = (AddonCharge)SelectedCharge.AddonChargeList.Last().Clone();
        //    SelectedAddonCharge = clone;
        //    RaisePropertyChanged("Addon");
        //}

        //private void GetNewAddonDependentOnUserPromptPreference()
        //{
        //    if (Settings.AddonPromptEnabled)
        //    {
        //        if (SupressAddonDialog == false)
        //        {
        //            PromptTypeOfNewAddon();
        //        }

        //        else
        //        {
        //            return;
        //        }
        //    }

        //    else if (Settings.AddonPromptEnabled == false)
        //    {
        //        CloneLastAddon();
        //    }
        //}

        private void CloneSelectedPatient()
        {

            selectedPatient = patientRepository.GetSelectedPatient(selectedPatient.Id).CopyPatient();
            RaisePropertyChanged("SelectedPatient");
        }

        //private AddonCharge PromptTypeOfNewAddon()
        //{

        //    var newAddonDialogResult = MessageBox.Show("Do you want to reuse this Addon?", "Return new Addon",
        //        MessageBoxButton.YesNo);
        //    {

        //        if (newAddonDialogResult == MessageBoxResult.Yes)
        //        {
        //            CloneLastAddon();

        //        }

        //        else
        //        {
        //            SelectedAddonCharge = new AddonCharge();
        //        }

        //        return SelectedAddonCharge;
        //    }
        //}


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
            return !string.IsNullOrEmpty(SelectedPatient.FirstName)
                && !string.IsNullOrEmpty(selectedPatient.LastName);
        }

        private Adjustment selectedAdjustment;

        public Adjustment SelectedAdjustment
        {
            get { return selectedAdjustment; }
            set
            {
                if (value == selectedAdjustment) return;
                selectedAdjustment = value;
                RaisePropertyChanged("SelectedAdjustment");
            }
        }

        public Adjustment SelectedChargeAdjustmentIndex { get; set; }

        private ObservableCollection<Adjustment> adjustments;

        public ObservableCollection<Adjustment> Adjustments
        {
            get { return adjustments; }
            set
            {
                if (value == adjustments) return;
                adjustments = value;
                RaisePropertyChanged("Adjustments");
            }
        }

        //private Adjustment addonAdjustment;

        //public Adjustment AddonAdjustment
        //{
        //    get { return addonAdjustment; }
        //    set
        //    {
        //        if (value == addonAdjustment) return;
        //        addonAdjustment = value;
        //        RaisePropertyChanged("AddonAdjustment");
        //    }
        //}

        public ICommand AddChargeAdjustmentCommand { get; private set; }

        public ICommand AddAddonChargeAdjustmentCommand { get; private set; }

        //private void AddAddonAdjustment(object obj)
        //{
        //    SelectedAddonCharge.AdjustmentList.Add(AddonAdjustment);
        //    AddonAdjustment = new Adjustment();
        //    RaisePropertyChanged("SelectedAdjustment");
        //    RefreshAllCounters();
        //}

        private void AddAdjustmentToCharge(object obj)
        {
            IAdjustmentRepository adjustmentRepository = new AdjustmentRepository(SelectedCharge);
            adjustmentRepository.Add(SelectedAdjustment);
            SelectedAdjustment = new Adjustment();
            RaisePropertyChanged("SelectedAdjustment");
            RefreshAllCounters();
        }

        //private bool CanAddAddonAdjustment(object obj)
        //{
        //    return !string.IsNullOrEmpty(addonAdjustment.AdjustmentReasonCode) &&
        //           !string.IsNullOrEmpty(addonAdjustment.AdjustmentType);
        //}

        private bool CanAddAdjustment(object obj)
        {
            return !string.IsNullOrEmpty(SelectedAdjustment.AdjustmentReasonCode) &&
                   SelectedAdjustment.AdjustmentAmount > 0;
        }


        private ObservableCollection<PrimaryCharge> charges;

        //private AddonCharge selectedAddonCharge;

        //public AddonCharge SelectedAddonCharge
        //{
        //    get { return selectedAddonCharge; }
        //    set
        //    {
        //        if (value == selectedAddonCharge) return;
        //        selectedAddonCharge = value;
        //        RaisePropertyChanged("Addon");
        //    }
        //}

        public AddonCharge SelectedAddonChargeIndex { get; set; }

        public ICommand AddAddonCommand { get; private set; }

        //private void AddAddonToCharge(object obj)
        //{

        //    IAddonChargeRepository addonChargeRepository = new AddonChargeRepository(selectedCharge);
        //    addonChargeRepository.Add(SelectedAddonCharge);

        //    if (Settings.ReuseSameAddonEnabled)
        //    {
        //        GetNewAddonDependentOnUserPromptPreference();
        //    }
        //    else
        //    {
        //        SelectedAddonCharge = new AddonCharge();
        //    }

        //    Messenger.Default.Send(new UpdateCalculations());
        //    RaisePropertyChanged("SelectedAddonCharge");
        //    //UpdateCheckAmount();
        //    RaisePropertyChanged("CheckAmount");
        //    RefreshAllCounters();

        //}

        //private bool CanAddAddon(object obj)
        //{
        //    return !string.IsNullOrEmpty(SelectedAddonCharge.ProcedureCode);
        //}

        public ICommand SaveFileCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadCommands()
        {
            AddPatientCommand = new Command(AddPatient, CanAddPatient);
            AddChargeAdjustmentCommand = new Command(AddAdjustmentToCharge, CanAddAdjustment);
            // AddAddonChargeAdjustmentCommand = new Command(AddAddonAdjustment, CanAddAddonAdjustment);
            //AddAddonCommand = new Command(AddAddonToCharge, CanAddAddon);
            SaveFileCommand = new Command(Save, CanSave);
            UpdateRenderingProviderCommand = new Command(UpdateRenderingProvider);
            AddChargeToPatientCommand = new Command(AddChargeToPatientV2, CanAddChargeToPatient);
            DeleteSelectedChargeCommand = new Command(DeleteSelectedCharge, CanEditOrDeleteSelectedCharge);
            EditSelectedChargeCommand = new Command(EditSelectedCharge, CanEditOrDeleteSelectedCharge);
            EditSelectedAdjustmentCommand = new Command(EditSelectedAdjustment, CanEditSelectedAdjustment);
            DeleteSelectedAdjustmentCommand = new Command(DeleteSelectedAdjustment, CanDeleteSelectedAdjustment);
            //DeleteSelectedAddonCommand = new Command(DeleteSelectedAddon, CanDeleteSelectedAddon);
            //EditSelectedAddonCommand = new Command(EditSelectedAddon, CanEditSelectedAddon);

        }

        public ICommand AddChargeToPatientCommand { get; set; }

        public ICommand UpdateRenderingProviderCommand { get; private set; }

        private void UpdateRenderingProvider(object obj)
        {

            Messenger.Default.Send(selectedPatient.RenderingProvider);
            //IProvider pr = new BillingProviderRepository();

            //if (pr.GetBillingProvider().IsAlsoRendering)
            //{
            //    selectedPatient.RenderingProvider.FirstName = pr.GetBillingProvider().FirstName;
            //    selectedPatient.RenderingProvider.LastName = pr.GetBillingProvider().LastName;
            //    selectedPatient.RenderingProvider.Npi = pr.GetBillingProvider().Npi;
            //}

            //if (BillingProvider.IsAlsoRendering)
            //{
            //selectedPatient.RenderingProvider.FirstName = BillingProvider.FirstName;
            //selectedPatient.RenderingProvider.LastName = BillingProvider.LastName;
            //selectedPatient.RenderingProvider.Npi = BillingProvider.Npi;
            //RaisePropertyChanged("Patient");
            //}

            //else if (billingProvider.IsAlsoRendering == false)
            //{
            //return;
            //}
        }

        private static bool CanSave(object obj)
        {
            return true;
        }

        private void SaveSettings()
        {
            //Settings.SetDefaultBillingProvider(billingProvider);
            //Settings.SetDefaultInsurance(insuranceCompany);
            Settings.SetDefaultPatient(selectedPatient);
        }

        private void Save(object obj)
        {

            Messenger.Default.Send(new UpdateRepositoriesMessage());
            SupressAddonDialog = true;
            //  MatchAdjustmentToCharge();
            //   MatchAddonToCharge();
            //   MatchChargeToPatient();
            //  UpdateCheckAmount();
            SaveSettings();

            //SaveProviderToRepository();
            //SaveInsuranceToRepository();

            var edi = new UpdatedEdi();

            var save = new SaveToFile();

            save.SaveFile(edi.Create835File());
        }


        //private void SaveProviderToRepository()
        //{
        //    IProvider saveProvider = new BillingProviderRepository();
        //    saveProvider.AddBillingProvider(billingProvider);
        //}

        //private void UpdateCheckAmount()
        //{

        //    decimal chargesPaidAmount = 0;
        //    decimal addonsPaidAmount = 0;
        //    foreach (var patient in patientRepository.GetAllPatients())
        //    {
        //        foreach (var charge in patient.Charges)
        //        {
        //            chargesPaidAmount += charge.PaymentAmount;
        //            addonsPaidAmount += charge.AddonChargeList.Sum(p => p.PaymentAmount);
        //            insuranceCompany.CheckAmount = chargesPaidAmount + addonsPaidAmount;
        //        }

        //    }

        //}

        public decimal PatientCount { get; private set; }

        private void RefreshAllCounters()
        {
            UpdatePatientCount();
            //UpdateAddonCount();
            //UpdateChargeAdjustmentCount();
            // UpdateAddonAdjustmentCount();
        }

        private void UpdatePatientCount()
        {
            PatientCount = Patients.Count();
            RaisePropertyChanged("PatientCount");
        }


        private void AddChargeToPatientV2(object obj)
        {

            var chargeRepository = new PrimaryChargeRepository(selectedPatient);
            chargeRepository.Add(SelectedCharge);
            ReturnNewCharge();

            Messenger.Default.Send(new UpdateCalculations());
            RaisePropertyChanged("SelectedCharge");
            RaisePropertyChanged("Charges");
        }

        private bool CanAddChargeToPatient(object obj)
        {
            bool b = false;
            if (!editModeEnabled)
            {
                b = SelectedCharge.ChargeCost > 0 && !string.IsNullOrEmpty(SelectedCharge.ProcedureCode);

            }

            return b;
        }

        public ICommand DeleteSelectedChargeCommand { get; private set; }


        private void DeleteSelectedCharge(object obj)
        {
            if (SelectedListChargeIndex == null) return;
            var index = selectedPatient.Charges.IndexOf(SelectedListChargeIndex);
            if (index <= -1) return;
            IPrimaryChargeRepository cp = new PrimaryChargeRepository(SelectedPatient);
            cp.Delete(SelectedListChargeIndex);
            if (editModeEnabled)
            {
                editModeEnabled = false;
            }
        }

        public ICommand EditSelectedChargeCommand { get; private set; }

        private bool editModeEnabled;

        private void EditSelectedCharge(object obj)
        {
            if (!editModeEnabled)
            {
                SelectedCharge = SelectedListChargeIndex;
                RaisePropertyChanged("SelectedCharge");
                editModeEnabled = true;

            }
            else
            {
                ReturnNewCharge();
                editModeEnabled = false;
            }

        }

        private bool CanEditOrDeleteSelectedCharge(object obj)
        {
            bool b = !string.IsNullOrEmpty(SelectedListChargeIndex?.ProcedureCode);

            return b;
        }

        public ICommand EditSelectedAdjustmentCommand { get; private set; }

        private void EditSelectedAdjustment(object obj)
        {

            selectedAdjustment = SelectedChargeAdjustmentIndex;
            RaisePropertyChanged("SelectedAdjustment");


        }

        private bool CanEditSelectedAdjustment(object obj)
        {
            bool b = editModeEnabled && (!string.IsNullOrEmpty(SelectedChargeAdjustmentIndex?.AdjustmentReasonCode));
            return b;

        }

        public ICommand DeleteSelectedAdjustmentCommand { get; set; }

        private void DeleteSelectedAdjustment(object obj)
        {
            if (SelectedChargeAdjustmentIndex == null) return;
            var index = SelectedCharge.AdjustmentList.IndexOf(SelectedChargeAdjustmentIndex);
            if (index <= -1) return;
            IAdjustmentRepository ar = new AdjustmentRepository(selectedCharge);
            ar.Delete(SelectedChargeAdjustmentIndex);
        }

        private bool CanDeleteSelectedAdjustment(object obj)
        {
            bool b = !string.IsNullOrEmpty(SelectedChargeAdjustmentIndex?.AdjustmentReasonCode);
            return b;
        }
        public ICommand DeleteSelectedAddonCommand { get; set; }

        //private void DeleteSelectedAddon(object obj)
        //{
        //    if (SelectedAddonChargeIndex == null) return;
        //    var index = SelectedCharge.AddonChargeList.IndexOf(SelectedAddonChargeIndex);
        //    if (index <= -1) return;
        //    IAddonChargeRepository ar = new AddonChargeRepository(selectedCharge);
        //    ar.Delete(SelectedAddonChargeIndex);
        //}

        //private bool CanDeleteSelectedAddon(object obj)
        //{
        //    bool b = !string.IsNullOrEmpty(SelectedAddonChargeIndex?.ProcedureCode);
        //    return b;
        //}
        //public ICommand EditSelectedAddonCommand { get; private set; }

        //private void EditSelectedAddon(object obj)
        //{
        //    SelectedAddonCharge = SelectedAddonChargeIndex;
        //    RaisePropertyChanged("SelectedAddonCharge");
        //}

        //private bool CanEditSelectedAddon(object obj)
        //{
        //    bool b = editModeEnabled && (!string.IsNullOrEmpty(SelectedAddonChargeIndex?.ProcedureCode));
        //    return b;

        //}
    }
}
