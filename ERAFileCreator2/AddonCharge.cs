using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERAFileCreator
{
    class AddonCharge
    {
        private string addonProcedureCode;

        public string AddonProcedureCode
        {
            get { return addonProcedureCode; }
            set { addonProcedureCode = value; }
        }
        private decimal addonChargeCost;

        public decimal AddonChargeCost
        {
            get { return addonChargeCost; }
            set { addonChargeCost = value; }
        }
        private decimal addonPaymentAmount;

        public decimal AddonPaymentAmount
        {
            get { return addonPaymentAmount; }
            set
            { addonPaymentAmount = value; }
        }
        private decimal addonContractualAdjustment;

        public decimal AddonContractualAdjustment
        {
            get { return addonContractualAdjustment; }
            set { addonContractualAdjustment = value; }

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
    }
}
