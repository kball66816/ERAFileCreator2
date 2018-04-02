using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PatientManagement.Model;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel.Mocks
{
    public class MessageBoxServiceMock : IMessageBoxService
    {
        public void DisplayMessage(string identifier)
        {
            Console.WriteLine(this.MessageBoxMessage(identifier));
        }

        public bool ShowDialog()
        {
            return false;
        }

        public string MessageBoxMessage(string identifier)
        {
            return $"Do you want to Reuse the details of {identifier}?";
        }
        public MessageBoxResult NewDialogResult { get; set; }
        public bool DialogResult { get; set; }
    }
}
