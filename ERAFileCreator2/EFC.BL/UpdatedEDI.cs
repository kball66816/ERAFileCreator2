using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edi835._835Segments;
using PatientManagement.DAL;
using PatientManagement.Model;

namespace EFC.BL
{
    public class UpdatedEdi
    {
        private readonly Provider _billingProvider;

        private readonly InsuranceCompany _insurance;

        private readonly List<Patient> _patients;

        private int _segmentCount = 6;

        public UpdatedEdi()
        {
            IInsurance insuranceCompany = new InsuranceRepository();
            this._insurance = insuranceCompany.GetInsurance();
            IPatientRepository patientList = new PatientRepository();
            this._patients = patientList.GetAllPatients().ToList();
            IProvider provider = new BillingProviderRepository();
            this._billingProvider = provider.GetBillingProvider();
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
            this._segmentCount++;

            var bpr = new Bpr(this._insurance);

            edi.Append(bpr.BuildBpr());
            this._segmentCount++;

            var trn = new Trn(this._insurance);

            edi.Append(trn.BuildTrn());
            this._segmentCount++;

            var dtm = new Dtm();
            edi.Append(dtm.BuildDtm());
            this._segmentCount++;

            var insuranceN1 = new N1(this._insurance);

            edi.Append(insuranceN1.BuildN1());
            this._segmentCount++;

            var insuranceN3 = new N3(this._insurance);
            edi.Append(insuranceN3.BuildN3());
            this._segmentCount++;

            var insuranceN4 = new N4(this._insurance);
            edi.Append(insuranceN4.BuildN4());
            this._segmentCount++;

            var insuranceRef = new Ref(this._insurance);
            edi.Append(insuranceRef.BuildRef());
            this._segmentCount++;

            var per = new Per();
            edi.Append(per.BuildPer());
            this._segmentCount++;

            var billingProviderN1 = new N1(this._billingProvider);
            edi.Append(billingProviderN1.BuildN1());
            this._segmentCount++;

            var billingProviderN3 = new N3(this._billingProvider);
            edi.Append(billingProviderN3.BuildN3());
            this._segmentCount++;

            var billingProviderN4 = new N4(this._billingProvider);
            edi.Append(billingProviderN4.BuildN4());
            this._segmentCount++;

            if (!this._billingProvider.IsIndividual)
            {
                var billingProviderRef = new Ref(this._billingProvider, this._billingProvider.IsIndividual);
                edi.Append(billingProviderRef.BuildRef());
            }

            else
            {
                var billingProviderRef = new Ref(this._billingProvider);
                edi.Append(billingProviderRef.BuildRef());
            }

            this._segmentCount++;

            var lx = new Lx();
            edi.Append(lx.BuildLx());
            this._segmentCount++;

            foreach (var patient in this._patients)
            {
                var clp = new Clp(patient.Charges);
                edi.Append(clp.BuildClp());
                this._segmentCount++;

                var patientNm1 = new Nm1(patient);
                edi.Append(patientNm1.BuildNm1());
                this._segmentCount++;

                if (patient.IncludeSubscriber)
                {
                    var subscriberNm1 = new Nm1(patient.Subscriber);
                    edi.Append(subscriberNm1.BuildNm1());
                    this._segmentCount++;
                }

                var renderingNm1 = new Nm1(patient.RenderingProvider);
                edi.Append(renderingNm1.BuildNm1());
                this._segmentCount++;

                var startDtm = new Dtm(patient);
                edi.Append(startDtm.BuildDtm());
                this._segmentCount++;

                var chargeCount = 0;

                foreach (var charge in patient.Charges)
                {
                    chargeCount++;
                    var svc = new Svc(charge);
                    edi.Append(svc.BuildSvc());
                    this._segmentCount++;

                    var dateOfServiceDtm = new Dtm(charge);
                    edi.Append(dateOfServiceDtm.BuildDtm());
                    this._segmentCount++;

                    var copayCas = new Cas(charge);

                    var chargeAdjustments = charge.Adjustments;

                    if (chargeAdjustments != null)
                    {
                        foreach (var adjustment in chargeAdjustments)
                        {
                            var cas = new Cas(adjustment);
                            edi.Append(cas.BuildCas());
                            this._segmentCount++;
                        }

                        edi.Append(copayCas.BuildCas());
                    }
                    else
                    {
                        edi.Append(copayCas.BuildCas());
                    }

                    this._segmentCount++;

                    var chargeRef = new Ref(charge, chargeCount);
                    edi.Append(chargeRef.BuildRef());
                    this._segmentCount++;

                    var amt = new Amt(charge);
                    edi.Append(amt.BuildAmt());
                    this._segmentCount++;

                    var addons = charge.AddonCharges;
                    foreach (var addonCharge in addons)
                    {
                        var addonsvc = new Svc(addonCharge);
                        edi.Append(addonsvc.BuildSvc());
                        this._segmentCount++;

                        var addonAdjustments = addonCharge.Adjustments;
                        foreach (var adjustment in addonAdjustments)
                        {
                            var cas = new Cas(adjustment);
                            edi.Append(cas.BuildCas());
                            this._segmentCount++;

                            var addonAmt = new Amt(addonCharge);
                            edi.Append(addonAmt.BuildAmt());
                            this._segmentCount++;
                        }
                    }
                }
            }

            var se = new Se(this._segmentCount);
            edi.Append(se.BuildSe());


            var ge = new Ge();
            edi.Append(ge.BuildGe());


            var iea = new Iea();
            edi.Append(iea.BuildIea());


            return edi.ToString();
        }
    }
}