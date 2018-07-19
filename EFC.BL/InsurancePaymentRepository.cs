using PatientManagement.DAL;

namespace EFC.BL
{
    public class InsurancePaymentRepository
    {


        private static InsurancePayment insurancePayment;

        public void AddInsurancePayment(InsuranceCompany insurance, Payment payment)
        {
            var insuranceWithPayment = new InsurancePayment(payment,insurance);
            insurancePayment = insuranceWithPayment;
        }

        public InsurancePayment GetInsuranceWithPayment()
        {
            return insurancePayment;
        }
    }
}
