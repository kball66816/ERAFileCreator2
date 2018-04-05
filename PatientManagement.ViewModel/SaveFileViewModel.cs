using Common.Common;
using Common.Common.Services;
using EFC.BL;
using PatientManagement.ViewModel.Service.Messaging;
using PatientManagement.ViewModel.Services;
using System.Windows.Input;

namespace PatientManagement.ViewModel
{
    public class SaveFileViewModel
    {
        public SaveFileViewModel()
        {
            this.LoadCommands();
        }

        public ICommand SaveFileCommand { get; set; }

        public ICommand SaveBatchOfFiles { get; set; }

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
            Messenger.Default.Send(new UpdateRepositoriesMessage(), "UpdateRepositories");
            Messenger.Default.Send(new SaveFileMessage(), "SaveTextFiletoSelectedDirectory");
        }

        private void LoadCommands()
        {
            this.SaveFileCommand = new Command(Save, CanSave);
            this.SaveBatchOfFiles = new Command(Save50Files, CanSave);
        }

        private static void SendCalculateRequest()
        {
            Messenger.Default.Send(new UpdateCalculations());
        }

        private static bool CanSave(object obj)
        {
            var canSave = false;

            foreach (var patient in PatientService.PatientRepository.GetAllPatients())
            {
                canSave = patient.Charges.Count > 0;
            }

            return canSave;
        }
    }
}