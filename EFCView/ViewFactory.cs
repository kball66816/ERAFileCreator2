
namespace EraFileCreator
{
    public class ViewFactory
    {
        public UpdateInsuranceCompaniesWindow InsuranceCompaniesWindow { get; set; }
        public void ShowUpdateInsuranceCompaniesWindow()
        {
            if (InsuranceCompaniesWindow == null)
            {
                InsuranceCompaniesWindow = new UpdateInsuranceCompaniesWindow();
                InsuranceCompaniesWindow.Show();
            }
        }

        public void CloseUpdateInsuranceCompaniesWindow()
        {
            this.InsuranceCompaniesWindow?.Close();
        }
    }
}
