namespace PatientManagement.ViewModel.Service.Messaging
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
