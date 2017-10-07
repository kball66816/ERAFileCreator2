namespace Edi835.Segments
{
    public abstract class SegmentBase
    {
        protected string SegmentIdentifier { get; set; }
        protected string DataElementTerminator = "*";
        protected string ComponentIdentifier = ":";
        protected string SegmentTerminator = "~";
    }
}
