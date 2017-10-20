using PatientManagement.Model;
using PatientManagement.ViewModel.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class PreferenceViewModel : INotifyPropertyChanged
    {
        public PreferenceViewModel()
        {
            Preference = new Preference();
            Settings = new SettingsService();
            Preference = SettingsService.PullDefaultPreferences(preference);
            LoadCommands();
        }

        public SettingsService Settings;

        private Preference preference;

        public Preference Preference
        {
            get { return preference; }
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

        private void SavePreference(object obj)
        {
            SettingsService.SetDefaultPreferences(Preference);
        }

        private void LoadCommands()
        {
            SavePreferenceCommand = new Command(SavePreference);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
