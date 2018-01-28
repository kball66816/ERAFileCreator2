namespace Edi835.Segments
{
    public abstract class SegmentBase
    {
        protected string ComponentIdentifier = ":";
        protected string DataElementTerminator = "*";
        protected string SegmentTerminator = "~";
        protected string SegmentIdentifier { get; set; }
    }
}