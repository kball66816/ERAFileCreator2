using System.ComponentModel;

namespace PatientManagement.Model
{
    public class Preference : INotifyPropertyChanged
    {
        private bool enableAddonReusePrompt;
        private bool enablePatientReusePrompt;

        private bool reloadLastPatientFromLastSession;

        private bool reuseAddon;

        private bool reuseLastChargeForNextPatient;

        private bool reusePatient;

        public bool ReusePatient
        {
            get => reusePatient;
            set
            {
                if (value != reusePatient)
                {
                    reusePatient = value;
                    RaisePropertyChanged("ReusePatient");
                    UpdatePatientPromptStatus();
                }
            }
        }

        public bool EnablePatientReusePrompt
        {
            get => enablePatientReusePrompt;
            set
            {
                if (value != enablePatientReusePrompt)
                {
                    enablePatientReusePrompt = value;
                    RaisePropertyChanged("EnablePatientReusePrompt");
                }
            }
        }

        public bool ReuseAddon
        {
            get => reuseAddon;
            set
            {
                if (value != reuseAddon)
                {
                    reuseAddon = value;
                    RaisePropertyChanged("ReuseAddon");
                    UpdateAddonPromptStatus();
                }
            }
        }

        public bool EnableAddonReusePrompt
        {
            get => enableAddonReusePrompt;
            set
            {
                if (value != enableAddonReusePrompt)
                {
                    enableAddonReusePrompt = value;
                    RaisePropertyChanged("EnableAddonReusePrompt");
                }
            }
        }

        public bool ReloadLastPatientFromLastSession
        {
            get => reloadLastPatientFromLastSession;
            set
            {
                if (value != reloadLastPatientFromLastSession)
                {
                    reloadLastPatientFromLastSession = value;
                    RaisePropertyChanged("ReloadLastPatientFromLastSession");
                }
            }
        }

        public bool ReuseLastChargeForNextPatient
        {
            get => reuseLastChargeForNextPatient;
            set
            {
                if (value != ReuseLastChargeForNextPatient)
                {
                    reuseLastChargeForNextPatient = value;
                    RaisePropertyChanged("ReuseLastChargeForNextPatient");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void UpdatePatientPromptStatus()
        {
            if (ReusePatient)
                EnablePatientReusePrompt = true;

            else if (ReusePatient == false)
                EnablePatientReusePrompt = false;

            RaisePropertyChanged("EnablePatientReusePrompt");
        }

        private void UpdateAddonPromptStatus()
        {
            if (ReuseAddon)
                EnableAddonReusePrompt = true;

            else if (ReuseAddon == false)
                EnableAddonReusePrompt = false;

            RaisePropertyChanged("EnableAddonReusePrompt");
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}