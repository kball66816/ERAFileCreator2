using System.Windows.Input;
using Common.Common;
using Common.Common.Services;
using EFC.BL;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class SaveFileViewModel
    {
        public SaveFileViewModel()
        {
            LoadCommands();
        }

        public ICommand SaveFileCommand { get; set; }

        public ICommand SaveBatchOfFiles { get; set; }

        public ICommand CalculateCommand { get; set; }

        private static void Save(object obj)
        {
            SendMessages();
            SendCalculateRequest();
            var edi = new UpdatedEdi();
            edi.Create835File().SaveTextFiletoSelectedDirectory();
        }

        private static void Save50Files(object obj)
        {
            SendCalculateRequest();
            for (var i = 0; i < 50; i++)
            {
                SendMessages();
                var edi = new UpdatedEdi();
                edi.Create835File().SaveFiletoADefaultDirectory();
            }
        }

        private static void SendMessages()
        {
            Messenger.Default.Send(new UpdateRepositoriesMessage(),"UpdateRepositories");
            Messenger.Default.Send(new SaveFileMessage(), "SaveTextFiletoSelectedDirectory");
        }

        private void LoadCommands()
        {
            SaveFileCommand = new Command(Save, CanSave);
            SaveBatchOfFiles = new Command(Save50Files, CanSave);
            CalculateCommand = new Command(Calculate, CanCalculate);
        }


        private void Calculate(object obj)
        {
            SendCalculateRequest();
        }

        private static void SendCalculateRequest()
        {
            Messenger.Default.Send(new UpdateCalculations());
        }

        private bool CanCalculate(object obj)
        {
            return true;
        }

        private static bool CanSave(object obj)
        {
            return true;
        }
    }
}