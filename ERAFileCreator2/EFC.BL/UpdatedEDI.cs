using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDI835.Segments;
using PatientManagement.Model;
using PatientManagement.DAL;


namespace EFC.BL
{
    public class UpdatedEdi
    {

        public UpdatedEdi()
        { 
            IInsurance insuranceCompany = new InsuranceRepository();
            insurance = insuranceCompany.GetInsurance();

            IPatientRepository patientList = new PatientRepository();
            patients = patientList.GetAllPatients().ToList();

            IProvider provider = new BillingProviderRepository();
            billingProvider = provider.GetBillingProvider();
        }

        private int segmentCount = 6;

        private readonly InsuranceCompany insurance;

        private readonly Provider billingProvider; 

        private readonly List<Patient> patients;


        public String Create835File()
        {
            var edi = new StringBuilder();

            var isa = new Isa();
            edi.Append(isa.BuildIsa());

            var gs = new Gs();
            edi.Append(gs.BuildGs());

            var st = new St();
            edi.Append(st.BuildSt());
            segmentCount++;


            var bpr = new Bpr(insurance);

            edi.Append(bpr.BuildBpr());
            segmentCount++;

            var trn = new Trn(insurance);

            edi.Append(trn.BuildTrn());
            segmentCount++;

            var dtm = new Dtm();
            edi.Append(dtm.BuildDtm());
            segmentCount++;

            var insuranceN1 = new N1(insurance);

            edi.Append(insuranceN1.BuildN1());
            segmentCount++;

            var insuranceN3 = new N3(insurance);
            edi.Append(insuranceN3.BuildN3());
            segmentCount++;

            var insuranceN4 = new N4(insurance);
            edi.Append(insuranceN4.BuildN4());
            segmentCount++;

            var insuranceRef = new Ref(insurance);
            edi.Append(insuranceRef.BuildRef());
            segmentCount++;

            var per = new Per();
            edi.Append(per.BuildPer());
            segmentCount++;

            var  billingProviderN1 = new N1(billingProvider);
            edi.Append(billingProviderN1.BuildN1());
            segmentCount++;

            var billingProviderN3 = new N3(billingProvider);
            edi.Append(billingProviderN3.BuildN3());
            segmentCount++;

            var billingProviderN4 = new N4(billingProvider);
            edi.Append(billingProviderN4.BuildN4());
            segmentCount++;

            if (billingProvider.IsAlsoRendering)
            {
                var billingProviderRef = new Ref(billingProvider, billingProvider.IsAlsoRendering);
                edi.Append(billingProviderRef.BuildRef());
            }

            else
            {
                var billingProviderRef = new Ref(billingProvider);
                edi.Append(billingProviderRef.BuildRef());
            }
            segmentCount++;

            var lx = new Lx();
            edi.Append(lx.BuildLx());
            segmentCount++;

            foreach (Patient patient in patients)
            {
                int chargeCount = 0;
                
                foreach (PrimaryCharge charge in patient.Charges)
                {
                    chargeCount++;

                    var clp = new Clp(charge);
                    edi.Append(clp.BuildClp());
                    segmentCount++;

                    var patientNm1 = new Nm1(patient);
                    edi.Append(patientNm1.BuildNm1());
                    segmentCount++;

                    if (patient.IncludeSubscriber)
                    {
                        var subscriberNm1 = new Nm1(patient.Subscriber);
                        edi.Append(subscriberNm1.BuildNm1());
                        segmentCount++;
                    }

                    var renderingNm1 = new Nm1(patient.RenderingProvider);
                    edi.Append(renderingNm1.BuildNm1());
                    segmentCount++;

                    var startDtm = new Dtm(patient, charge);
                    edi.Append(startDtm.BuildDtm());
                    segmentCount++;

                    var svc = new Svc(charge);
                    edi.Append(svc.BuildSvc());
                    segmentCount++;

                    var dateOfServiceDtm = new Dtm(charge);
                    edi.Append(dateOfServiceDtm.BuildDtm());
                    segmentCount++;

                    var copayCas = new Cas(charge);

                    if (charge.AdjustmentList != null)
                    {
                        foreach (Adjustment adjustment in charge.AdjustmentList.Where(a => a.AdjustmentAmount != 0))
                        {
                            var cas = new Cas(adjustment);
                            edi.Append(cas.BuildCas());
                            segmentCount++;
                        }

                        edi.Append(copayCas.BuildCas());
                        
                    }

                    else
                    {
                        edi.Append(copayCas.BuildCas());
                    }
                    segmentCount++;

                    var chargeRef = new Ref(charge, chargeCount);
                    edi.Append(chargeRef.BuildRef());
                    segmentCount++;

                    var amt = new Amt(charge);
                    edi.Append(amt.BuildAmt());
                    segmentCount++;

                    if (charge.AddonChargeList != null)
                    {
                        foreach (AddonCharge addonCharge in charge.AddonChargeList)
                        {
                            var addonsvc = new Svc(addonCharge);
                            edi.Append(addonsvc.BuildSvc());
                            segmentCount++;

                            foreach (Adjustment adjustment in addonCharge.AdjustmentList.Where(a=>a.AdjustmentAmount!=0))
                            {
                                var cas = new Cas(adjustment);
                                edi.Append(cas.BuildCas());
                                segmentCount++;

                                var addonAmt = new Amt(addonCharge);
                                edi.Append(addonAmt.BuildAmt());
                                segmentCount++;

                            }
                        }
                    }

                }


            }

            var se = new Se(segmentCount);
            edi.Append(se.BuildSe());


            var ge = new Ge();
            edi.Append(ge.BuildGe());


            var iea = new Iea();
            edi.Append(iea.BuildIea());

            return edi.ToString();

            

        }


    }
}
