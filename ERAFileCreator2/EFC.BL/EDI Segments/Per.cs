using System.Text;

namespace EFC.BL.EDI_Segments
{
    class Per
    {
        public string BuildPerPhone()
        {
            var buildPer = new StringBuilder();

            buildPer.Append("PER" + "*");
            buildPer.Append("BL" + "*"); //PER01 Contact Functional Code
            buildPer.Append("EDI SUPPORT" + "*"); //PER02 Payer Contact Name
            buildPer.Append("TE" + "*"); //PER03 Communication Number Qualifier
            buildPer.Append("888888888"); //PER04 Payer Contact Communication Number 
            buildPer.Append("~");

            return buildPer.ToString();
        }

        public string BuildPerWebSite()
        {
            var buildPer = new StringBuilder();
            buildPer.Append("PER" + "*");
            buildPer.Append("IC" + "*"); //PER01 Contact Function Code
            buildPer.Append("Payer Contact Name" + "*"); //PER02 Name
            buildPer.Append("UR" + "*"); //PER03 Communication Number Qualifier
            buildPer.Append("WWW.WEBSITE.COM"); //PER04 Communication Number
            buildPer.Append("~");

            return buildPer.ToString();
        }
    }
}
