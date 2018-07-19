namespace PatientManagement.DAL
{
    public interface IInsurance
    {
        void AddInsurance(InsuranceCompany insurance);
        InsuranceCompany GetInsurance();
    }
}