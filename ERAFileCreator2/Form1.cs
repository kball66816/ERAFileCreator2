using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace ERAFileCreator
{
    public partial class Form1 : Form
    {
        InsuranceCompany Insurance = new InsuranceCompany();
        Patient Patient;
        Provider Providers = new Provider();
        StringBuilder BuildEDI = new StringBuilder();


        public Form1()
        {
            InitializeComponent();
            SettingsFromLastVersion();
            CheckIfDefaultSettingsExist();
            LoadInitialPatient();
        }
        /// <summary>
        /// Merges the settings file from the users current version to 
        /// the updated app
        /// </summary>
        private void SettingsFromLastVersion()
        {

            if (Settings1.Default.UpgradeSettings == true)
            {
                ShowCurrentUpdate();
                Settings1.Default.Upgrade();
                Settings1.Default.UpgradeSettings = false;
                Settings1.Default.Save();
            }

        }

        /// <summary>
        /// create a patient on application startup and then add them to list
        /// this will prevent a list from being empty
        /// </summary>
        private void LoadInitialPatient()
        {

            Patient = new Patient();
            PatientsList.Add(Patient);
            Patient.PatientList = PatientsList;
            PatientsInList.Text = PatientsList.Count.ToString();
            AddtoListButton.Enabled = false;
            patientInformationPanel.Hide();
            options.Hide();
            PaymentDetailsPanel.Show();
        }

        /// <summary>
        /// Save File
        /// </summary>
        private void SaveToFile()
        {
            using (SaveFileDialog SaveEDI = new SaveFileDialog())
            {

                SaveEDI.Filter = "Text Files| *.txt";
                SaveEDI.DefaultExt = "txt";
                SaveEDI.FileName = DateTime.Now.ToString("yyyy_MM_dd_hmmssff");
                if (SaveEDI.ShowDialog(this) == DialogResult.OK)
                {
                    File.WriteAllText
                    (SaveEDI.FileName, BuildEDI.ToString());
                }
                saveLastPatientDetails();
                BuildEDI.Clear();
                ClearPatientList();
                
            }
        }

        /// <summary>
        /// Builds the EDI information to a string for saving
        /// </summary>
        private string EdiToString()
        {
            List<Patient> patientlist = PatientsList;
            Patient patient = Patient;
            DateTime today = DateTime.Now;

            Providers.ProviderFirstName = providerFirstNameInput.Text;
            Providers.ProviderLastName = providerLastNameInput.Text;
            Providers.ProviderNPI = providerNPIInput.Text;
            Insurance.InsuranceCompanyName = insuranceCoNameInput.Text;
            Insurance.InsuranceCompanyTaxID = insuranceCoTaxIDInput.Text;
            Insurance.InsuranceCheckTotal = Convert.ToDecimal(checkAmountInput.Text);
            Insurance.PaymentType = paymentTypeInput.Text;

            //Begin ISA

            BuildEDI.Append("ISA*");
            BuildEDI.Append("00*");
            BuildEDI.Append("          *");
            BuildEDI.Append("00*");
            BuildEDI.Append("          *");
            BuildEDI.Append("30*");
            BuildEDI.Append("166055205UPSERB*");
            BuildEDI.Append("30*");
            BuildEDI.Append("074056672XPCMXJ*");
            BuildEDI.Append("160603*");
            BuildEDI.Append("0839*");
            BuildEDI.Append("^*");
            BuildEDI.Append("00501*");
            BuildEDI.Append("201541257*");
            BuildEDI.Append("0*");
            BuildEDI.Append("P*");
            BuildEDI.Append(":");
            BuildEDI.Append("~");

            //Begin GS
            BuildEDI.Append("GS*");
            BuildEDI.Append("HP*");
            BuildEDI.Append("7802840731*");
            BuildEDI.Append("7234068*");
            BuildEDI.Append("29169693*");
            BuildEDI.Append("0839*");
            BuildEDI.Append("201541257*");
            BuildEDI.Append("X*");
            BuildEDI.Append("005010X221A1");
            BuildEDI.Append("~");

            //Begin ST            
            BuildEDI.Append("ST*");
            BuildEDI.Append("835*");
            BuildEDI.Append("000000001");
            BuildEDI.Append("~");

            //Begin BPR          
            BuildEDI.Append("BPR*");
            BuildEDI.Append("C*"); // BPR01 Transaction Handling Code
            BuildEDI.Append(Insurance.InsuranceCheckTotal); // BPR02 Monetary Amount
            BuildEDI.Append("*");
            BuildEDI.Append("C*"); //BPR03 Credit/Debit Flag
            BuildEDI.Append(Insurance.PaymentType); //BPR04 Payment Method Code
            BuildEDI.Append("*");
            BuildEDI.Append("CCP*"); //BPR05 Payment Format Code
            BuildEDI.Append("01*"); //BPR06 DFI ID Number Qualifier
            BuildEDI.Append("043000096*"); //BPR07 ABA Transit Routing Number
            BuildEDI.Append("DA*"); //BPR08 Account Number Qualifier
            BuildEDI.Append("0*"); //BPR09 Sender Bank Account NUmber
            BuildEDI.Append("5135511997*"); //BPR10 Originating Company Identifier
            BuildEDI.Append("*");
            BuildEDI.Append("01*"); //BPR12 ABA Transit Routing Number
            BuildEDI.Append("GAPFILL*");
            BuildEDI.Append("DA*"); //BPR013 Account Number Qualifier
            BuildEDI.Append("0*"); //BPR015 Receiver or Provider Account Number
            BuildEDI.Append(paymentDateInput.Value.ToString("yyyyMMdd")); //BPR016 Check Issue/Effective date
            BuildEDI.Append("~");

            //Build TRN
            BuildEDI.Append("TRN*");
            BuildEDI.Append("1*"); // TRN01 Trace Type Code
            BuildEDI.Append(today.ToString("yyyyMMddhhmmssff")); //TRN02 Reference Identification(Check Number)
            BuildEDI.Append("*");
            BuildEDI.Append("1"); //TRN03 Originating Company Identifier Prefix
            BuildEDI.Append(Insurance.InsuranceCompanyTaxID); //TRN03 Originating Company Identifier
            BuildEDI.Append("*");
            BuildEDI.Append("13551"); //TRN04 Originating COmpany SUpplemental Code
            BuildEDI.Append("~");

            //Ref Segment conditional, not included at this time
            //REF Receiver Identification
            //REF01 Receiver Reference Number
            //REF02 Receiver Reference Identification

            //Build DTM Loop Production Date
            BuildEDI.Append("DTM*");
            BuildEDI.Append("405*");
            BuildEDI.Append(patient.ChargeList.First().DateofService);
            BuildEDI.Append("~");

            //Build N1 Insurance Company Identification Segment 1000A
            BuildEDI.Append("N1*");
            BuildEDI.Append("PR*");//N101 Payer Identifier Code
            BuildEDI.Append(Insurance.InsuranceCompanyName); //N102 Payer Name
            BuildEDI.Append("*");
            BuildEDI.Append("XV*"); //N103 Identification Code Qualifier
            BuildEDI.Append("20123"); //N104 Payer Identification Code
            BuildEDI.Append("~");

            //BuildN3 Insurance Company Identification Segment 1000A
            BuildEDI.Append("N3*");
            BuildEDI.Append("123 LYON STREET*"); //N301 Payer Address Line
            BuildEDI.Append("~");


            // Build N4 Insurance Company Identification 1000A
            BuildEDI.Append("N4*");
            BuildEDI.Append("DENISE*"); //N401 Payer City Line
            BuildEDI.Append("NY*");     //N402 Payer City Name
            BuildEDI.Append("93945*"); // N403 Payer City Zip
            BuildEDI.Append("~");

            //Build Ref Insurance Company Identification 1000A
            BuildEDI.Append("REF*");
            BuildEDI.Append("2U*"); //
            BuildEDI.Append("20123");
            BuildEDI.Append("~");

            //Build Per Insurance Company Identification 1000A
            BuildEDI.Append("PER*");
            BuildEDI.Append("BL*"); //PER01 Contact Functional Code
            BuildEDI.Append("EDI SUPPORT*"); //PER02 Payer Contact Name
            BuildEDI.Append("TE*"); //PER03 Communication Number Qualifier
            BuildEDI.Append("410612055"); //PER04 Payer Contact Communication Number 
            BuildEDI.Append("~");

            //Build Per Insurance Company Website Information 1000A  
            BuildEDI.Append("PER*");
            BuildEDI.Append("IC*"); //PER01 Contact Function Code
            BuildEDI.Append("*"); //PER02 Name
            BuildEDI.Append("UR*"); //PER03 Communication Number Qualifier
            BuildEDI.Append("WWW.WEBSITE.COM"); //PER04 Communication NUmber
            BuildEDI.Append("~");

            //Build N1 Provider Identifier Segment 1000B
            BuildEDI.Append("N1*");
            BuildEDI.Append("PE*"); //N101 Payer Identifier Code

            string providerFullName = Providers.ProviderFirstName + " " + Providers.ProviderLastName;
            BuildEDI.Append(providerFullName); //N102 Payee Name
            BuildEDI.Append("*");
            BuildEDI.Append("XX*"); //N103 Payee Identification Qualifier
            BuildEDI.Append(Providers.ProviderNPI); //N104 Payee Identification Code
            BuildEDI.Append("~");


            //Build N3 Provider Identifier Segment 1000B
            BuildEDI.Append("N3*");
            BuildEDI.Append("123 PROVIDER RD*"); //N301 Payee Address Line
            BuildEDI.Append("STE 1"); //N302 Payee Address Line
            BuildEDI.Append("~");

            //Build N4 Provider Identifier Segment 1000B
            BuildEDI.Append("N4*");
            BuildEDI.Append("CITY*"); //N401 Payee City Name
            BuildEDI.Append("ST*"); //N402 Payee State Name
            BuildEDI.Append("12345"); //N403 Payee Payee Postal Zone or Zip Code
            BuildEDI.Append("~");

            //Build Ref Provider Identifier 1000B
            BuildEDI.Append("REF*");
            BuildEDI.Append("PQ*"); //Ref01 Additional Payee Identification Qualifier
            BuildEDI.Append("1440054"); //Ref02 Reference Identification Code
            BuildEDI.Append("~");
            BuildEDI.Append("REF*");
            BuildEDI.Append("TJ*"); //Ref01 Additional Payee Identification Qualifier
            BuildEDI.Append("122074233"); //Ref02 Additional reference Identification Code
            BuildEDI.Append("~");

            //LX Segment 2000B
            BuildEDI.Append("LX*");
            BuildEDI.Append("1"); //Claim Sequence Number
            BuildEDI.Append("~");

            //TS3 2000B NOT USED
            //TS2 2000B NOT USED             
            //CLP Segment 2100
            //Potential TODO
            //Refactor as a method?
            //Would require to pull PatientPaymentInfo out
            //of current home Research potential effects
            foreach (Patient name in patient.PatientList)
            {
                BuildEDI.Append("CLP*");
                if (isNotPlatform.Checked == true)//CLP01 for Suite Use
                {
                    BuildEDI.Append("1");
                    {
                        ReturnClassicBillID(name);
                    }
                }
                else if (isNotPlatform.Checked == false)
                {
                    BuildEDI.Append(name.BillId + "-1"); //CLP01 for Platform Use
                }

                BuildEDI.Append("*");
                BuildEDI.Append("1*");//CLP02 Claim Status Code 
                BuildEDI.Append(name.ChargeList.First().TotalCostofCharges()); //CLP03 Total Claim Charge Amount
                BuildEDI.Append("*");
                BuildEDI.Append(name.ChargeList.First().PrimaryPaymentAmount);//CLP04 Total Claim Payment Amount
                BuildEDI.Append("*");
                BuildEDI.Append(patient.PatientCopay);//CLP05 Patient Responsibility Amount
                BuildEDI.Append("*");
                BuildEDI.Append("12*");//CLP06 Claim Filing Indicator Code
                BuildEDI.Append("EMC5841338*");//CLP07 Payer Claim Control Number
                BuildEDI.Append("11");//CLP08 Facility Type Code
                                      //CLP09 CLaim Frequency Code
                                      //CLP10 Patient Status Code
                                      //CLP11 Diagnosis Related Group (DRG) Code
                                      //CLP12 DRG Weight
                                      //CLP13 Percent Discharge Fraction
                BuildEDI.Append("~");


                //Potential TODO
                //CAS Here?
                //Conflicting information. Possibly Claim level instead of line level?

                //NM1 Patient Identifier 2100
                BuildEDI.Append("NM1*");
                BuildEDI.Append("QC*");  //NM101 Patient Identifier Code
                BuildEDI.Append("1*"); //NM102 Entity Type Qualifier
                BuildEDI.Append(name.PatientLastName);//NM103 Patient Last Name
                BuildEDI.Append("*");
                BuildEDI.Append(name.PatientFirstName); //NM104 Patient First Name
                BuildEDI.Append("*");
                BuildEDI.Append("*"); //NM105 Patient Middle Initial
                BuildEDI.Append("*"); //NM106 Name Prefix
                BuildEDI.Append("*"); //NM107 Patient Name Suffix
                BuildEDI.Append("MI*");//NM108 Identification Code Qualifier
                BuildEDI.Append("633154152*"); //NM109 Patient Member Number
                BuildEDI.Append("~");

                //NM1 Insured Identifier 2100

                if (IncludeSubscriberCheckbox.Checked == true)
                {
                    BuildEDI.Append("NM1*");
                    BuildEDI.Append("IL*"); //NM101 Insured Name
                    BuildEDI.Append("1*"); //NM102 Insured Entity Type Qualifier
                    BuildEDI.Append("MORIN*"); //NM103 Insured Last Name
                    BuildEDI.Append("MATTHEW*"); //NM104 Insured First Name
                    BuildEDI.Append("*"); //NM105 Insured Middle Initial
                    BuildEDI.Append("*"); //NM106 Insured Name Prefix
                    BuildEDI.Append("*"); //NM107 Insured Name Suffix
                    BuildEDI.Append("MI*"); //NM108 Identification Code Qualifier
                    BuildEDI.Append("633154111"); //NM109 Insured Member Number
                    BuildEDI.Append("~");
                }

                //NM1 Service Provider 2100
                BuildEDI.Append("NM1*");
                BuildEDI.Append("82*"); //NM101 Entity Identifier Code
                BuildEDI.Append("1*"); //NM102 Entity Type Qualifier
                BuildEDI.Append(Providers.ProviderLastName); //NM103 Rendering Provider Last Name
                BuildEDI.Append("*");
                BuildEDI.Append(Providers.ProviderFirstName);//NM104 Rendering Provider First Name
                BuildEDI.Append("*");
                BuildEDI.Append("*"); //NM105 Rendering Provider Middle Name
                BuildEDI.Append("*"); //NM106 Name Prefix
                BuildEDI.Append("*"); //NM107 Rendering Provider Name Suffix
                BuildEDI.Append("XX*"); //NM108 Rendering Provider Identification Code Qualifier
                BuildEDI.Append(Providers.ProviderNPI); //NM109 Rendering Provider Identifier
                BuildEDI.Append("~");

                //NM1 Crossover Carrier
                //NM1 Correct Priority Payer
                //MIA Inpatient Adjudication Information
                //MOA Outpatient Adjudication Information
                //REF Other CLaim Related Identification


                //Ref Rendering Provider Identifier 2100
                BuildEDI.Append("REF*");
                BuildEDI.Append("G2*");
                BuildEDI.Append("7570867");
                BuildEDI.Append("~");

                //DTM Statement From or To Date 2100
                BuildEDI.Append("DTM*");
                BuildEDI.Append("232*"); //DTM01 Date Time Qualifier
                BuildEDI.Append(name.ChargeList.First().DateofService); //DTM02 Start Date
                BuildEDI.Append("~");

                //DTM Coverage Expiration Date 2100
                BuildEDI.Append("DTM*");
                BuildEDI.Append("233*"); //DTM01 Date Time Qualifier
                BuildEDI.Append(name.ChargeList.First().DateofService); //DTM02 Expiration Date
                BuildEDI.Append("~");

                //DTM Claim Received Date
                BuildEDI.Append("DTM*");
                BuildEDI.Append("050*"); //DTM01 Date Time Qualifier
                BuildEDI.Append(name.ChargeList.First().DateofService); //DTM02 Date of Service Date
                BuildEDI.Append("~");

                //PER Claim Contact Information 2100

                //SVC Level 2110
                BuildEDI.Append("SVC*");
                BuildEDI.Append("HC:"); //SVC01-1 Service Type Code
                BuildEDI.Append(name.ChargeList.First().PrimaryProcedureCode); //SVC01-2 Service Code

                //SVC 01-03 through 01-07 contingent on modifiers and description
                if (!string.IsNullOrEmpty(name.ChargeList.First().ModifierOne))
                {
                    BuildEDI.Append(":");
                    BuildEDI.Append(name.ChargeList.First().ModifierOne);
                }
                if (!string.IsNullOrEmpty(name.ChargeList.First().ModifierTwo))
                {
                    BuildEDI.Append(":");
                    BuildEDI.Append(name.ChargeList.First().ModifierTwo);
                }
                if (!string.IsNullOrEmpty(name.ChargeList.First().ModifierThree))
                {
                    BuildEDI.Append(":");
                    BuildEDI.Append(name.ChargeList.First().ModifierThree);
                }
                if (!string.IsNullOrEmpty(name.ChargeList.First().ModifierFour))
                {
                    BuildEDI.Append(":");
                    BuildEDI.Append(name.ChargeList.First().ModifierFour);
                }
                //svc0107 Procedure Code Description
                BuildEDI.Append("*");

                //Potential TODO
                //Implement conversion as method keep as Convert or change to
                //Tryparse?

                BuildEDI.Append(name.ChargeList.First().PrimaryChargeCost); //SVC02 Monetary Amount
                BuildEDI.Append("*");
                BuildEDI.Append(name.ChargeList.First().PrimaryPaymentAmount); //SVC03 Monetary Amount
                BuildEDI.Append("*");
                BuildEDI.Append("*"); //SVC04 NUBC Revenue Code
                BuildEDI.Append("1"); //SVC05 Units of Service Paid Count

                //SVC06-1 Product/Service ID Qualifier
                //SVC06-2 Procedure Code
                //SVC06-3 Procedure Modifier 1
                //SVC06-4 Pocedure Modifier 2
                //SVC06-5 Procedure Modifier 3
                //SVC06-6 Procedure Modifier 4
                //SVC06-7 Procedure Code Description
                //SVC07 Original Units of Service Count

                BuildEDI.Append("~");

                //DTM Service Start Date 2110
                //DTM01 Date Time QUalifier
                //DTM02 Service Date

                //DTM Service End Date 2110
                //DTM01 Date Time Qualifier
                //DTM02 Service Date

                //DTM Service Date 2110
                BuildEDI.Append("DTM*");
                BuildEDI.Append("472*"); //DTM01 Date Time Qualifier
                BuildEDI.Append(name.ChargeList.First().DateofService); //DTM02 Service Date
                BuildEDI.Append("~");


                //CAS Segment Insurance Adjustment 2110
                BuildEDI.Append("CAS*");
                BuildEDI.Append("CO*"); //CAS01 Claim Adjustment Group Code

                //Potential TODO recouple as list
                //Include enumeration for adjustment reason codes
                //Include existing adjustment codes or implement as freetext?
                //Change UI to add n number of adjustments+reasoncodes

                BuildEDI.Append("45*"); //Cas02 Adjustment Reason Code
                BuildEDI.Append(name.ChargeList.First().PrimaryChargeContractualAdjustment); //Cas03 Adjustment Quantity
                BuildEDI.Append("~");
                //Cas05-19 Repeat of reason code, Amount, and Quantity up to 5 times

                //Cas Segment Patient Adjustment
                BuildEDI.Append("CAS*");
                BuildEDI.Append("PR*"); //Cas01 Claim Adjustment Group Code
                BuildEDI.Append("3*"); //Cas02 Adjustment Reason Code
                BuildEDI.Append(name.PatientCopay); //Cas03 Adjustment Quantity
                BuildEDI.Append("~");

                //REF Service Identification 2110
                //REF01 Reference Identification Qualifier
                //REF02 Provider Identifier


                //REF Line Item Control Number 2110
                BuildEDI.Append("REF*");
                BuildEDI.Append("6R*"); //REF01 Reference Identification Qualifier
                BuildEDI.Append("1"); //REF02 Reference Identification
                BuildEDI.Append("~");

                //REF Rendering Provider Information 2110
                //REF01 Reference Identification NUmber
                //REF02 Rendering Provider Federal ID

                //AMT Segment Service Supplemental Amount
                BuildEDI.Append("AMT*");
                BuildEDI.Append("B6*"); //Amount Qualifier Code
                decimal serviceLineAllowedAmount = name.ChargeList.First().PrimaryPaymentAmount + name.PatientCopay
                    + name.ChargeList.First().AddonChargeList.Sum(addon => addon.AddonPaymentAmount);
                BuildEDI.Append(serviceLineAllowedAmount);//AMT02 Service Line Allowed Amount
                BuildEDI.Append("~");

                //QTY 2110
                //LQ 2110
                //LQ01 Service Line Remittance Remark Code 1
                //LQ02 Service Line Remittance Remark Code 2


                foreach (AddonCharge addon in name.ChargeList.First().AddonChargeList)
                {
                    if (name.ChargeList.First().AddonChargeList.First().AddonProcedureCode != null)
                        BuildEDI.Append("SVC*");
                    BuildEDI.Append("HC:");
                    BuildEDI.Append(addon.AddonProcedureCode);

                    if (!string.IsNullOrEmpty(name.ChargeList.First().AddonChargeList.First().ModifierOne))
                    {
                        BuildEDI.Append(":");
                        BuildEDI.Append(name.ChargeList.First().AddonChargeList.First().ModifierOne);
                    }
                    if (!string.IsNullOrEmpty(name.ChargeList.First().AddonChargeList.First().ModifierTwo))
                    {
                        BuildEDI.Append(":");
                        BuildEDI.Append(name.ChargeList.First().AddonChargeList.First().ModifierTwo);
                    }
                    if (!string.IsNullOrEmpty(name.ChargeList.First().AddonChargeList.First().ModifierThree))
                    {
                        BuildEDI.Append(":");
                        BuildEDI.Append(name.ChargeList.First().AddonChargeList.First().ModifierThree);
                    }
                    if (!string.IsNullOrEmpty(name.ChargeList.First().AddonChargeList.First().ModifierFour))
                    {
                        BuildEDI.Append(":");
                        BuildEDI.Append(name.ChargeList.First().AddonChargeList.First().ModifierFour);
                    }
                    BuildEDI.Append("*");
                    BuildEDI.Append(addon.AddonChargeCost);
                    BuildEDI.Append("*");
                    BuildEDI.Append(addon.AddonPaymentAmount);
                    BuildEDI.Append("*");
                    BuildEDI.Append("*");
                    BuildEDI.Append("1");
                    BuildEDI.Append("~");

                    //Cas Segment Addon
                    BuildEDI.Append("CAS*");
                    BuildEDI.Append("CO*");
                    BuildEDI.Append("45*");
                    BuildEDI.Append(addon.AddonContractualAdjustment);
                    BuildEDI.Append("~");
                    BuildEDI.Append("REF*");
                    BuildEDI.Append("6R*");
                    BuildEDI.Append("2");
                    BuildEDI.Append("~");
                    BuildEDI.Append("AMT*");
                    BuildEDI.Append("B6*");
                    BuildEDI.Append(addon.AddonPaymentAmount);
                    BuildEDI.Append("~");
                }
            }


            //SVC Addon Code


            //Begin SE
            BuildEDI.Append("SE*");
            BuildEDI.Append("74*");
            BuildEDI.Append("~");

            //Begin GE
            BuildEDI.Append("GE*");
            BuildEDI.Append("1*");
            BuildEDI.Append("201541257");
            BuildEDI.Append("~");

            //BeginIEA                
            BuildEDI.Append("IEA*");
            BuildEDI.Append("1*");
            BuildEDI.Append("201541257");
            BuildEDI.Append("~");
            return BuildEDI.ToString();

        }

        private void ReturnClassicBillID(Patient name)
        {
            string substringofPatientFirstName;

            if (name.PatientFirstName.Length > 3)
            {
                substringofPatientFirstName = name.PatientFirstName.Substring(3, 0);
            }

            else substringofPatientFirstName = name.PatientFirstName;

            string substringofPatientLastName;
            if (name.PatientLastName.Length > 3)
            {
                substringofPatientLastName = name.PatientLastName.Substring(3, 0);
            }

            else substringofPatientLastName = name.PatientLastName;
            string concatenatedClassicIdFormat = substringofPatientLastName + substringofPatientFirstName;


            BuildEDI.Append(name.BillId.PadLeft(10, '0'));
            BuildEDI.Append(concatenatedClassicIdFormat);
        }

        private void CopayInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void CheckIfDefaultSettingsExist()
        {
            if (Settings1.Default.IncludeSubscriber == false)
            {
                IncludeSubscriberCheckbox.Checked = false;
            }

            else if (Settings1.Default.IncludeSubscriber == true)
            {
                IncludeSubscriberCheckbox.Checked = true;
            }
            if (Settings1.Default.BillIDPreference == true)
            {
                isNotPlatform.Checked = false;
                isPlatform.Checked = true;
            }
            paymentTypeInput.SelectedIndex = 1;
            PatientReusePreference();
            AddonPreferenceLoad();
            ProviderLoad();
            InsuranceCompanyLoad();
            PatientLoadPreference();
        }

        private void PatientReusePreference()
        {

            if (Settings1.Default.ReUseLastPatientPrompt == true)
            {
                AlwaysPromptPatient.Checked = true;
                disablePatientPromptAndReuse.Checked = false;
                disablePromptAndDontReusePatient.Checked = false;
            }

            else if (Settings1.Default.NeverReUseLastPatient == true)
            {
                AlwaysPromptPatient.Checked = false;
                disablePatientPromptAndReuse.Checked = false;
                disablePromptAndDontReusePatient.Checked = true;
            }

            else if (Settings1.Default.ReUseLastPatientWithoutPrompt == true)
            {
                AlwaysPromptPatient.Checked = false;
                disablePatientPromptAndReuse.Checked = true;
                disablePromptAndDontReusePatient.Checked = false;
            }
        }

        private void AddonPreferenceLoad()
        {
            if (Settings1.Default.ReUseAddonPrompt == true)
            {
                AlwaysPromptAddon.Checked = true;
                disablePromptAndReuseAddon.Checked = false;
                disablePromptAndNeverReuseAddon.Checked = false;
            }
            else if (Settings1.Default.ReUseAddonWithoutPrompt == true)
            {
                AlwaysPromptAddon.Checked = false;
                disablePromptAndReuseAddon.Checked = true;
                disablePromptAndNeverReuseAddon.Checked = false;
            }
            else if (Settings1.Default.NeverReUseAddon == true)
            {
                AlwaysPromptAddon.Checked = false;
                disablePromptAndReuseAddon.Checked = false;
                disablePromptAndNeverReuseAddon.Checked = true;
            }

        }

        private void ProviderLoad()
        {
            if (Settings1.Default.ProviderFirstName != null)
            {
                providerFirstNameInput.Text = Settings1.Default.ProviderFirstName;
            }
            if (Settings1.Default.ProviderLastName != null)
            {
                providerLastNameInput.Text = Settings1.Default.ProviderLastName;
            }

            if (Settings1.Default.ProviderNPI != null)
            {
                providerNPIInput.Text = Settings1.Default.ProviderNPI;
            }
        }

        private void InsuranceCompanyLoad()
        {
            if (Settings1.Default.InsuranceCompanyName != null)
            {
                insuranceCoNameInput.Text = Settings1.Default.InsuranceCompanyName;
            }
            if (Settings1.Default.InsuranceCompanyTaxID != null)
            {
                insuranceCoTaxIDInput.Text = Settings1.Default.InsuranceCompanyTaxID;
            }
        }

        private void PatientLoadPreference()
        {
            if (Settings1.Default.ReloadLastPatient == true)
            {
                if (Settings1.Default.PatientFirstName != null)
                {
                    patientFirstNameInput.Text = Settings1.Default.PatientFirstName;
                }
                if (Settings1.Default.PatientLastName != null)
                {
                    patientLastNameInput.Text = Settings1.Default.PatientLastName;
                }
                if (Settings1.Default.PatientCopay != null)
                {
                    patientCopayInput.Text = Settings1.Default.PatientCopay;
                }
                lastPatientOnStartupCheckbox.Checked = true;
            }
            else if (Settings1.Default.ReloadLastPatient == false)
            {
                lastPatientOnStartupCheckbox.Checked = false;
            }


        }


        private void IsNotPlatform_CheckedChanged(object sender, EventArgs e)
        {
            if (isNotPlatform.Checked == true)
            {
                isPlatform.Checked = false;
                Settings1.Default.BillIDPreference = false;
            }
        }

        private void IsPlatform_CheckedChanged(object sender, EventArgs e)
        {
            if (isNotPlatform.Checked == false)
            {
                isPlatform.Checked = true;
                Settings1.Default.BillIDPreference = true;
            }
        }

        List<Patient> PatientsList = new List<Patient>();

        private void AddtoListButton_Click(object sender, EventArgs e)
        {
            //create a new list and match it to the static list
            List<Patient> patientList = PatientsList;
            clearPatientDetails();

            //return a new patient with null details
            Patient = new Patient();

            //Opens up text fields to allow a new patient entry
            EnablePatientDataFields();

            patientList.Add(Patient);
            Patient.PatientList = PatientsList;
            AddAddOn.Text = "Add Charge";
            PatientsInList.Text = patientList.Count.ToString();
            AddonCodeCountPerPatient.Text = "0";
            AddtoListButton.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
        }

        private void AddAddOn_Click(object sender, EventArgs e)
        {
            List<Patient> patientList = PatientsList;

            AddChargeToPatientList();
        }

        private void AddChargeToPatientList()
        {
            //Holding charge to assign based on whether the patient has a charge or not
            Charge charge;

            CheckIfAddonchargeisValid();
            CalculateCharges();

            if (Patient.ChargeList.Count < 1)
            {
                EnableSaveAs();
                SetPatientDataFromForm();

                charge = new Charge();
                AddNewChargeToPatient(charge);

            }
            else
            {
                SetPatientDataFromForm();
                charge = Patient.ChargeList.First();
            }

            AddAddonToChargeList(charge);
        }

        private void AddNewChargeToPatient(Charge charge)
        {
            if (string.IsNullOrEmpty(primaryProcedureInput.Text))
            {
                InformUserAPrimaryProcedureIsNeeded();
            }

            else if (!string.IsNullOrEmpty(primaryProcedureInput.Text))
            {
                //Update text label to reflect change from adding a charge to adding add-on codes
                AddAddOn.Text = "Add Add-On";

                SetPrimaryProcedureValuesFromForm(charge);
                Patient.ChargeList.Add(charge);

                //disables modification of charge in list until a new patient is added.
                //To confirm the charge has been added to the existing patient on screen.
                TextFieldsToReadOnly();
                AddtoListButton.Enabled = true;
            }
        }

        private void SetPatientDataFromForm()
        {
            Patient.PatientFirstName = patientFirstNameInput.Text;
            Patient.PatientLastName = patientLastNameInput.Text;
            decimal patientCopayValue;
            decimal.TryParse(patientCopayInput.Text, out patientCopayValue);
            {

                Patient.PatientCopay = patientCopayValue;
            }
            Patient.BillId = billIdInput.Text;
        }

        private static void InformUserAPrimaryProcedureIsNeeded()
        {
            MessageBox.Show("You must have a charge");
        }

        private void SetPrimaryProcedureValuesFromForm(Charge charge)
        {
            charge.DateofService = dateofServiceInput.Value.ToString("yyyyMMdd");
            charge.PrimaryProcedureCode = primaryProcedureInput.Text;
            CheckForModifiersToAddToChargeList(charge);
            ParsePrimaryChargeInput(charge);
            ParsePrimaryAdjustmentInput(charge);
            ParsePrimaryPaidInput(charge);
        }

        private void ParsePrimaryPaidInput(Charge charge)
        {
            if (!string.IsNullOrEmpty(primaryPaidInput.Text))
            {
                decimal value;
                decimal.TryParse(primaryPaidInput.Text, out value);
                {
                    charge.PrimaryPaymentAmount = value;
                }
            }
        }

        private void ParsePrimaryAdjustmentInput(Charge charge)
        {
            if (!string.IsNullOrEmpty(primaryAdjustmentInput.Text))
            {
                decimal value;
                decimal.TryParse(primaryAdjustmentInput.Text, out value);
                {
                    charge.PrimaryChargeContractualAdjustment = value;
                }
            }
        }

        private void ParsePrimaryChargeInput(Charge charge)
        {
            if (!string.IsNullOrEmpty(primaryChargeInput.Text))
            {
                decimal value;
                decimal.TryParse(primaryChargeInput.Text, out value);
                charge.PrimaryChargeCost = value;
            }
        }

        private void CheckForModifiersToAddToChargeList(Charge charge)
        {
            if (!string.IsNullOrEmpty(primaryModifierOneInput.Text))
            {
                charge.ModifierOne = primaryModifierOneInput.Text;
                IncludeModifierTwoIfModifierOneExists(charge);
            }
        }

        private void IncludeModifierTwoIfModifierOneExists(Charge charge)
        {
            if (!string.IsNullOrEmpty(primaryModifierTwoInput.Text))
            {
                charge.ModifierTwo = primaryModifierTwoInput.Text;
                IncludeModifierThreeIfModifierThreeExists(charge);
            }
        }

        private void IncludeModifierThreeIfModifierThreeExists(Charge charge)
        {
            if (!string.IsNullOrEmpty(primaryModifierThreeInput.Text))
            {
                charge.ModifierThree = primaryModifierThreeInput.Text;

                IncludeModifierFourIfModifierThreeExists(charge);
            }
        }

        private void IncludeModifierFourIfModifierThreeExists(Charge charge)
        {
            if (!string.IsNullOrEmpty(primaryModifierFourInput.Text))
            {
                charge.ModifierFour = primaryModifierFourInput.Text;
            }
        }

        private void AddAddonToChargeList(Charge charge)
        {
            AddOnChargesToList(charge);
        }

        private void AddOnChargesToList(Charge charge)
        {
            if (!string.IsNullOrEmpty(addonProcedureInput.Text))
            {
                AddonCharge newAddonCharge = new AddonCharge();

                newAddonCharge.AddonProcedureCode = addonProcedureInput.Text;

                ParseAddonModifiers(newAddonCharge);

                ParseAddonCharge(newAddonCharge);

                ParseAddonAdjustment(newAddonCharge);

                ParseAddonPaid(newAddonCharge);
                charge.AddonChargeList.Add(newAddonCharge);
                AddonCodeCountPerPatient.Text = charge.AddonChargeList.Count.ToString();
                ReuseAddOnCode();
                EnableSaveAs();
            }
        }

        private void ParseAddonModifiers(AddonCharge newAddonCharge)
        {
            if (!string.IsNullOrEmpty(addOnModifierOneInput.Text))
            {
                newAddonCharge.ModifierOne = addOnModifierOneInput.Text;

                AddonModifierTwoIfModifierOneExists(newAddonCharge);
            }
        }

        private void ParseAddonCharge(AddonCharge newAddonCharge)
        {
            if (!string.IsNullOrEmpty(addonChargeInput.Text))
            {
                decimal value;
                decimal.TryParse(addonChargeInput.Text, out value);
                newAddonCharge.AddonChargeCost = value;
            }
        }

        private void ParseAddonAdjustment(AddonCharge newAddonCharge)
        {
            if (!string.IsNullOrEmpty(addonAdjustmentInput.Text))
            {
                decimal value;
                decimal.TryParse(addonAdjustmentInput.Text, out value);
                newAddonCharge.AddonContractualAdjustment = value;
            }
        }

        private void ParseAddonPaid(AddonCharge newAddonCharge)
        {
            if (!string.IsNullOrEmpty(addonPaidInput.Text))
            {
                decimal value;
                decimal.TryParse(addonPaidInput.Text, out value);
                newAddonCharge.AddonPaymentAmount = value;
            }
        }

        private void AddonModifierTwoIfModifierOneExists(AddonCharge newAddonCharge)
        {
            if (!string.IsNullOrEmpty(addOnModifierTwoInput.Text))
            {
                newAddonCharge.ModifierTwo = addOnModifierTwoInput.Text;

                AddonModifierThreeifModifierTwoExists(newAddonCharge);
            }
        }

        private void AddonModifierThreeifModifierTwoExists(AddonCharge newAddonCharge)
        {
            if (!string.IsNullOrEmpty(addOnModifierThreeInput.Text))
            {
                newAddonCharge.ModifierThree = addOnModifierThreeInput.Text;

                AddonModifierFourifThreeExists(newAddonCharge);
            }
        }

        private void AddonModifierFourifThreeExists(AddonCharge newAddonCharge)
        {
            if (!string.IsNullOrEmpty(addOnModifierFourInput.Text))
            {
                newAddonCharge.ModifierFour = addOnModifierFourInput.Text;
            }
        }

        private void EnableSaveAs()
        {
            saveAsToolStripMenuItem.Enabled = true;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NPIValidator Validate = new NPIValidator();
            Validate.Npi = providerNPIInput.Text;
            Validate.Validate();

            if (Validate.InvalidNPI == false)
            {
                EdiToString();
                SaveToFile();
            }

        }

        private void providerNPIInput_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void providerNPIInput_Leave(object sender, EventArgs e)
        {

        }

        private void checkAmountInput_TextChanged(object sender, EventArgs e)
        {

        }

        private void next_Click(object sender, EventArgs e)
        {

            PaymentDetailsPanel.Hide();
            patientInformationPanel.Show();
        }

        private void addonCodeCountPerPatient_TextChanged(object sender, EventArgs e)
        {

        }

        private void addonProcedureInput_Leave(object sender, EventArgs e)
        {

        }

        private void TextFieldsToReadOnly()
        {
            patientFirstNameInput.Enabled = false;
            patientLastNameInput.Enabled = false;
            patientCopayInput.Enabled = false;
            billIdInput.Enabled = false;
            primaryProcedureInput.Enabled = false;
            primaryAdjustmentInput.Enabled = false;
            primaryChargeInput.Enabled = false;
            primaryPaidInput.Enabled = false;
            dateofServiceInput.Enabled = false;
            primaryModifierOneInput.Enabled = false;
            primaryModifierTwoInput.Enabled = false;
            primaryModifierThreeInput.Enabled = false;
            primaryModifierFourInput.Enabled = false;
        }

        private void EnablePatientDataFields()
        {
            patientFirstNameInput.Enabled = true;
            patientLastNameInput.Enabled = true;
            patientCopayInput.Enabled = true;
            billIdInput.Enabled = true;
            primaryProcedureInput.Enabled = true;
            primaryAdjustmentInput.Enabled = true;
            primaryChargeInput.Enabled = true;
            primaryPaidInput.Enabled = true;
            dateofServiceInput.Enabled = true;
            primaryModifierOneInput.Enabled = true;
            primaryModifierTwoInput.Enabled = true;
            primaryModifierThreeInput.Enabled = true;
            primaryModifierFourInput.Enabled = true;
        }

        private void ReuseAddOnCode()
        {
            if (Settings1.Default.ReUseAddonPrompt == true)
            {
                DialogResult keepAddonCode = MessageBox.Show("Do you want to reuse the same Add-On Code?", "Re-use Addon?", MessageBoxButtons.YesNo);
                {
                    if (keepAddonCode == DialogResult.No)
                    {
                        ClearAddOnInformationFromForm();
                    }
                }

            }
            else if (Settings1.Default.NeverReUseAddon == true)
            {
                ClearAddOnInformationFromForm();
            }
        }

        private void CheckIfAddonchargeisValid()
        {
            if (!string.IsNullOrEmpty(addonProcedureInput.Text) && string.IsNullOrEmpty(addonPaidInput.Text))
            {
                ClearAddOnInformationFromForm();
            }
        }

        private void CalculateCharges()
        {
            //If there is no established check amount set it to the value of the first patients primary and/or addon charge.
            //if (string.IsNullOrEmpty(checkAmountInput.Text))
            //{
            //    if (primaryPaidInput.Enabled == true && (!string.IsNullOrEmpty(addonPaidInput.Text)))
            //    {
            //        decimal primaryCodeValue;
            //        decimal addonCodeValue;
            //        decimal.TryParse(primaryPaidInput.Text, out primaryCodeValue);
            //        {

            //            decimal.TryParse(addonPaidInput.Text, out addonCodeValue);
            //            {

            //            }
            //        }
            //        decimal valueOfOneCodeCombination = primaryCodeValue + addonCodeValue;
            //        checkAmountInput.Text = valueOfOneCodeCombination.ToString();
            //    }
            //    else if (primaryPaidInput.Enabled == true && (string.IsNullOrEmpty(addonPaidInput.Text)))
            //    {
            //        checkAmountInput.Text = primaryPaidInput.Text;
            //    }

            //    else if (primaryPaidInput.Enabled = false && (!string.IsNullOrEmpty(addonPaidInput.Text)))
            //    {
            //        checkAmountInput.Text = addonPaidInput.Text;
            //    }
            //    else checkAmountInput.Text = "0";
            //}

            //If check amount does exist then update the value of check amount.
            if (!string.IsNullOrEmpty(checkAmountInput.Text))
            {
                decimal calculate;
                decimal primaryPaidValue;
                decimal addonPaidValue;
                if (primaryPaidInput.Enabled == true && (string.IsNullOrEmpty(addonPaidInput.Text)))
                {

                    calculate = Convert.ToDecimal(checkAmountInput.Text);

                    decimal.TryParse(primaryPaidInput.Text, out primaryPaidValue);
                    {
                        calculate = primaryPaidValue + calculate;
                    }
                    checkAmountInput.Text = calculate.ToString();
                }
                else if (primaryPaidInput.Enabled == true && (!string.IsNullOrEmpty(addonPaidInput.Text)))
                {
                    calculate = Convert.ToDecimal(checkAmountInput.Text);

                    decimal.TryParse(primaryPaidInput.Text, out primaryPaidValue);
                    {
                        calculate = primaryPaidValue + calculate;
                    }

                    decimal.TryParse(addonPaidInput.Text, out addonPaidValue);
                    {
                        calculate = addonPaidValue + calculate;
                    }
                    //calculate = Convert.ToDecimal(primaryPaidInput.Text) + Convert.ToDecimal(addonPaidInput.Text) + calculate;
                    checkAmountInput.Text = calculate.ToString();
                }
                else if (primaryPaidInput.Enabled == false && (!string.IsNullOrEmpty(addonPaidInput.Text)))
                {
                    calculate = Convert.ToDecimal(checkAmountInput.Text);
                    calculate = Convert.ToDecimal(addonPaidInput.Text) + calculate;
                    checkAmountInput.Text = calculate.ToString();
                }

            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            patientInformationPanel.Hide();
            PaymentDetailsPanel.Show();
            options.Hide();
        }

        private void paymentDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            patientInformationPanel.Hide();
            PaymentDetailsPanel.Show();
            options.Hide();
        }

        private void patientInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            patientInformationPanel.Show();
            PaymentDetailsPanel.Hide();
            options.Hide();
        }

        private void clearPatientDetails()
        {
            if (Settings1.Default.ReUseLastPatientPrompt == true)
            {
                DialogResult keepPatient = MessageBox.Show("Is this the same patient?", "Repeat Patient", MessageBoxButtons.YesNo);
                {

                    if (keepPatient == DialogResult.No)
                    {
                        ClearPatientFromForm();
                        ClearChargesFromForm();

                    }
                }
            }

            else if (Settings1.Default.NeverReUseLastPatient == true)
            {
                ClearPatientFromForm();
                ClearChargesFromForm();
            }
        }

        private void saveLastPatientDetails()
        {

            Settings1.Default.PatientFirstName = patientFirstNameInput.Text;
            Settings1.Default.PatientLastName = patientLastNameInput.Text;
            Settings1.Default.PatientCopay = patientCopayInput.Text;
            Settings1.Default.InsuranceCompanyName = insuranceCoNameInput.Text;
            Settings1.Default.InsuranceCompanyTaxID = insuranceCoTaxIDInput.Text;
            Settings1.Default.ProviderFirstName = providerFirstNameInput.Text;
            Settings1.Default.ProviderLastName = providerLastNameInput.Text;
            Settings1.Default.ProviderNPI = providerNPIInput.Text;
            Settings1.Default.Save();
        }

        private void ClearPatientList()
        {
            Patient.PatientList.Clear();
            PatientsInList.Text = Patient.PatientList.Count.ToString();
            EnablePatientDataFields();
            AddAddOn.Text = "Add Charge";
            checkAmountInput.Text = "0.00";
            AddonCodeCountPerPatient.Text = "0";
        }

        private void lastPatientOnStartupCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (lastPatientOnStartupCheckbox.Checked == false)
            {
                Settings1.Default.ReloadLastPatient = false;
                Settings1.Default.Save();
            }

            else if (lastPatientOnStartupCheckbox.Checked == true)
            {
                Settings1.Default.ReloadLastPatient = true;
                Settings1.Default.Save();
            }

        }

        private void reUsePatientPromptCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void alwaysRadio_CheckedChanged(object sender, EventArgs e)
        {
            Settings1.Default.ReUseLastPatientPrompt = true;
            Settings1.Default.ReUseLastPatientWithoutPrompt = false;
            Settings1.Default.NeverReUseLastPatient = false;
            Settings1.Default.Save();
        }

        private void disablePromptButReuse_CheckedChanged(object sender, EventArgs e)
        {
            Settings1.Default.ReUseLastPatientWithoutPrompt = true;
            Settings1.Default.ReUseLastPatientPrompt = false;
            Settings1.Default.NeverReUseLastPatient = false;
            Settings1.Default.Save();
        }

        private void neverReUsePatient_CheckedChanged(object sender, EventArgs e)
        {
            Settings1.Default.NeverReUseLastPatient = true;
            Settings1.Default.ReUseLastPatientPrompt = false;
            Settings1.Default.ReUseLastPatientWithoutPrompt = false;
            Settings1.Default.Save();
        }

        private void AlwaysPromptAddon_CheckedChanged(object sender, EventArgs e)
        {
            Settings1.Default.ReUseAddonPrompt = true;
            Settings1.Default.ReUseAddonWithoutPrompt = false;
            Settings1.Default.NeverReUseAddon = false;
            Settings1.Default.Save();
        }

        private void disablePromptAndReuseAddon_CheckedChanged(object sender, EventArgs e)
        {
            Settings1.Default.ReUseAddonWithoutPrompt = true;
            Settings1.Default.ReUseAddonPrompt = false;
            Settings1.Default.NeverReUseAddon = false;
            Settings1.Default.Save();
        }

        private void disablePromptAndNeverReuseAddon_CheckedChanged(object sender, EventArgs e)
        {
            Settings1.Default.NeverReUseAddon = true;
            Settings1.Default.ReUseAddonWithoutPrompt = false;
            Settings1.Default.ReUseAddonPrompt = false;
            Settings1.Default.Save();
        }

        private void ClearAddOnInformationFromForm()
        {
            addonProcedureInput.Text = null;
            addonChargeInput.Text = null;
            addonAdjustmentInput.Text = null;
            addonPaidInput.Text = null;
            addOnModifierOneInput.Text = null;
            addOnModifierTwoInput.Text = null;
            addOnModifierThreeInput.Text = null;
            addOnModifierFourInput.Text = null;
        }

        private void ClearPatientFromForm()
        {
            patientFirstNameInput.Text = null;
            patientLastNameInput.Text = null;
            patientCopayInput.Text = null;
            billIdInput.Text = null;
        }

        private void ClearChargesFromForm()
        {
            primaryChargeInput.Text = null;
            primaryAdjustmentInput.Text = null;
            primaryPaidInput.Text = null;
            primaryModifierOneInput.Text = null;
            primaryModifierTwoInput.Text = null;
            primaryModifierThreeInput.Text = null;
            primaryModifierFourInput.Text = null;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void clearListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult Positive = MessageBox.Show("Are you Sure you want to clear your list?", "Confirm", MessageBoxButtons.YesNo);
            if (Positive == DialogResult.Yes)
            {
                ClearPatientList();
                LoadInitialPatient();
                PatientsInList.Text = Patient.PatientList.Count.ToString();
                AddonCodeCountPerPatient.Text = "0";
            }
        }

        private void paymentTypeInput_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void IncludeSubscriberCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (IncludeSubscriberCheckbox.Checked == true)
            {
                Settings1.Default.IncludeSubscriber = true;
            }
            else if (IncludeSubscriberCheckbox.Checked == false)
            {
                Settings1.Default.IncludeSubscriber = false;
            }

            Settings1.Default.Save();
        }

        private void addonChargeListBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void readMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPatchHistory();
            //ShowCurrentUpdate();
        }

        private void ShowPatchHistory()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var patchNotes = "ERAFileCreator.PatchHistory.txt";

            using (Stream stream = assembly.GetManifestResourceStream(patchNotes))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                MessageBox.Show(result);
            }
        }

        private void ShowCurrentUpdate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var readMe = "ERAFileCreator.ReadMe.txt";

            using (Stream stream = assembly.GetManifestResourceStream(readMe))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                MessageBox.Show(result);
            }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            patientInformationPanel.Hide();
            PaymentDetailsPanel.Hide();
            options.Show();
        }

    }
}