namespace Edi835._835Segments
{
    public abstract class SegmentBase
    {
        protected string ComponentIdentifier = ":";
        protected string DataElementTerminator = "*";
        protected string SegmentTerminator = "~";
        protected string SegmentIdentifier { get; set; }
    }
}