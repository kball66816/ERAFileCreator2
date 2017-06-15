using System;

namespace EFC.BL
{
    public class Patient : Person
    {
        public Patient()
        {
            Subscriber = new Subscriber();
            Provider = new RenderingProvider();
            Charge = new Charge();
        }

        public Charge Charge { get; set; }

        public string MemberId { get; set; }

        private string billId;

        public string BillId
        {
            get { return billId; }
            set
            {
                if (value != billId)
                {
                    billId = value;
                    RaisePropertyChanged("BillId");
                    RaisePropertyChanged("FormattedBillId");
                }
            }
        }

        public string FormattedBillId
        {
            get
            {
                if (billId != null)
                {
                    return FormatBillId(billId);
                }

                else
                {
                    return string.Empty;
                }
            }

        }

        private bool usePlatformId;

        public bool UsePlatformId
        {
            get { return usePlatformId; }
            set
            {
                if (value != usePlatformId)
                {
                    usePlatformId = value;
                    RaisePropertyChanged("UsePlatformId");
                    RaisePropertyChanged("FormattedBillId");
                }
            }

        }

        private string FormatBillId(string unformattedBillId)
        {
            string formatBillId = string.Empty;
            if (UsePlatformId == false)

            {
                formatBillId = "1" + unformattedBillId.PadLeft(10, '0') + ClassicIdConcatination();

            }
            else if (UsePlatformId == true)
            {
                formatBillId = unformattedBillId;

            }
            return formatBillId;
        }

        private string ClassicIdConcatination()
        {
            var substringofPatientFirstName = FirstName.Length > 3 ? FirstName.Substring(0, 3) : FirstName;

            var substringofPatientLastName = LastName.Length > 3 ? LastName.Substring(0, 3) : LastName;

            return substringofPatientLastName.ToUpper() + substringofPatientFirstName.ToUpper();
        }

        public bool IncludeSubscriber { get; set; }

        public Subscriber Subscriber { get; set; }

        public RenderingProvider Provider { get; set; }

        public Patient CopyPatient()
        {
            var clone = (Patient)MemberwiseClone();
            clone.billId = string.Empty;
            clone.Charge = new Charge();
            clone.Charge.DateOfService = DateTime.Today;
            return clone;
        }
    }
}
