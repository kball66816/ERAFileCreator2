using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Model
{
    public class ClaimStatusCode
    {
        public readonly Dictionary<string, string> Codes = new Dictionary<string, string>
        {
            { "1","Primary"},
            {"2","Secondary" },
            {"3","Tertiary" },
            {"4","Denied" },
            {"19","Processed as Primary, Forwarded to Additional Payer(s)"},
            {"20","Processed as Secondary, Forwarded to Additional Payer(s)" },
            {"22","Reversal of Previous Payment" },
            {"23","Not Our Claim, Forwarded To Additional Payers" },
            {"25","Predetermination Pricing ONly - No Payment" }
        };

        public ClaimStatusCode()
        {
            Code = "1";
        }

        public ClaimStatusCode(ClaimStatusCode claimStatus)
        {
            this.Code = claimStatus.Code;
        }
        public string Code { get; set; }
    }
}
