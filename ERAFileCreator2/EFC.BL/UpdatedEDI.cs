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

            ReturnEdiTransactionHeader(edi);
            ReturnEdiTransactionPayerId(edi);
            ReturnEdiTransactionPayeeId(edi);
            ReturnEdiTransactionHeaderNumberDetail(edi);
            ReturnEdiClaimPaymentDetails(edi);
            ReturnEdiTransactionSummary(edi);

            return edi.ToString();
        }

        private void ReturnEdiTransactionSummary(StringBuilder edi)
        {
            var se = new Se(segmentCount);
            edi.Append(se.BuildSe());


            var ge = new Ge();
            edi.Append(ge.BuildGe());


            var iea = new Iea();
            edi.Append(iea.BuildIea());
        }

        private void ReturnEdiTransactionHeaderNumberDetail(StringBuilder edi)
        {
            var lx = new Lx();
            edi.Append(lx.BuildLx());
            segmentCount++;
        }

        private void ReturnEdiTransactionPayeeId(StringBuilder edi)
        {
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
        }

        private void ReturnEdiTransactionPayerId(StringBuilder edi)
        {
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
        }

        private void ReturnEdiTransactionHeader(StringBuilder edi)
        {
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
        }

        private void ReturnEdiClaimPaymentDetails(StringBuilder edi)
        {
            foreach (Patient patient in patients)
            {
                ReturnEdiTransactionClaimPaymentInformation(edi, patient);

                int chargeCount = 0;

                foreach (var charge in patient.Charges)
                {
                    chargeCount++;
                    ReturnEdiServicePaymentInformationForEncounters(edi, chargeCount, charge);
                }
            }
        }

        private void ReturnEdiServicePaymentInformationForEncounters(StringBuilder edi, int chargeCount, PrimaryCharge charge)
        {
            var svc = new Svc(charge);
            edi.Append(svc.BuildSvc());
            segmentCount++;

            var dateOfServiceDtm = new Dtm(charge);
            edi.Append(dateOfServiceDtm.BuildDtm());
            segmentCount++;

            var copayCas = new Cas(charge);

            if (charge.AdjustmentList != null)
            {
                AddEdiAdjustmentsToEncounter(edi, charge, copayCas);

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
                    AddEdiAddonOrEmToEncounter(edi, addonCharge);
                }
            }
        }

        private void AddEdiAddonOrEmToEncounter(StringBuilder edi, AddonCharge addonCharge)
        {
            var addonsvc = new Svc(addonCharge);
            edi.Append(addonsvc.BuildSvc());
            segmentCount++;

            foreach (Adjustment adjustment in addonCharge.AdjustmentList.Where(
                a => a.AdjustmentAmount != 0 && addonCharge.AdjustmentList!=null))
            {
                var cas = new Cas(adjustment);
                edi.Append(cas.BuildCas());
                segmentCount++;

                var addonAmt = new Amt(addonCharge);
                edi.Append(addonAmt.BuildAmt());
                segmentCount++;

            }
        }

        private void AddEdiAdjustmentsToEncounter(StringBuilder edi, PrimaryCharge charge, Cas copayCas)
        {
            foreach (Adjustment adjustment in charge.AdjustmentList.Where(a => a.AdjustmentAmount != 0))
            {
                var cas = new Cas(adjustment);
                edi.Append(cas.BuildCas());
                segmentCount++;
            }

            edi.Append(copayCas.BuildCas());
        }

        private void ReturnEdiTransactionClaimPaymentInformation(StringBuilder edi, Patient patient)
        {
            var clp = new Clp(patient);
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

            var startDtm = new Dtm(patient, patient.Charges.FirstOrDefault());
            edi.Append(startDtm.BuildDtm());
            segmentCount++;
        }
    }
}
