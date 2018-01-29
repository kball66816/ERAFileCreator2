using System.Text;

namespace Edi835._835Segments
{
    public class Iea : SegmentBase
    {
        public Iea()
        {
            SegmentIdentifier = "IEA";
            NumberOfFunctionalGroups = "1";
            InterchangeControlNumber = "201541257";
        }

        private string NumberOfFunctionalGroups { get; }
        private string InterchangeControlNumber { get; }

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