namespace EFC.BL
{
    public class BillingProvider : Provider

    {
        public BillingProvider()
        {
            Address = new Address();
        }

        private string name;

        public string Name
        {
            get {
                if (isIndividual)
                {
                    return FirstName + " " + LastName;
                }

                else
                {
                    return name;
                }
            }

            set
            {
                if (value != name)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        private bool isIndividual;

        public bool IsIndividual
        {
            get { return isIndividual; }
            set
            {
                if (value != isIndividual)
                {
                    isIndividual = value;
                    RaisePropertyChanged("IsIndividual");
                }

            }
        }

        private bool isAlsoRendering;

        public bool IsAlsoRendering
        {
            get { return isAlsoRendering; }
            set
            {
                if (value != isAlsoRendering)
                {
                    isAlsoRendering = value;
                    RaisePropertyChanged("IsAlsoRendering");
                }

            }
        }
        public Address Address { get; set; }

    }
}
