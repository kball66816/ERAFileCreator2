using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERAFileCreator
{
    class Charge
    {
        public Charge()
        {
            List<AddonCharge> AddonChargeList = new List<AddonCharge>();
            this.addonChargeList = new List<AddonCharge>();
        }

        private string primaryProcedureCode;
        public string PrimaryProcedureCode
        {
            get { return primaryProcedureCode; }
            set { primaryProcedureCode = value; }
        }

        private string dateOfService;
        public string DateofService
        {
            get { return dateOfService; }
            set { dateOfService = value; }
        }

        private decimal primaryChargeCost;
        public decimal PrimaryChargeCost
        {
            get { return primaryChargeCost; }
            set { primaryChargeCost = value; }
        }

        private DateTime chargePaymentDate;
        public DateTime ChargePaymentDate
        {
            get { return chargePaymentDate; }
            set { chargePaymentDate = value; }
        }

        private decimal primaryChargeContractualAdjustment;
        public decimal PrimaryChargeContractualAdjustment
        {
            get { return primaryChargeContractualAdjustment; }
            set { primaryChargeContractualAdjustment = value; }
        }

        private decimal primaryPaymentAmount;
        public decimal PrimaryPaymentAmount
        {
            get { return primaryPaymentAmount; }
            set { primaryPaymentAmount = value; }
        }

        private List<AddonCharge> addonChargeList;
        public List<AddonCharge> AddonChargeList
        {
            get { return addonChargeList; }
            set { addonChargeList = value; }
        }

        private string modifierOne;

        public string ModifierOne
        {
            get { return modifierOne; }
            set { modifierOne = value; }
        }

        private string modifierTwo;

        public string ModifierTwo
        {
            get { return modifierTwo; }
            set { modifierTwo = value; }
        }

        private string modifierThree;

        public string ModifierThree
        {
            get { return modifierThree; }
            set { modifierThree = value; }
        }

        private string modifierFour;

        public string ModifierFour
        {
            get { return modifierFour; }
            set { modifierFour = value; }
        }



        public decimal TotalCostofCharges()
        {
            decimal totalcost = 0;

            totalcost = PrimaryChargeCost;

            foreach (AddonCharge addon in addonChargeList)
            {
                totalcost = addon.AddonChargeCost + totalcost;
            }
            return totalcost;
        }



        public void TotalChargesPaid()
        {
            AddonChargeList.Sum(addon => addon.AddonPaymentAmount);
        }

        public decimal TotalPaidforaddons()
        {
            decimal totalpaid = 0;
            foreach (AddonCharge addon in AddonChargeList)
            {
                totalpaid = addon.AddonPaymentAmount + totalpaid;

            }
            return totalpaid;
        }
    }
}
