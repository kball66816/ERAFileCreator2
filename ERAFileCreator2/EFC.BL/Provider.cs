namespace EFC.BL
{
    /// <summary>
    /// Base properties of a provider
    /// </summary>
    public abstract class Provider : Person
    {


        private string npi;

        public string Npi
        {
            get { return npi; }
            set
            {
                if (value != npi)
                {
                    npi = value;
                    RaisePropertyChanged("Npi");
                }
            }
        }
    }

}
