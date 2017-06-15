using System.Text;

namespace EFC.BL.EDI_Segments
{
    class Ge
    {
        public string BuildGe()
        {
            var buildGe = new StringBuilder();
           buildGe.Append("GE*");
           buildGe.Append("1*");
           buildGe.Append("201541257");
           buildGe.Append("~");
            return buildGe.ToString();
        }
    }
}
