using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    public interface IMessageBoxService
    {
        bool ShowDialog();

       MessageBoxResult NewDialogResult { get; set; }

        void DisplayMessage(string identifier);

        bool DialogResult { get; set; }

        string MessageBoxMessage(string identifier);
    }
}
