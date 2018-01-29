using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using PatientManagement.ViewModel.Services;
using Common.Common;

namespace PatientManagement.ViewModel
{
    public class UploadFileViewModel
    {
        public UploadFileViewModel()
        {
            UploadFileCommand = new Command(GetUploadFile, CanUploadFile);
        }

        public static string UploadedFile { get; private set; }

        public ICommand UploadFileCommand { get; set; }

        private void GetUploadFile(object obj)
        {
            UploadedFile = string.Empty;
            UploadFile.TextFileUpload();

            UploadedFile = UploadFile.UploadedFileAsStringContent;
            if (!UploadedFile.Contains("ISA"))
            {
                MessageBox.Show("Wrong FileType Please upload a different file");
                UploadedFile = string.Empty;
            }
        }

        private bool CanUploadFile(object obj)
        {
            return true;
        }
    }
}
