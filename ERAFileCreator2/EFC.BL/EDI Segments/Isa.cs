using System.Text;

namespace EFC.BL.EDI_Segments
{
    public class Isa
    {
        public string BuildIsa()
        {
            var buildIsa = new StringBuilder();

            buildIsa.Append("ISA" + "*");
            buildIsa.Append("00" + "*"); // ISA01 Authorization Information Qualifier
            buildIsa.Append("          *"); //ISA02 Authorization Information always 10 blank spaces
            buildIsa.Append("00" + "*"); //ISA03 Security Information Qualifier
            buildIsa.Append("          *"); //ISA04 Security INformation always 10 blank spaces
            buildIsa.Append("30" + "*"); //ISA05 Interchange Id Qualifier
            buildIsa.Append("166055205UPSERB" + "*"); //ISA06 Interchange Sender Id must be 15 bytes
            buildIsa.Append("30" + "*"); //ISA07 Interchange ID Qualifier
            buildIsa.Append("074056672XPCMXJ" + "*"); //ISA08 Interchange Receiver Id must be 15 bytes
            buildIsa.Append("160603" + "*"); //ISA09 Interchange Date YYMMDD
            buildIsa.Append("0839" + "*"); //ISA10 Interchange Time HHMM
            buildIsa.Append("^" + "*"); //ISA11 Interchange Control Standard Identifier
            buildIsa.Append("00501" + "*"); //ISA12 Interchange Control version number
            buildIsa.Append("201541257" + "*");//ISA13 Interchange Control Number
            buildIsa.Append("0" + "*"); //ISA14 Acknowledge Requested Status
            buildIsa.Append("P" + "*"); //ISA15 Usage Indicator
            buildIsa.Append(":"); //Element Seperator
            buildIsa.Append("~");

            return buildIsa.ToString();
        }
    }
}
