using PatientManagement.Model;
using System.Windows;

namespace PatientManagement.ViewModel.Services
{
    public class MessageBoxService
    {
        public MessageBoxService(Patient patient)
        {
            identifier = patient.FullName;
            DisplayMessageBox();
        }

        public MessageBoxService(AddonCharge addon)
        {
            identifier = addon.ProcedureCode;
            DisplayMessageBox();
        }

        private MessageBoxResult newDialogResult;

        private bool dialogResult;

        private readonly string identifier;

        private void DisplayMessageBox()
        {
            newDialogResult = MessageBox.Show($"Do you want to Add an additional {identifier}?", $"Reuse {identifier}",
                MessageBoxButton.YesNo);
        }

        public bool ShowDialog()
        {
            dialogResult = newDialogResult == MessageBoxResult.Yes;

            return dialogResult;
        }
    }
}