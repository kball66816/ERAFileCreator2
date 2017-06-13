using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERAFileCreator
{
    class InsuranceCompany
    {
        private string insuranceCompanyName;

        public string InsuranceCompanyName
        {
            get { return insuranceCompanyName; }
            set { insuranceCompanyName = value; }
        }
        private string insuranceCompanyTaxID;

        public string InsuranceCompanyTaxID
        {
            get { return insuranceCompanyTaxID; }
            set { insuranceCompanyTaxID = value; }
        }
        private decimal insuranceCheckTotal;

        public decimal InsuranceCheckTotal
        {
            get { return insuranceCheckTotal; }
            set { insuranceCheckTotal = value; }
        }

        private DateTime insuranceCheckDate;

        public DateTime InsuranceCheckDate
        {
            get { return insuranceCheckDate; }
            set { insuranceCheckDate = value; }
        }

        private string paymentType;

        public string PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }

    }
}
