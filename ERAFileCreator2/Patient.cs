using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERAFileCreator
{
    class Patient
    {
        public List<Patient> PatientList {get;set;}
        public Patient()
        {
            ChargeList = new List<Charge>();
            this.chargelist = new List<Charge>();
        }

        private string patientFirstName;

        public string PatientFirstName
        {
            get { return patientFirstName; }
            set { patientFirstName = value; }
        }

        private string patientLastName;

        public string PatientLastName
        {
            get { return patientLastName; }
            set { patientLastName = value; }
        }

        private decimal patientCopay;

        public decimal PatientCopay
        {
            get { return patientCopay; }
            set { patientCopay = value; }

        }
        private string billId;

        public string BillId
        {
            get { return billId; }
            set { billId = value; }
        }

        private List<Charge> chargelist;

        public List<Charge> ChargeList
        {
            get { return chargelist; }
            set { chargelist = value; }
        }             
    }
}
