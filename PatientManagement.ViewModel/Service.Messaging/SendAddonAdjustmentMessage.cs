using PatientManagement.Model;

namespace PatientManagement.ViewModel.Service.Messaging
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