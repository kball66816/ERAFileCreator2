using System.Text;
using EFC.BL.EDI_Segments;
using System.Collections.Generic;
using PatientManagement.Model;

namespace EFC.BL
{
    public class ElectronicDataInterchange
    {
        public string BuildEdi(List<Patient> patientList, InsuranceCompany insurance,
           Provider billingProvider)
        {
            var buildEdi = new StringBuilder();

            //Begin ISA
            var buildisa = new Isa();

            buildEdi.Append(buildisa.BuildIsa());

            //Begin GS
            var buildGs = new Gs();
            buildEdi.Append(buildGs.BuildGs());

            //Begin ST    
            var buildSt = new St();
            buildEdi.Append(buildSt.BuildSt());

            //Begin BPR 
            var buildBpr = new Bpr();
            buildEdi.Append(buildBpr.BuildBpr(insurance));

            //Build TRN
            var buildTrn = new Trn();
            buildEdi.Append(buildTrn.BuildTrn(insurance));

            //Ref Segment conditional, not included at this time
            //REF Receiver Identification
            //REF01 Receiver Reference Number
            //REF02 Receiver Reference Identification

            //Build DTM Loop Production Date
            var buildDtm = new Dtm();
            buildEdi.Append(buildDtm.BuildDtm(patientList));

            //Build N1 Insurance Company Identification Segment 1000A
            var buildNOne = new N1();
            buildEdi.Append(buildNOne.BuildNOne(insurance));

            //BuildN3 Insurance Company Identification Segment 1000A
            var buildNThree = new N3();
            buildEdi.Append(buildNThree.BuildNThree(insurance));

            // Build N4 Insurance Company Identification 1000A
            var buildNFour = new N4();
            buildEdi.Append(buildNFour.BuildN4(insurance));

            //Build Ref Insurance Company Identification 1000A
            var buildRef = new Ref();
            buildEdi.Append(buildRef.BuildRef());

            //Build Per Insurance Company Identification 1000A
            var buildPer = new Per();
            buildEdi.Append(buildPer.BuildPerPhone());

            //Build Per Insurance Company Website Information 1000A  
            buildEdi.Append(buildPer.BuildPerWebSite());

            //Build N1 Provider Identifier Segment 1000B
            buildEdi.Append(buildNOne.BuildNOne(billingProvider));

            //Build N3 Provider Identifier Segment 1000B
            buildEdi.Append(buildNThree.BuildNThree(billingProvider));

            //Build N4 Provider Identifier Segment 1000B
            buildEdi.Append(buildNFour.BuildN4(billingProvider));

            //Build Ref Provider Identifier 1000B
            buildEdi.Append(buildRef.BuildRefAdditionalPayee());
            buildEdi.Append(buildRef.BuildRefAdditionalPayeeTwo());

            //LX Segment 2000B
            var buildLx = new Lx();
            buildEdi.Append(buildLx.BuildLx());

            //TS3 2000B NOT USED
            //TS2 2000B NOT USED             
            //CLP Segment 2100

            foreach (Patient patient in patientList)
            {

                foreach (PrimaryCharge charge in patient.Charges)
                {
                    var buildClp = new Clp();
                    buildEdi.Append(buildClp.BuildClp(patient));

                    var buildNmOne = new Nm1();
                    buildEdi.Append(buildNmOne.BuildNm1(patient));


                    //MIA Inpatient Adjudication Information
                    //MOA Outpatient Adjudication Information
                    //REF Other CLaim Related Identification


                    //Ref Rendering Provider Identifier 2100
                    buildEdi.Append(buildRef.BuildRef(patient.RenderingProvider));

                    //DTM Statement From or To Date 2100
                    buildEdi.Append(buildDtm.BuildDtm(patient));

                    //PER Claim Contact Information 2100

                    //SVC Level 2110
                    var buildSvc = new Svc();

                    buildEdi.Append(buildSvc.BuildSvc(charge));
                    buildEdi.Append(buildDtm.BuildDtm(charge));

                    var buildCas = new Cas();
                    if (charge.AdjustmentList != null)
                    {
                        buildEdi.Append(buildCas.BuildCas(charge.AdjustmentList));
                        buildEdi.Append(buildCas.BuildCas(charge));
                    }
                    else
                    {
                        buildEdi.Append(buildCas.BuildCas(charge));
                    }
                    buildEdi.Append(buildRef.BuildRefControlNumber());

                    var buildAmt = new Amt();
                    buildEdi.Append(buildAmt.BuildAmt(charge));

                    //QTY 2110
                    //LQ 2110
                    //LQ01 Service Line Remittance Remark Code 1
                    //LQ02 Service Line Remittance Remark Code 2

                    foreach (AddonCharge addon in charge.AddonChargeList)
                    {
                        buildEdi.Append(buildSvc.BuildSvc(addon));
                        buildEdi.Append(buildCas.BuildCas(addon.AdjustmentList));
                        buildEdi.Append(buildRef.BuildRefControlNumber());
                        buildEdi.Append(buildAmt.BuildAmt(addon));
                    }
                }
                
            }

            var buildSe = new Se();
            buildEdi.Append(buildSe.BuildSe());

            var buildGe = new Ge();
            buildEdi.Append(buildGe.BuildGe());

            var buildIea = new Iea();
            buildEdi.Append(buildIea.BuildIea());

            return buildEdi.ToString();

        }

    }
}
