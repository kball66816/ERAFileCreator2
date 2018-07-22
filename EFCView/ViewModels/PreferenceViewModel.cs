using System.Windows.Input;
using Common.Common.Services;
using EraFileCreator.Service.Messaging;
using EraFileCreator.Services;
using PatientManagement.DAL;

namespace EraFileCreator.ViewModels
{
    public class PreferenceViewModel : BaseViewModel
    {
        private readonly ISettingsService _settingsService;
        private Preference _preference;

        public PreferenceViewModel()
        {
            this._settingsService = new SettingsService();
            this.Preference = new Preference();
            this.Preference = this._settingsService.PullDefaultPreferences(this._preference);
            this.LoadCommands();
        }

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

        private void SavePreference(object preference)
        {
            this._settingsService.SetDefaultPreferences(preference as Preference);
            this.PreferencesUpdated();
        }

        private void LoadCommands()
        {
            this.SavePreferenceCommand = new Command(this.SavePreference);
        }

        private void PreferencesUpdated()
        {
            Messenger.Default.Send(new PreferenceUpdatedMessage(), "Preferences Updated");
        }
    }
}