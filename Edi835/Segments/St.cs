﻿using System.Text;

namespace EDI835.Segments
{
    public class St:SegmentBase
    {
        public St()
        {
            SegmentIdentifier = "ST";
            TransactionsetIdentifierCode = "835";
            TransactionSetControlNumber = "000000001";
        }

        private string TransactionsetIdentifierCode { get; set; }
        private string TransactionSetControlNumber { get; set; }

        public string BuildSt()
        {
            var buildSt = new StringBuilder();

            buildSt.Append(SegmentIdentifier)
                   .Append(DataElementTerminator)
                   .Append(TransactionsetIdentifierCode)
                   .Append(DataElementTerminator)
                   .Append(TransactionSetControlNumber)
                   .Append(SegmentTerminator);

            return buildSt.ToString();
        }
    }
}