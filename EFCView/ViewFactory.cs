namespace EraFileCreator
{
    public class ViewFactory
    {
        private UpdateInsuranceCompaniesWindow InsuranceCompaniesWindow { get; set; }

        public void ShowUpdateInsuranceCompaniesWindow()
        {
            this.InsuranceCompaniesWindow = new UpdateInsuranceCompaniesWindow();
            this.InsuranceCompaniesWindow.ShowDialog();
        }

        public void CloseUpdateInsuranceCompaniesWindow()
        {
            this.InsuranceCompaniesWindow?.Close();
            this.InsuranceCompaniesWindow = null;
        }
    }
}