using Common.Common;
using EFC.BL;
using PatientManagement.ViewModel.Services;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class SaveFileViewModel
    {
        public SaveFileViewModel()
        {
            LoadCommands();
        }
        private static void Save(object obj)
        {
            SendMessages();
            var edi = new UpdatedEdi();
            var save = new SaveToFile();
            save.SaveTextFile(edi.Create835File());
        }

        private static void SendMessages()
        {
            Messenger.Default.Send(new UpdateRepositoriesMessage());
            Messenger.Default.Send(new SaveFileMessage(), "SaveTextFile");
        }

        public ICommand SaveFileCommand { get; set; }

        private void LoadCommands()
        {
            SaveFileCommand = new Command(Save, CanSave);
        }

        private static bool CanSave(object obj)
        {
            return true;
        }
    }
}
