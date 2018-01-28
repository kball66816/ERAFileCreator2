using System.ComponentModel;
using System.Windows.Input;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class PreferenceViewModel : INotifyPropertyChanged
    {
        private Preference preference;

        public SettingsService Settings;

        public PreferenceViewModel()
        {
            Preference = new Preference();
            Settings = new SettingsService();
            Preference = SettingsService.PullDefaultPreferences(preference);
            LoadCommands();
        }

        public Preference Preference
        {
            get => preference;
            set
            {
                if (value != preference)
                {
                    preference = value;
                    RaisePropertyChanged("Preference");
                }
            }
        }

        public ICommand SavePreferenceCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SavePreference(object obj)
        {
            SettingsService.SetDefaultPreferences(Preference);
        }

        private void LoadCommands()
        {
            SavePreferenceCommand = new Command(SavePreference);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}