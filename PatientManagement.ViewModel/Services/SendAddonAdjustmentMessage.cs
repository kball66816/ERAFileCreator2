using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    internal class SendAddonAdjustmentMessage
    {
        public SendAddonAdjustmentMessage(Adjustment addonAdjustment)
        {
            Adjustment = addonAdjustment;
        }

        public Adjustment Adjustment { get; set; }
    }
}