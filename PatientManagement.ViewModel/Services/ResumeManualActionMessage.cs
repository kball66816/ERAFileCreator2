using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.ViewModel.Services
{
    class ResumeManualActionMessage
    {
        public bool IsEnabled { get; private set; }
        public ResumeManualActionMessage(bool isEnabled)
        {
            IsEnabled = !isEnabled;
        }
    }
}
