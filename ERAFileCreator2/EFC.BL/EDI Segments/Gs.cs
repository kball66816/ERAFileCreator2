using System.Text;

namespace EFC.BL.EDI_Segments
{
    public class Gs
    {
        public string BuildGs()
        {
            var buildGs = new StringBuilder();

            buildGs.Append("GS" + "*");
            buildGs.Append("HP" + "*"); //GS01 Functional Identifier Code
            buildGs.Append("7802840731" + "*");//GS02 Application Senders Code, ISA06 must be identical
            buildGs.Append("7234068" + "*");//GS03 Trading Partner Id
            buildGs.Append("20169693" + "*"); //GS04 Date CCYYMMDD
            buildGs.Append("0839" + "*"); //GS05 Time HHMM
            buildGs.Append("201541257" + "*");//GS06 Group Control Number must be equal to Ge02
            buildGs.Append("X" + "*");//GS07 Responsible Agency Code
            buildGs.Append("005010X221A1"); //GS08 Version/Release Code
            buildGs.Append("~");

            return buildGs.ToString();
        }
    }
}
