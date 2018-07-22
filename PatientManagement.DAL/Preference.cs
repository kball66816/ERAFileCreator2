using System.ComponentModel;

namespace PatientManagement.DAL
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
            get => this.reusePatient;
            set
            {
                if (value != this.reusePatient)
                {
                    this.reusePatient = value;
                    this.RaisePropertyChanged("ReusePatient");
                    this.UpdatePatientPromptStatus();
                }
            }
        }

        public bool EnablePatientReusePrompt
        {
            get => this.enablePatientReusePrompt;
            set
            {
                if (value != this.enablePatientReusePrompt)
                {
                    this.enablePatientReusePrompt = value;
                    this.RaisePropertyChanged("EnablePatientReusePrompt");
                }
            }
        }

        public bool ReuseAddon
        {
            get => this.reuseAddon;
            set
            {
                if (value != this.reuseAddon)
                {
                    this.reuseAddon = value;
                    this.RaisePropertyChanged("ReuseAddon");
                    this.UpdateAddonPromptStatus();
                }
            }
        }

        public bool EnableAddonReusePrompt
        {
            get => this.enableAddonReusePrompt;
            set
            {
                if (value != this.enableAddonReusePrompt)
                {
                    this.enableAddonReusePrompt = value;
                    this.RaisePropertyChanged("EnableAddonReusePrompt");
                }
            }
        }

        public bool ReloadLastPatientFromLastSession
        {
            get => this.reloadLastPatientFromLastSession;
            set
            {
                if (value != this.reloadLastPatientFromLastSession)
                {
                    this.reloadLastPatientFromLastSession = value;
                    this.RaisePropertyChanged("ReloadLastPatientFromLastSession");
                }
            }
        }

        public bool ReuseLastChargeForNextPatient
        {
            get => this.reuseLastChargeForNextPatient;
            set
            {
                if (value != this.ReuseLastChargeForNextPatient)
                {
                    this.reuseLastChargeForNextPatient = value;
                    this.RaisePropertyChanged("ReuseLastChargeForNextPatient");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void UpdatePatientPromptStatus()
        {
            if (this.ReusePatient)
                this.EnablePatientReusePrompt = true;

            else if (this.ReusePatient == false)
                this.EnablePatientReusePrompt = false;

            this.RaisePropertyChanged("EnablePatientReusePrompt");
        }

        private void UpdateAddonPromptStatus()
        {
            if (this.ReuseAddon)
                this.EnableAddonReusePrompt = true;

            else if (this.ReuseAddon == false)
                this.EnableAddonReusePrompt = false;

            this.RaisePropertyChanged("EnableAddonReusePrompt");
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}