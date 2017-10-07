using System.Text;
using Edi835.Segments;

namespace EDI835.Segments
{
    public class Iea:SegmentBase
    {
        public Iea()
        {
            SegmentIdentifier = "IEA";
            NumberOfFunctionalGroups = "1";
            InterchangeControlNumber = "201541257";
        }

        private string NumberOfFunctionalGroups { get; set; }
        private string InterchangeControlNumber { get; set; }

        public string BuildIea()
        {
            var buildIea = new StringBuilder();

            buildIea.Append(SegmentIdentifier);
            buildIea.Append(DataElementTerminator);
            buildIea.Append(NumberOfFunctionalGroups);
            buildIea.Append(DataElementTerminator);
            buildIea.Append(InterchangeControlNumber);
            buildIea.Append(SegmentTerminator);

            return buildIea.ToString();
        }
    }
}
