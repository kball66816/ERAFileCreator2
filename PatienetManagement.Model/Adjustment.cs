using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PatientManagement.Model
{
    [Serializable]
    public class Adjustment:INotifyPropertyChanged
    {
        public Adjustment()
        {
            adjustmentType = "CO";
            adjustmentReasonCode = "45";
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        private string adjustmentType;

        public string AdjustmentType
        {
            get { return adjustmentType; }
            set
            {
                if (value != AdjustmentType)
                {
                    adjustmentType = value;
                    RaisePropertyChanged("AdjustmentType");

                }
            }
        }


        private string adjustmentReasonCode;

        public string AdjustmentReasonCode
        {
            get { return adjustmentReasonCode; }
            set
            {
                if (value != adjustmentReasonCode)
                {
                    adjustmentReasonCode = value;
                    RaisePropertyChanged("AdjustmentReasonCode");
                }
  
            }
        }

        public readonly Dictionary<string, string> AdjustmentTypes = new Dictionary<string, string>
        {
            {"Contractual Obligation", "CO"},
            {"Patient Responsibility", "PR"},
            {"Payer Initiated","PI" },
            {"Other Adjustment", "OA"},

        };

        public readonly Dictionary<string, string> AdjustmentReasonCodes = new Dictionary<string, string>
        {
            {"Deductible", "1" },
            {"Coinsurance","2" },
            {"Copay","3" },
            {"This Procedure code is inconsistent with the modifier used or a required modifier is missing. Usage:Refer to the 835 Healthcare Policy Identification Segment(loop 2110 Service Payment Information REF), if present.","4" },
            {"The procedure code/bill type is inconsistent with the place of service. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.", "5" },
            {"The procedure/revenue code is inconsistent with the patient's age. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.", "6" },
            {"The procedure/revenue code is inconsistent with the patient's gender. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","7" },
            {"The procedure code is inconsistent with the provider type/specialty (taxonomy). Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","8"},
            {"The diagnosis is inconsistent with the patient's age. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","9" },
            {"The diagnosis is inconsistent with the patient's gender. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","10" },
            {"The diagnosis is inconsistent with the procedure. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","11" },
            {"The diagnosis is inconsistent with the provider type. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","12" },
            {"The date of death precedes the date of service.","13" },
            {"The date of birth follows the date of service.","14" },
            {"The authorization number is missing, invalid, or does not apply to the billed services or provider.","15" },
            {"Claim/service lacks information or has submission/billing error(s) which is needed for adjudication. Do not use this code for claims attachment(s)/other documentation. At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT.) Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","16" },
            {"Exact duplicate claim/service (Use only with Group Code OA except where state workers' compensation regulations requires CO)", "18" },
            {"This is a work-related injury/illness and thus the liability of the Worker's Compensation Carrier.", "19" },
            {"This injury/illness is covered by the liability carrier.","20" },
            {"This injury/illness is the liability of the no-fault carrier.", "21"},
            {"This care may be covered by another payer per coordination of benefits.","22" },
            {"The impact of prior payer(s) adjudication including payments and/or adjustments. (Use only with Group Code OA)","23" },
            {"Charges are covered under a capitation agreement/managed care plan." ,"24"},
            {"Expenses incurred prior to coverage.","26" },
            {"Expenses incurred after coverage terminated.","27" },
            {"The time limit for filing has expired.","29" },
            {"Patient cannot be identified as our insured.","31" },
            {"Our records indicate that this dependent is not an eligible dependent as defined.","32" },
            {"Insured has no dependent coverage.","33" },
            {"Insured has no coverage for newborns.","34" },
            {"Lifetime benefit maximum has been reached.","35" },
            {"Services denied at the time authorization/pre-certification was requested.","39" },
            {"Charges do not meet qualifications for emergent/urgent care. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","40" },
            {"Prompt-pay discount.","44" },
            {"Charges exceeds fee schedule/maximum allowable or contracted/legislated fee arrangement. Usage: This adjustment amount cannot equal the total service or claim charge amount; and must not duplicate provider adjustment amounts (payments and contractual reductions) that have resulted from prior payer(s) adjudication. (Use only with Group Codes PR or CO depending upon liability)","45"},
            {"These are non-covered services because this is not deemed a 'medical necessity' by the payer. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","50" },
            {"These are non-covered services because this is a pre-existing condition. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","51" },
            {"Services by an immediate relative or a member of the same household are not covered.","53" },
            {"Multiple physicians/assistants are not covered in this case. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","54" },
            {"Procedure/treatment/drug is deemed experimental/investigational by the payer. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","55" },
            {"Procedure/treatment has not been deemed 'proven to be effective' by the payer. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","56" },
            {"Treatment was deemed by the payer to have been rendered in an inappropriate or invalid place of service. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","58" },
            {"Processed based on multiple or concurrent procedure rules. (For example multiple surgery or diagnostic imaging, concurrent anesthesia.) Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present","59" },
            {"Charges for outpatient services are not covered when performed within a period of time prior to or after inpatient services.","60" },
            {"Adjusted for failure to obtain second surgical opinion","61" },
            {"Blood Deductible.","66" },
            {"Day outlier amount.","69" },
            {"Cost outlier - Adjustment to compensate for additional costs.","70" },
            {"Indirect Medical Education Adjustment.","74"},
            {"Direct Medical Education Adjustment.","75" },
            {"Disproportionate Share Adjustment.","76" },
            {"Non-Covered days/Room charge adjustment.","78" },
            {"Patient Interest Adjustment (Use Only Group code PR)","85" },
            {"Professional fees removed from charges.","89"},
            {"Ingredient cost adjustment. Usage: To be used for pharmaceuticals only.","90" },
            {"Dispensing fee adjustment.","91" },
            {"Processed in Excess of charges.","94" },
            {"Plan procedures not followed.","95" },
            {"Non-covered charge(s). At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT.) Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","96" },
            {"The benefit for this service is included in the payment/allowance for another service/procedure that has already been adjudicated. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","97" },
            {"Payment made to patient/insured/responsible party/employer.","100" },
            {"Predetermination: anticipated payment upon completion of services or claim adjudication.","101" },
            {"Major Medical Adjustment.","102" },
            {"Provider promotional discount (e.g., Senior citizen discount).","103" },
            {"Managed care withholding.","104" },
            {"Tax withholding." ,"105"},
            {"Patient payment option/election not in effect.","106" },
            {"The related or qualifying claim/service was not identified on this claim. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","107" },
            {"Rent/purchase guidelines were not met. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","108" },
            {"Claim/service not covered by this payer/contractor. You must send the claim/service to the correct payer/contractor.","109" },
            {"Billing date predates service date.","110" },
            {"Not covered unless the provider accepts assignment.","111" },
            {"Service not furnished directly to the patient and/or not documented.","112" },
            {"Procedure/product not approved by the Food and Drug Administration.","114" },
            {"Procedure postponed, canceled, or delayed.","115" },
            {"The advance indemnification notice signed by the patient did not comply with requirements.","116" },
            {"Transportation is only covered to the closest facility that can provide the necessary care.","117" },
            {"ESRD network support adjustment.","118" },
            {"Benefit maximum for this time period or occurrence has been reached.","119" },
            {"Indemnification adjustment - compensation for outstanding member responsibility.","121" },
            {"Psychiatric reduction.","122" },
            {"Newborn's services are covered in the mother's Allowance.","128" },
            {"Prior processing information appears incorrect. At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT.)","129" },
            {"Claim submission fee.","130"},
            {"Claim specific negotiated discount.","131" },
            {"Prearranged demonstration project adjustment.","132" },
            {"The disposition of this service line is pending further review. (Use only with Group Code OA). Usage: Use of this code requires a reversal and correction when the service line is finalized (use only in Loop 2110 CAS segment of the 835 or Loop 2430 of the 837).","133" },
            {"Technical fees removed from charges.","134" },
            {"Interim bills cannot be processed.","135" },
            {"Failure to follow prior payer's coverage rules. (Use only with Group Code OA)","136" },
            {"Regulatory Surcharges, Assessments, Allowances or Health Related Taxes.","137" },
            {"Appeal procedures not followed or time limits not met.","138" },
            {"Contracted funding agreement - Subscriber is employed by the provider of services.","139" },
            {"Patient/Insured health identification number and name do not match.","140"},
            {"Monthly Medicaid patient liability amount.","142" },
            {"Portion of payment deferred.","143" },
            {"Incentive adjustment, e.g. preferred product/service.","144" },
            {"Diagnosis was invalid for the date(s) of service reported.","146"},
            {"Provider contracted/negotiated rate expired or not on file.","147" },
            {"Information from another provider was not provided or was insufficient/incomplete. At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT.)","148" },
            {"Lifetime benefit maximum has been reached for this service/benefit category.","149" },
            {"Payer deems the information submitted does not support this level of service.","150" },
            {"Payment adjusted because the payer deems the information submitted does not support this many/frequency of services.","151" },
            {"Payer deems the information submitted does not support this length of service. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","152" },
            {"Payer deems the information submitted does not support this dosage.","153" },
            {"Payer deems the information submitted does not support this day's supply.","154" },
            {"Patient refused the service/procedure.","155" },
            {"Service/procedure was provided as a result of an act of war.","157" },
            {"Service/procedure was provided outside of the United States.","158" },
            {"Service/procedure was provided as a result of terrorism.","159" },
            {"Injury/illness was the result of an activity that is a benefit exclusion.","160" },
            {"Provider performance bonus","161" },
            {"Attachment/other documentation referenced on the claim was not received.","163" },
            {"Attachment/other documentation referenced on the claim was not received in a timely fashion.","164" },
            {"Referral absent or exceeded.","165" },
            {"These services were submitted after this payers responsibility for processing claims under this plan ended.","166" },
            {"This (these) diagnosis(es) is (are) not covered. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","167" },
            {"Service(s) have been considered under the patient's medical plan. Benefits are not available under this dental plan.","168" },
            {"Alternate benefit has been provided." ,"169"},
            {"Payment is denied when performed/billed by this type of provider. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","170" },
            {"Payment is denied when performed/billed by this type of provider in this type of facility. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","171"},
            {"Payment is adjusted when performed/billed by a provider of this specialty. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","172" },
            {"Service/equipment was not prescribed by a physician.","173" },
            {"Service was not prescribed prior to delivery.","174" },
            {"Prescription is incomplete.","175" },
            {"Prescription is not current.","176" },
            {"Patient has not met the required eligibility requirements.","177" },
            {"Patient has not met the required spend down requirements.","178" },
            {"Patient has not met the required waiting requirements. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","179" },
            {"Patient has not met the required residency requirements.","180" },
            {"Procedure code was invalid on the date of service.","181" },
            {"Procedure modifier was invalid on the date of service.","182" },
            {"The referring provider is not eligible to refer the service billed. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","183" },
            {"The prescribing/ordering provider is not eligible to prescribe/order the service billed. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","184" },
            {"The rendering provider is not eligible to perform the service billed. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","185" },
            {"Level of care change adjustment.","186" },
            {"Consumer Spending Account payments (includes but is not limited to Flexible Spending Account, Health Savings Account, Health Reimbursement Account, etc.)","187" },
            {"This product/procedure is only covered when used according to FDA recommendations.","188" },
            {"'Not otherwise classified' or 'unlisted' procedure code (CPT/HCPCS) was billed when there is a specific procedure code for this procedure/service","189" },
            {"Payment is included in the allowance for a Skilled Nursing Facility (SNF) qualified stay.","190" },
            {"Non standard adjustment code from paper remittance. Usage: This code is to be used by providers/payers providing Coordination of Benefits information to another payer in the 837 transaction only. This code is only used when the non-standard code cannot be reasonably mapped to an existing Claims Adjustment Reason Code, specifically Deductible, Coinsurance and Co-payment.","192" },
            {"Original payment decision is being maintained. Upon review, it was determined that this claim was processed properly.","193" },
            {"Anesthesia performed by the operating physician, the assistant surgeon or the attending physician.","194" },
            {"Refund issued to an erroneous priority payer for this claim/service.","195" },
            {"Precertification/authorization/notification absent.","197" },
            {"Precertification/authorization exceeded.","198" },
            {"Revenue code and Procedure code do not match.","199" },
            {"Expenses incurred during lapse in coverage","200" },
            {"Patient is responsible for amount of this claim/service through 'set aside arrangement' or other agreement. (Use only with Group Code PR) At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT.)","201" },
            {"Non-covered personal comfort or convenience services.","202" },
            {"Discontinued or reduced service.","203" },
            {"This service/equipment/drug is not covered under the patient’s current benefit plan","204" },
            {"Pharmacy discount card processing fee","205" },
            {"National Provider Identifier - missing.","206" },
            {"National Provider identifier - Invalid format","207" },
            {"National Provider Identifier - Not matched.","208" },
            {"Per regulatory or other agreement. The provider cannot collect this amount from the patient. However, this amount may be billed to subsequent payer. Refund to patient if collected. (Use only with Group code OA)","209" },
            {"Payment adjusted because pre-certification/authorization not received in a timely fashion","210" },
            {"National Drug Codes (NDC) not eligible for rebate, are not covered.","211" },
            {"Administrative surcharges are not covered","212" },
            {"Non-compliance with the physician self referral prohibition legislation or payer policy.","213" },
            {"Based on subrogation of a third party settlement","215" },
            {"Based on the findings of a review organization","216" },
            {"Based on extent of injury. Usage: If adjustment is at the Claim Level, the payer must send and the provider should refer to the 835 Insurance Policy Number Segment (Loop 2100 Other Claim Related Information REF qualifier 'IG') for the jurisdictional regulation. If adjustment is at the Line Level, the payer must send and the provider should refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment information REF).","219" },
            {"Exceeds the contracted maximum number of hours/days/units by this provider for this period. This is not patient specific. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","222" },
            {"Adjustment code for mandated federal, state or local law/regulation that is not already covered by another code and is mandated before a new code can be created.","223" },
            {"Patient identification compromised by identity theft. Identity verification required for processing this and future claims.","224" },
            {"Penalty or Interest Payment by Payer (Only used for plan to plan encounter reporting within the 837)","225" },
            {"Information requested from the Billing/Rendering Provider was not provided or not provided timely or was insufficient/incomplete. At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT.)","226" },
            {"Information requested from the patient/insured/responsible party was not provided or was insufficient/incomplete. At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT.)","227" },
            {"Denied for failure of this provider, another provider or the subscriber to supply requested information to a previous payer for their adjudication","228" },
            {"Partial charge amount not considered by Medicare due to the initial claim Type of Bill being 12X. Usage: This code can only be used in the 837 transaction to convey Coordination of Benefits information when the secondary payer's cost avoidance policy allows providers to bypass claim submission to a prior payer. (Use only with Group Code PR)","229" },
            {"Mutually exclusive procedures cannot be done in the same day/setting. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","231" },
            {"Institutional Transfer Amount. Usage: Applies to institutional claims only and explains the DRG amount difference when the patient care crosses multiple institutions.","232" },
            {"Services/charges related to the treatment of a hospital-acquired condition or preventable medical error.","233"},
            {"This procedure is not paid separately. At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT.)","234" },
            {"Sales Tax","235" },
            {"This procedure or procedure/modifier combination is not compatible with another procedure or procedure/modifier combination provided on the same day according to the National Correct Coding Initiative or workers compensation state regulations/ fee schedule requirements.","236" },
            {"Legislated/Regulatory Penalty. At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT.)","237" },
            {"Claim spans eligible and ineligible periods of coverage, this is the reduction for the ineligible period. (Use only with Group Code PR)","238" },
            {"Claim spans eligible and ineligible periods of coverage. Rebill separate claims.","239" },
            {"The diagnosis is inconsistent with the patient's birth weight. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","240" },
            {"Low Income Subsidy (LIS) Co-payment Amount","241" },
            {"Services not provided by network/primary care providers.","242" },
            {"Services not authorized by network/primary care providers.","243" },
            {"Provider performance program withhold.","245" },
            {"This non-payable code is for required reporting only.","246" },
            {"Deductible for Professional service rendered in an Institutional setting and billed on an Institutional claim.","247" },
            {"Coinsurance for Professional service rendered in an Institutional setting and billed on an Institutional claim.","248" },
            {"This claim has been identified as a readmission. (Use only with Group Code CO)","249" },
            {"The attachment/other documentation that was received was the incorrect attachment/document. The expected attachment/document is still missing. At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT).","250" },
            {"The attachment/other documentation that was received was incomplete or deficient. The necessary information is still needed to process the claim. At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT).","251" },
            {"An attachment/other documentation is required to adjudicate this claim/service. At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT).","252" },
            {"Sequestration - reduction in federal payment","253" },
            {"Claim received by the dental plan, but benefits not available under this plan. Submit these services to the patient's medical plan for further consideration.","254" },
            {"Service not payable per managed care contract.","256" },
            {"The disposition of the claim/service is undetermined during the premium payment grace period, per Health Insurance Exchange requirements. This claim/service will be reversed and corrected when the grace period ends (due to premium payment or lack of premium payment). (Use only with Group Code OA)","257" },
            {"Claim/service not covered when patient is in custody/incarcerated. Applicable federal, state or local authority may cover the claim/service.","258" },
            {"Additional payment for Dental/Vision service utilization.","259" },
            {"Processed under Medicaid ACA Enhanced Fee Schedule","260" },
            {"The procedure or service is inconsistent with the patient's history.","261" },
            {"Adjustment for delivery cost. Usage: To be used for pharmaceuticals only.","262" },
            {"Adjustment for shipping cost. Usage: To be used for pharmaceuticals only.","263" },
            {"Adjustment for postage cost. Usage: To be used for pharmaceuticals only.","264" },
            {"Adjustment for administrative cost. Usage: To be used for pharmaceuticals only.","265" },
            {"Adjustment for compound preparation cost. Usage: To be used for pharmaceuticals only.","266" },
            {"Claim/service spans multiple months. At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT.)","267" },
            {"The Claim spans two calendar years. Please resubmit one claim per calendar year.","268" },
            {"Anesthesia not covered for this service/procedure. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","269" },
            {"Claim received by the medical plan, but benefits not available under this plan. Submit these services to the patient’s dental plan for further consideration.","270" },
            {"Prior contractual reductions related to a current periodic payment as part of a contractual payment schedule when deferred amounts have been previously reported. (Use only with group code OA)","271"},
            {"Coverage/program guidelines were not met.","272" },
            {"Coverage/program guidelines were exceeded.","273" },
            {"Fee/Service not payable per patient Care Coordination arrangement.","274"},
            {"Prior payer's (or payers') patient responsibility (deductible, coinsurance, co-payment) not covered. (Use only with Group Code PR)","275" },
            {"Services denied by the prior payer(s) are not covered by this payer.","276" },
            {"The disposition of the claim/service is undetermined during the premium payment grace period, per Health Insurance SHOP Exchange requirements. This claim/service will be reversed and corrected when the grace period ends (due to premium payment or lack of premium payment). (Use only with Group Code OA)","277" },
            {"Performance program proficiency requirements not met. (Use only with Group Codes CO or PI) Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","278" },
            {"Services not provided by Preferred network providers. Usage: Use this code when there are member network limitations. For example, using contracted providers not in the member's \"narrow\" network.","279" },
            {"Claim received by the medical plan, but benefits not available under this plan. Submit these services to the patient's Pharmacy plan for further consideration.","280" },
            {"Deductible waived per contractual agreement. Use only with Group Code CO.","281" },
            {"The procedure/revenue code is inconsistent with the type of bill. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","282" },
            {"Patient refund amount.","A0" },
            {"Claim/Service denied. At least one Remark Code must be provided (may be comprised of either the NCPDP Reject Reason Code, or Remittance Advice Remark Code that is not an ALERT.)","A1" },
            {"Medicare Claim PPS Capital Cost Outlier Amount.","A5" },
            {"Prior hospitalization or 30 day transfer requirement not met.","A6" },
            {"Ungroupable DRG.","A8" },
            {"Non-covered visits.","B1" },
            {"Late filing penalty.","B4" },
            {"This provider was not certified/eligible to be paid for this procedure/service on this date of service. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","B7" },
            {"Alternative services were available, and should have been utilized. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","B8" },
            {"Patient is enrolled in a Hospice.","B9" },
            {"Allowed amount has been reduced because a component of the basic procedure/test was paid. The beneficiary is not liable for more than the charge limit for the basic procedure/test.","B10" },
            {"The claim/service has been transferred to the proper payer/processor for processing. Claim/service not covered by this payer/processor.","B11" },
            {"Services not documented in patients' medical records.","B12" },
            {"Previously paid. Payment for this claim/service may have been provided in a previous payment.","B13" },
            {"Only one visit or consultation per physician per day is covered.","B14"},
            {"This service/procedure requires that a qualifying service/procedure be received and covered. The qualifying other service/procedure has not been received/adjudicated. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present.","B15" },
            {"'New Patient' qualifications were not met.","B16" },
            {"Procedure/service was partially or fully furnished by another provider.","B20" },
            {"This payment is adjusted based on the diagnosis.","B22" },
            {"Procedure billed is not authorized per your Clinical Laboratory Improvement Amendment (CLIA) proficiency test.","B23"},
            {"State-mandated Requirement for Property and Casualty, see Claim Payment Remarks Code for specific explanation. To be used for Property and Casualty only.","P1" },
            {"Not a work related injury/illness and thus not the liability of the workers' compensation carrier Usage: If adjustment is at the Claim Level, the payer must send and the provider should refer to the 835 Insurance Policy Number Segment (Loop 2100 Other Claim Related Information REF qualifier 'IG') for the jurisdictional regulation. If adjustment is at the Line Level, the payer must send and the provider should refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment information REF). To be used for Workers' Compensation only.","P2" },
            {"Workers' Compensation case settled. Patient is responsible for amount of this claim/service through WC 'Medicare set aside arrangement' or other agreement. To be used for Workers' Compensation only. (Use only with Group Code PR)","P3" },
            {"Workers' Compensation claim adjudicated as non-compensable. This Payer not liable for claim or service/treatment. Usage: If adjustment is at the Claim Level, the payer must send and the provider should refer to the 835 Insurance Policy Number Segment (Loop 2100 Other Claim Related Information REF qualifier 'IG') for the jurisdictional regulation. If adjustment is at the Line Level, the payer must send and the provider should refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment information REF). To be used for Workers' Compensation only","P4" },
            {"Based on payer reasonable and customary fees. No maximum allowable defined by legislated fee arrangement. To be used for Property and Casualty only.","P5" },
            {"Based on entitlement to benefits. Usage: If adjustment is at the Claim Level, the payer must send and the provider should refer to the 835 Insurance Policy Number Segment (Loop 2100 Other Claim Related Information REF qualifier 'IG') for the jurisdictional regulation. If adjustment is at the Line Level, the payer must send and the provider should refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment information REF). To be used for Property and Casualty only.","P6" },
            {"The applicable fee schedule/fee database does not contain the billed code. Please resubmit a bill with the appropriate fee schedule/fee database code(s) that best describe the service(s) provided and supporting documentation if required. To be used for Property and Casualty only.","P7" },
            {"Claim is under investigation. Usage: If adjustment is at the Claim Level, the payer must send and the provider should refer to the 835 Insurance Policy Number Segment (Loop 2100 Other Claim Related Information REF qualifier 'IG') for the jurisdictional regulation. If adjustment is at the Line Level, the payer must send and the provider should refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment information REF). To be used for Property and Casualty only.","P8" },
            {"No available or correlating CPT/HCPCS code to describe this service. To be used for Property and Casualty only.","P9" },
            {"Payment reduced to zero due to litigation. Additional information will be sent following the conclusion of litigation. To be used for Property and Casualty only.","P10" },
            {"The disposition of the related Property & Casualty claim (injury or illness) is pending due to litigation. To be used for Property and Casualty only. (Use only with Group Code OA)","P11" },
            {"Workers' compensation jurisdictional fee schedule adjustment. Usage: If adjustment is at the Claim Level, the payer must send and the provider should refer to the 835 Class of Contract Code Identification Segment (Loop 2100 Other Claim Related Information REF). If adjustment is at the Line Level, the payer must send and the provider should refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment information REF) if the regulations apply. To be used for Workers' Compensation only.","P12" },
            {"Payment reduced or denied based on workers' compensation jurisdictional regulations or payment policies, use only if no other code is applicable. Usage: If adjustment is at the Claim Level, the payer must send and the provider should refer to the 835 Insurance Policy Number Segment (Loop 2100 Other Claim Related Information REF qualifier 'IG') if the jurisdictional regulation applies. If adjustment is at the Line Level, the payer must send and the provider should refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment information REF) if the regulations apply. To be used for Workers' Compensation only.","P13" },
            {"The Benefit for this Service is included in the payment/allowance for another service/procedure that has been performed on the same day. Usage: Refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment Information REF), if present. To be used for Property and Casualty only.","P14" },
            {"Workers' Compensation Medical Treatment Guideline Adjustment. To be used for Workers' Compensation only.","P15" },
            {"Medical provider not authorized/certified to provide treatment to injured workers in this jurisdiction. To be used for Workers' Compensation only. (Use with Group Code CO or OA)","P16" },
            {"Referral not authorized by attending physician per regulatory requirement. To be used for Property and Casualty only.","P17" },
            {"Procedure is not listed in the jurisdiction fee schedule. An allowance has been made for a comparable service. To be used for Property and Casualty only.","P18" },
            {"Procedure has a relative value of zero in the jurisdiction fee schedule, therefore no payment is due. To be used for Property and Casualty only.","P19" },
            {"Service not paid under jurisdiction allowed outpatient facility fee schedule. To be used for Property and Casualty only.","P20" },
            {"Payment denied based on Medical Payments Coverage (MPC) or Personal Injury Protection (PIP) Benefits jurisdictional regulations or payment policies, use only if no other code is applicable. Usage: If adjustment is at the Claim Level, the payer must send and the provider should refer to the 835 Insurance Policy Number Segment (Loop 2100 Other Claim Related Information REF qualifier 'IG') if the jurisdictional regulation applies. If adjustment is at the Line Level, the payer must send and the provider should refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment information REF) if the regulations apply. To be used for Property and Casualty Auto only.","P21" },
            {"Payment adjusted based on Medical Payments Coverage (MPC) or Personal Injury Protection (PIP) Benefits jurisdictional regulations or payment policies, use only if no other code is applicable. Usage: If adjustment is at the Claim Level, the payer must send and the provider should refer to the 835 Insurance Policy Number Segment (Loop 2100 Other Claim Related Information REF qualifier 'IG') if the jurisdictional regulation applies. If adjustment is at the Line Level, the payer must send and the provider should refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment information REF) if the regulations apply. To be used for Property and Casualty Auto only.","P22" },
            {"Medical Payments Coverage (MPC) or Personal Injury Protection (PIP) Benefits jurisdictional fee schedule adjustment. Usage: If adjustment is at the Claim Level, the payer must send and the provider should refer to the 835 Class of Contract Code Identification Segment (Loop 2100 Other Claim Related Information REF). If adjustment is at the Line Level, the payer must send and the provider should refer to the 835 Healthcare Policy Identification Segment (loop 2110 Service Payment information REF) if the regulations apply. To be used for Property and Casualty Auto only.","P23" }
        };

        private decimal adjustmentAmount;

        public decimal AdjustmentAmount
        {
            get { return adjustmentAmount; }
            set
            {
                if (value != adjustmentAmount)
                {
                    adjustmentAmount = value;
                    RaisePropertyChanged("AdjustmentAmount");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
