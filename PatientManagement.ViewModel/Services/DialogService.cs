using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    public class DialogService
    {
        public DialogService(Patient patient)
        {
            dialogResult = false;
            newDialogResult = MessageBox.Show("Do you want to add additional encounters to this patient?", "Return new patient",
                    MessageBoxButton.YesNo);
        }

        public DialogService(AddonCharge addon)
        {
            dialogResult = false;
            newDialogResult = MessageBox.Show("Do you want to reuse this Addon?", "Return new Addon",
                MessageBoxButton.YesNo);

        }
        private bool dialogResult;

        private readonly MessageBoxResult newDialogResult;

        public bool ShowDialog()
        {
            dialogResult = newDialogResult == MessageBoxResult.Yes;

            return dialogResult;
        }

    }
}
