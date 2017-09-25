using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDI835.Segments;
using PatientManagement.Model;

namespace EFC.BL
{
    class UpdatedEdi
    {

        public UpdatedEdi(InsuranceCompany insurance, Provider billingProvider)
        {
            Insurance = insurance;
        }

        private InsuranceCompany Insurance { get; set; }

        private Provider BillingProvider { get; set; }
        public String Create835File()
        {
            var edi = new StringBuilder();
            var st = new St();

            edi.Append(st.BuildSt());

            var bpr = new Bpr(Insurance);

            edi.Append(bpr.BuildBpr());

            var trn = new Trn(Insurance);

            edi.Append(trn.BuildTrn());

            var insuranceN1 = new N1(Insurance);

            edi.Append(insuranceN1.BuildN1());

            var insuranceN3 = new N3(Insurance);
            edi.Append(insuranceN3.BuildN3());

            var insuranceN4 = new N4(Insurance);
            edi.Append(insuranceN4.BuildN4());


            var per = new Per();
            edi.Append(per.BuildPer());

            var  billingProviderN1 = new N1(BillingProvider);
            return edi.ToString();


        }


    }
}
