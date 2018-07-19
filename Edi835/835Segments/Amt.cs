﻿using System.Text;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class Amt : SegmentBase
    {
        public Amt(ServiceDescription charge)
        {
            SegmentIdentifier = "AMT";
            AmountQualifier = "B6";
            AllowedAmount = charge.AllowedAmount;
        }

        private string AmountQualifier { get; }
        private decimal AllowedAmount { get; }

        public string BuildAmt()
        {
            var buildAmt = new StringBuilder();

            buildAmt.Append(SegmentIdentifier);
            buildAmt.Append(DataElementTerminator);
            buildAmt.Append(AmountQualifier);
            buildAmt.Append(DataElementTerminator);
            buildAmt.Append(AllowedAmount);
            buildAmt.Append(SegmentTerminator);
            return buildAmt.ToString();
        }
    }
}