using PatientManagement.DAL;
using PatientManagement.Model;

namespace EFC.BL.Utility
{
    public static class FormatBillId
    {
        public static void BillIdFormatter()
        {
            IPatientRepository pr = new PatientRepository();
            foreach (var patient in pr.GetAllPatients())
            {
                IPrimaryChargeRepository cr = new PrimaryChargeRepository(patient);

                foreach (var charge in cr.GetAllCharges())
                {
                    FormatBillIdToClassicId(patient, charge);
                }
            }  
        }

        private static void FormatBillIdToClassicId(Patient patient, PrimaryCharge charge)
        {
            if (charge.BillId.Length > 10) return;
            if (charge.FormatClassicBillId)
            {
                charge.BillId = "1" + charge.BillId.PadLeft(10, '0') + ClassicIdConcatination(patient.FirstName, patient.LastName);
            }
        }

        private static string ClassicIdConcatination(string firstName, string lastName)
        {
            var substringofPatientFirstName = firstName.Length > 3 ? firstName.Substring(0, 3) : firstName;

            var substringofPatientLastName = lastName.Length > 3 ? lastName.Substring(0, 3) :lastName;

            var concatenatedClassicIdFormat = substringofPatientLastName.ToUpper() + substringofPatientFirstName.ToUpper();
            return concatenatedClassicIdFormat;
        }
    }
}
