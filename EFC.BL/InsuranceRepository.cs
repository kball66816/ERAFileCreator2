using PatientManagement.DAL;

namespace EFC.BL
{
    public class InsuranceRepository : IInsurance
    {
        private static InsuranceCompany _insurance;


        public void AddInsurance(InsuranceCompany insuranceCompany)
        {
            _insurance = insuranceCompany;
        }


        public InsuranceCompany GetInsurance()
        {
            return _insurance;
        }
    }
}