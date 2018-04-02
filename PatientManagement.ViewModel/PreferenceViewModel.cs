using System.ComponentModel;
using System.Windows.Input;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class PreferenceViewModel : INotifyPropertyChanged
    {
        private Preference _preference;

        public PreferenceViewModel()
        {
            this._settingsService = new SettingsService();
            this.Preference = new Preference();
            this.Preference = this._settingsService.PullDefaultPreferences(this._preference);
            this.LoadCommands();
        }

        private readonly ISettingsService _settingsService;
        public Preference Preference
        {
            get => this._preference;
            set
            {
                if (value != this._preference)
                {
                    this._preference = value;
                    this.RaisePropertyChanged("Preference");
                }
            }
        }

        public ICommand SavePreferenceCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SavePreference(object obj)
        {
            this._settingsService.SetDefaultPreferences(this.Preference);
        }

        private void LoadCommands()
        {
            this.SavePreferenceCommand = new Command(this.SavePreference);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}