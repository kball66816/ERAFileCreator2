using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Common.Common.Services;
using EraFileCreator.Service.Messaging;
using EraFileCreator.Services;
using PatientManagement.DAL;

namespace EraFileCreator.ViewModels
{
    public class UpdateInsuranceCompaniesViewModel : INotifyPropertyChanged
    {
        private InsuranceCompany _insuranceCompany;
        private ObservableCollection<InsuranceCompany> _insuranceCompanies;

        public UpdateInsuranceCompaniesViewModel()
        {
            this.InsuranceCompany = new InsuranceCompany();
            this.AddInsuranceCompanyCommand = new Command(this.AddInsuranceCompany, this.CanAddInsuranceCompany);
            this.SaveInsuranceCompaniesCommand = new Command(this.SaveInsuranceCompanies, this.CanSaveInsuranceCompanies);
            this.SettingsService = new SettingsService();
            this.InsuranceCompanies =
                new ObservableCollection<InsuranceCompany>(SettingsService.GetInsuranceCompanies());
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

        public ISettingsService SettingsService { get; set; }
        public ICommand AddInsuranceCompanyCommand { get; set; }
        public ICommand SaveInsuranceCompaniesCommand { get; set; }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
