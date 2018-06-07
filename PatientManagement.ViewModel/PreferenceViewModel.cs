using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using PatientManagement.Model;
using PatientManagement.ViewModel.Service.Messaging;
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

        private void SavePreference(object preference)
        {
            this._settingsService.SetDefaultPreferences(preference as Preference);
            PreferencesUpdated();
        }

        private void LoadCommands()
        {
            this.SavePreferenceCommand = new Command(this.SavePreference);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PreferencesUpdated()
        {
            Messenger.Default.Send(new PreferenceUpdatedMessage(), "Preferences Updated");
        }
    }
}