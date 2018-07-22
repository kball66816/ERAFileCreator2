using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Common.Common;
using Common.Common.Services;
using EraFileCreator.Service.Messaging;
using EraFileCreator.Services;
using PatientManagement.DAL;

namespace EraFileCreator.ViewModels
{
    public class UpdateInsuranceCompaniesViewModel : BaseViewModel
    {
        private InsuranceCompany _insuranceCompany;
        private ObservableCollection<InsuranceCompany> _insuranceCompanies;
        private InsuranceCompany _insuranceCompanyInEdit;
        private Dictionary<string, string> _states;

        public UpdateInsuranceCompaniesViewModel()
        {
            this.InsuranceCompanyInEdit = new InsuranceCompany();
            this.InsuranceCompany = new InsuranceCompany();
            this.AddInsuranceCompanyCommand = new Command(this.AddInsuranceCompany, this.CanAddInsuranceCompany);
            this.SaveInsuranceCompaniesCommand = new Command(this.SaveInsuranceCompanies, this.CanSaveInsuranceCompanies);
            this.DeleteSelectedInsuranceCommand = new Command(this.DeleteSelectedInsurance, CanDeleteSelectedInsurance);
            this.SettingsService = new SettingsService();
            this.InsuranceCompanies =
                new ObservableCollection<InsuranceCompany>(SettingsService.GetInsuranceCompanies().OrderBy(a=>a.Name));
            var stateDictionary = new States();
            States = stateDictionary.StatesDictionary;

        }

        private void DeleteSelectedInsurance(object obj)
        {
            if (obj is InsuranceCompany insuranceCompany)
            {
                InsuranceCompanies.Remove(insuranceCompany);
            }
        }

        private bool CanDeleteSelectedInsurance(object obj)
        {
            return true;
        }

        private bool CanSaveInsuranceCompanies(object obj)
        {
            return this.InsuranceCompanies.Count > 0;
        }

        private void SaveInsuranceCompanies(object obj)
        {
            if (obj is IEnumerable<InsuranceCompany> insuranceCompanyList)
            {
                this.SettingsService.SaveInsuranceCompanies(insuranceCompanyList.ToList());
                Messenger.Default.Send(new UpdateInsuranceCompaniesMessage(), "NewInsuranceSaved");
                Messenger.Default.Send(new WindowMessenger(),"CloseWindow");
            }
        }

        private void AddInsuranceCompany(object obj)
        {
            var insuranceCompany = obj as InsuranceCompany;
            this.InsuranceCompanies.Add(insuranceCompany);
            this.InsuranceCompany = new InsuranceCompany();
        }

        private bool CanAddInsuranceCompany(object obj)
        {
            return !string.IsNullOrEmpty(this.InsuranceCompany.Name) && !string.IsNullOrEmpty(this.InsuranceCompany.TaxId);
        }

        public ICommand DeleteSelectedInsuranceCommand { get; set; }

        public InsuranceCompany InsuranceCompanyInEdit
        {
            get => this._insuranceCompanyInEdit;
            set
            {
                this._insuranceCompanyInEdit = value;
                RaisePropertyChanged("InsuranceCompanyInEdit");
            }
        }

        public ISettingsService SettingsService { get; set; }
        public ICommand AddInsuranceCompanyCommand { get; set; }
        public ICommand SaveInsuranceCompaniesCommand { get; set; }

        public Dictionary<string,string> States
        {
            get => this._states;
            set
            {
                this._states = value;
                RaisePropertyChanged("States");
            }
        }

        public ObservableCollection<InsuranceCompany> InsuranceCompanies
        {
            get => this._insuranceCompanies;
            set
            {
                this._insuranceCompanies = value;
                this.RaisePropertyChanged("InsuranceCompanies");
            }
        }

        public InsuranceCompany InsuranceCompany
        {
            get => this._insuranceCompany;
            set
            {
                this._insuranceCompany = value;
                this.RaisePropertyChanged("InsuranceCompany");
            }
        }

    }
}
