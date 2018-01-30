﻿using System.Text;

namespace Edi835._835Segments
{
    public class Lx : SegmentBase
    {
        public Lx()
        {
            SegmentIdentifier = "LX";

            ClaimSequenceNumber = "1";
        }

        private string ClaimSequenceNumber { get; }

        public string BuildLx()
        {
            var buildLx = new StringBuilder();

            buildLx.Append(SegmentIdentifier)
                .Append(DataElementTerminator)
                .Append(ClaimSequenceNumber)
                .Append(SegmentTerminator);

            return buildLx.ToString();
        }
    }
}