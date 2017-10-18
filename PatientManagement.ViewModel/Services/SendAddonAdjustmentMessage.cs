using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    class SendAddonAdjustmentMessage
    {
        public SendAddonAdjustmentMessage(Adjustment addonAdjustment)
        {
            Adjustment = addonAdjustment;
        }

        public Adjustment Adjustment { get; set; }
    }
}
