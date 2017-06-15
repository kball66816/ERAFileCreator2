using System.Text;

namespace EFC.BL.EDI_Segments
{
    class Lx
    {
        public string BuildLx()
        {
            var buildLx = new StringBuilder();

           buildLx.Append("LX*");
           buildLx.Append("1"); //Claim Sequence Number
           buildLx.Append("~");

            return buildLx.ToString();
        }
    }
}
