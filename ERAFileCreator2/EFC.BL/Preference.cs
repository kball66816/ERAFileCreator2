using System.ComponentModel;

namespace EFC.BL
{
    public class Preference : INotifyPropertyChanged
    {

        private bool reusePatient;

        public bool ReusePatient
        {
            get { return reusePatient; }
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

        private void UpdatePatientPromptStatus()
        {
            if(ReusePatient)
            {
                EnablePatientReusePrompt = true;
            }

            else if (ReusePatient == false)
            {
                EnablePatientReusePrompt = false;
            }

            RaisePropertyChanged("EnablePatientReusePrompt");
        }
        private bool enablePatientReusePrompt;

        public bool EnablePatientReusePrompt
        {
            get { return enablePatientReusePrompt; }
            set
            {
                if (value != enablePatientReusePrompt)
                {
                    enablePatientReusePrompt = value;
                    RaisePropertyChanged("EnablePatientReusePrompt");
                }
            }
        }

        private void UpdateAddonPromptStatus()
        {
            if (ReuseAddon)
            {
                EnableAddonReusePrompt = true;
            }

            else if (ReuseAddon == false)
            {
                EnableAddonReusePrompt = false;
            }

            RaisePropertyChanged("EnableAddonReusePrompt");
        }

        private bool reuseAddon;

        public bool ReuseAddon
        {
            get { return reuseAddon; }
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

        private bool enableAddonReusePrompt;

        public bool EnableAddonReusePrompt
        {
            get { return enableAddonReusePrompt; }
            set
            {
                if (value != enableAddonReusePrompt)
                {
                    enableAddonReusePrompt = value;
                    RaisePropertyChanged("EnableAddonReusePrompt");
                }
            }
        }

        private bool reloadLastPatientFromLastSession;

        public bool ReloadLastPatientFromLastSession
        {
            get { return reloadLastPatientFromLastSession; }
            set
            {
                if (value != reloadLastPatientFromLastSession)
                {
                    reloadLastPatientFromLastSession = value;
                    RaisePropertyChanged("ReloadLastPatientFromLastSession");
                }
            }
        }

        private bool reuseLastChargeForNextPatient;

        public bool ReuseLastChargeForNextPatient
        {
            get { return reuseLastChargeForNextPatient; }
            set
            {
                if(value!=ReuseLastChargeForNextPatient)
                {
                    reuseLastChargeForNextPatient = value;
                    RaisePropertyChanged("ReuseLastChargeForNextPatient");
                }
              
            }
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
