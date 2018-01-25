using Common.Common;
using EFC.BL;
using PatientManagement.ViewModel.Services;
using System.Windows.Input;
using Common.Common.Services;

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
            edi.Create835File().SaveTextFiletoSelectedDirectory();
        }

        private static void Save50Files(object obj)
        {
            for (int i = 0; i < 50; i++)
            {
                SendMessages();
                var edi = new UpdatedEdi();
                edi.Create835File().SaveFiletoADefaultDirectory();
            }
            
        }
        private static void SendMessages()
        {
            Messenger.Default.Send(new UpdateRepositoriesMessage());
            Messenger.Default.Send(new SaveFileMessage(), "SaveTextFiletoSelectedDirectory");
        }

        public ICommand SaveFileCommand { get; set; }

        public ICommand SaveBatchOfFiles { get; set; }

        private void LoadCommands()
        {
            SaveFileCommand = new Command(Save, CanSave);
            SaveBatchOfFiles = new Command(Save50Files, CanSave);

        }

        private static bool CanSave(object obj)
        {
            return true;
        }
    }
}
