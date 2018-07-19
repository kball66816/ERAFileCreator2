using System.Windows.Input;
using Common.Common;
using Common.Common.Services;
using EFC.BL;
using EraFileCreator.Service.Messaging;
using EraFileCreator.Services;

namespace EraFileCreator.ViewModels
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
            SaveToFile.SaveTextFiletoSelectedDirectory(edi.Create835File());
            SendFileCreationComplete();
        }

        private static void SendFileCreationComplete()
        {
            Messenger.Default.Send(new SaveFileMessage(),"CreationCompleted");
        }
        private static void Save50Files(object obj)
        {
            SendCalculateRequest();
            for (var i = 0; i < 50; i++)
            {
                SendMessages();
                var edi = new UpdatedEdi();
                SaveToFile.SaveFiletoADefaultDirectory(edi.Create835File());
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