using Common.Common;
using EFC.BL;
using EFC.BL.Utility;
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
            FormatBillId.BillIdFormatter();
            SendMessages();
            var edi = new UpdatedEdi();
            var save = new SaveToFile();
            save.SaveFile(edi.Create835File());
        }

        private static void SendMessages()
        {
            Messenger.Default.Send(new UpdateRepositoriesMessage());
            Messenger.Default.Send(new SaveFileMessage(), "SaveFile");
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
