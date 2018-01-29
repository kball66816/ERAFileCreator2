using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PatientManagement.ViewModel.Services;
using Common.Common;

namespace PatientManagement.ViewModel
{
    public class UploadFileViewModel
    {
        public UploadFileViewModel()
        {
            UploadFileCommand = new Command(GetUploadFile,CanUploadFile);
        }

        private ICommand UploadFileCommand { get; set; }

        private static void GetUploadFile(object obj)
        {
            UploadFile.TextFileUpload();
        }

        private bool CanUploadFile(object obj)
        {
            bool canUpload = obj != null;

            return canUpload;
        }
    }
}
