using Common.Common;
using Common.Common.Services;
using EFC.BL;
using PatientManagement.ViewModel.Services;
using System.Windows.Forms;
using System.Windows.Input;
using PatientManagement.ViewModel.Service.Messaging;

namespace PatientManagement.ViewModel
{
    public class UploadFileViewModel
    {
        public UploadFileViewModel()
        {
            UploadFileCommand = new Command(GetUploadFile, CanUploadFile);
            ResumeManualActionCommand = new Command(ResumeManualAction, CanResumeManualAction);
        }

        private static string UploadedFile { get; set; }

        public ICommand ResumeManualActionCommand { get; set; }

        public ICommand UploadFileCommand { get; set; }

        private static bool IsFileUploaded { get; set; }

        private static void ResumeManualAction(object obj)
        {
            IsFileUploaded = false;
            Messenger.Default.Send(new ResumeManualActionMessage(IsFileUploaded),"Disable");
        }

        private static void GetUploadFile(object obj)
        {
            UploadedFile = string.Empty;
            UploadFile.TextFileUpload();
            AssignStringIfUploadedStringIsNotNull();
            ConfirmStringContent();
        }

        private static void ConfirmStringContent()
        {
            if (UploadedFile.Contains("ISA"))
            {
                ParseFile();
            }
            else
            {
                MessageBox.Show("No File was selected or File was not 837. Please try again");
                UploadedFile = string.Empty;
            }
        }

        private static void AssignStringIfUploadedStringIsNotNull()
        {
            if (!string.IsNullOrEmpty(UploadFile.UploadedFileAsStringContent) )
            {
                UploadedFile = UploadFile.UploadedFileAsStringContent;
            }
        }

        private static void ParseFile()
        {
            var delimited = new[] {'~'};
            var loops = UploadedFile.Split(delimited);
            foreach (var loop in loops)
            {
                loop.Parse837Loop();
            }
            IsFileUploaded = true;
            Messenger.Default.Send(new ResumeManualActionMessage(IsFileUploaded),"Disable");
        }

        private static bool CanResumeManualAction(object obj)
        {
            return IsFileUploaded;
        }
        private static bool CanUploadFile(object obj)
        {
            return true;
        }
    }
}
