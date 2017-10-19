using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class RenderingProviderViewModel:INotifyPropertyChanged
    {
        public RenderingProviderViewModel()
        {
        
            RenderingProvider = new Provider();
            Messenger.Default.Register<UpdateRepositoriesMessage>(this, OnUpdateRepositoriesMessage);
            Messenger.Default.Register<Provider>(this, OnReceiptOfBillingProvider, "BillingProvider");
            Messenger.Default.Register<Patient>(this, OnReceiptOfPatient, "AddRenderingProvider");

        }

        private void OnReceiptOfPatient(Patient patient)
        {
            patient.RenderingProvider = RenderingProvider;
            RaisePropertyChanged("SelectedPatient");

        }

        private void OnReceiptOfBillingProvider(Provider billingProvider)
        {
            if (billingProvider.IsIndividual)
            {
                RenderingProvider = billingProvider;
                //    renderingProvider.FirstName = billingProvider.FirstName;
                //    renderingProvider.LastName = billingProvider.LastName;
                //    renderingProvider.Npi = billingProvider.Npi;
                //    RaisePropertyChanged("RenderingProvider");
            }

            else if (billingProvider.IsAlsoRendering == false)
            {
                return;
            }
        }

        private Provider renderingProvider;

        public Provider RenderingProvider
        {
            get { return renderingProvider; }
            set
            {
                if (renderingProvider == value) return;
                renderingProvider = value;
                RaisePropertyChanged("RenderingProvider");

            }
        }

        private void OnUpdateRepositoriesMessage(UpdateRepositoriesMessage obj)
        {
            Messenger.Default.Send(RenderingProvider, "RenderingProvider");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
