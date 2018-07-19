using System.Windows;

namespace EraFileCreator.Services
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
