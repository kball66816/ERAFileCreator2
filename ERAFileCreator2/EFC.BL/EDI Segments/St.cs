using System.Text;

namespace EFC.BL.EDI_Segments
{
    public class St
    {
        public string BuildSt()
        {
            var buildSt = new StringBuilder();
            
                buildSt.Append("ST" + "*");
                buildSt.Append("835" + "*"); //Transaction Set Identification Code
                buildSt.Append("000000001"); //Transaction Set Control Number must be equal to SE02
                buildSt.Append("~");

                return buildSt.ToString();   
        }

    }
}
