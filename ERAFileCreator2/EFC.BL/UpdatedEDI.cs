using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edi835.Segments;
using PatientManagement.DAL;
using PatientManagement.Model;

namespace EFC.BL
{
    public class UpdatedEdi
    {
        private readonly Provider billingProvider;

        private readonly InsuranceCompany insurance;

        private readonly List<Patient> patients;

        private readonly IAddonChargeRepository addonChargeRepository;

        private readonly IAdjustmentRepository adjustmentRepository;

        private readonly IPrimaryChargeRepository chargeRepository;

        private int segmentCount = 6;

        public UpdatedEdi()
        {
            IInsurance insuranceCompany = new InsuranceRepository();
            insurance = insuranceCompany.GetInsurance();

            IPatientRepository patientList = new PatientRepository();
            patients = patientList.GetAllPatients().ToList();

            IProvider provider = new BillingProviderRepository();
            billingProvider = provider.GetBillingProvider();

            chargeRepository = new PrimaryChargeRepository();

            addonChargeRepository = new AddonChargeRepository();

            adjustmentRepository = new AdjustmentRepository();
        }


        public string Create835File()
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

            var billingProviderN1 = new N1(billingProvider);
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

            foreach (var patient in patients)
            {
                var encounters = chargeRepository.GetSelectedCharges(patient.Id).ToList();

                var clp = new Clp(encounters);
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

                var startDtm = new Dtm(patient,
                    chargeRepository.GetAllCharges().FirstOrDefault(c => c.PatientId == patient.Id));
                edi.Append(startDtm.BuildDtm());
                segmentCount++;

                var chargeCount = 0;

                foreach (var charge in encounters)
                {
                    chargeCount++;
                    var svc = new Svc(charge);
                    edi.Append(svc.BuildSvc());
                    segmentCount++;

                    var dateOfServiceDtm = new Dtm(charge);
                    edi.Append(dateOfServiceDtm.BuildDtm());
                    segmentCount++;

                    var copayCas = new Cas(charge);

                    var chargeAdjustments = adjustmentRepository.GetSelectedAdjustments(charge.Id);

                    if (chargeAdjustments != null)
                    {
                        foreach (var adjustment in chargeAdjustments)
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

                    var addons = addonChargeRepository.GetSelectedAddonCharges(charge.Id);
                    foreach (var addonCharge in addons)
                    {
                        var addonsvc = new Svc(addonCharge);
                        edi.Append(addonsvc.BuildSvc());
                        segmentCount++;

                        var addonAdjustments = adjustmentRepository.GetSelectedAdjustments(addonCharge.Id);
                        foreach (var adjustment in addonAdjustments)
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