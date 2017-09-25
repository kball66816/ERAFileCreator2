namespace EDI835
{
    public class SegmentBase
    {
        public SegmentBase()
        {
            DataElementTerminator = "*";
            ComponentIdentifier = ":";
            SegmentTerminator = "~";
        }
        public string SegmentIdentifier { get; set; }
        public string DataElementTerminator { get; set; }
        public string ComponentIdentifier { get; set; }
        public string SegmentTerminator { get; set; }
    }
}
