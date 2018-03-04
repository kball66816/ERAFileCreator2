
using System.Timers;
using EFC.BL;
using PatientManagement.DAL;
using PatientManagement.Model;

namespace PatientManagement.ViewModel.Services
{
    public static class ChargeService
    {
        static ChargeService()
        {
            ChargeRepository = new PrimaryChargeRepository();
            ChargeDisplayTimer = new Timer { Interval = 5000 };
        }

        private static readonly IPrimaryChargeRepository ChargeRepository;

        public static Timer ChargeDisplayTimer { get; }


        public static PrimaryCharge GetNewCharge()
        {
            return new PrimaryCharge();
        }

        private static PrimaryCharge Clone(PrimaryCharge charge)
        {
            return new PrimaryCharge(charge);
        }

        public static void AddChargeToRepository(this PrimaryCharge charge)
        {
            ChargeRepository.Add(charge);
        }

        public static PrimaryCharge SetNewOrClonedChargeByUserSettings(PrimaryCharge charge)
        {
            charge = SettingsService.ReuseCharge ? Clone(charge) : GetNewCharge();

            return charge;
        }
    }
}
