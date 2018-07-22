using System.Text;

namespace Edi835._835Segments
{
    public class Iea : SegmentBase
    {
        public Iea()
        {
            this.SegmentIdentifier = "IEA";
            this.NumberOfFunctionalGroups = "1";
            this.InterchangeControlNumber = "201541257";
        }

        private string NumberOfFunctionalGroups { get; }
        private string InterchangeControlNumber { get; }

        public string BuildIea()
        {
            var buildIea = new StringBuilder();

            buildIea.Append(this.SegmentIdentifier);
            buildIea.Append(this.DataElementTerminator);
            buildIea.Append(this.NumberOfFunctionalGroups);
            buildIea.Append(this.DataElementTerminator);
            buildIea.Append(this.InterchangeControlNumber);
            buildIea.Append(this.SegmentTerminator);

            return buildIea.ToString();
        }
    }
}