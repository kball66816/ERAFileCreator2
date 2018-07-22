using System.Windows;

namespace EraFileCreator.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        public string Identifier { get; set; }
        public MessageBoxResult NewDialogResult { get; set; }

        public bool DialogResult { get; set; }

        public void DisplayMessage(string identifier)
        {
            this.NewDialogResult = MessageBox.Show(this.MessageBoxMessage(identifier), $"Reuse {identifier}",
                MessageBoxButton.YesNo);
        }

        public string MessageBoxMessage(string identifier)
        {
            return $"Do you want to Reuse the details of {identifier}?";
        }

        public bool ShowDialog()
        {
            this.DialogResult = this.NewDialogResult == MessageBoxResult.Yes;

            return this.DialogResult;
        }

        public void ClearMessage(string identifier)
        {
            this.NewDialogResult = MessageBox.Show(this.ClearMessageBoxMessage(identifier), $"Clear {identifier}",
                MessageBoxButton.YesNo);
        }

        public string ClearMessageBoxMessage(string identifier)
        {
            return $"Do you want to clear this {identifier}";
        }
    }
}