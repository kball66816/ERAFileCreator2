namespace PatientManagement.DAL
{
    public class InsurancePayment
    {
        public InsurancePayment(Payment payment, InsuranceCompany insuranceCompany)
        {
            this.Payment = payment;
            this.InsuranceCompany = insuranceCompany;
        }

        public InsuranceCompany InsuranceCompany { get; }

        public Payment Payment { get; }
    }
}
