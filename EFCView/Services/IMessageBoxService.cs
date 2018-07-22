using System.Windows;

namespace EraFileCreator.Services
{
    public interface IMessageBoxService
    {
        MessageBoxResult NewDialogResult { get; set; }

        bool DialogResult { get; set; }
        bool ShowDialog();

        void DisplayMessage(string identifier);

        string MessageBoxMessage(string identifier);
    }
}