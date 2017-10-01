﻿using System;
using System.ComponentModel;
using System.Windows.Input;
using EraView.Services;
using PatientManagement.Model;

namespace EraView.ViewModel
{
    public class PreferenceViewModel : INotifyPropertyChanged
    {
        public PreferenceViewModel()
        {
            Preference = new Preference();
            Settings = new SettingsService();
            Preference = Settings.PullDefaultPreferences(preference);
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

        private void SavePreference(Object obj)
        {
            Settings.SetDefaultPreferences(Preference);
        }

        public void LoadCommands()
        {
            SavePreferenceCommand = new Command(SavePreference);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
