using System.Windows;
using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    public class DialogService
    {
        private readonly MessageBoxResult newDialogResult;

        private bool dialogResult;

        public DialogService(Patient patient)
        {
            newDialogResult = MessageBox.Show("Do you want to add additional encounters to this patient?",
                "Return new patient",
                MessageBoxButton.YesNo);
        }

        public DialogService(AddonCharge addon)
        {
            newDialogResult = MessageBox.Show("Do you want to reuse this Addon?", "Return new Addon",
                MessageBoxButton.YesNo);
        }

        public bool ShowDialog()
        {
            dialogResult = newDialogResult == MessageBoxResult.Yes;

            return dialogResult;
        }
    }
}