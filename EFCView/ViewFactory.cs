namespace EraFileCreator
{
    public class ViewFactory
    {
        private UpdateInsuranceCompaniesWindow InsuranceCompaniesWindow { get; set; }

        public void ShowUpdateInsuranceCompaniesWindow()
        {
            if (this.InsuranceCompaniesWindow == null)
            {
                this.InsuranceCompaniesWindow = new UpdateInsuranceCompaniesWindow();
                this.InsuranceCompaniesWindow.Show();
            }
        }

        public void CloseUpdateInsuranceCompaniesWindow()
        {
            this.InsuranceCompaniesWindow?.Close();
        }
    }
}