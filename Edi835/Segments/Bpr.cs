using System.Text;
using Common.Common;
using Edi835.Segments;
using PatientManagement.Model;

namespace EDI835.Segments
{
    public class Bpr : SegmentBase
    {
        public Bpr(InsuranceCompany insurance)
        {
            Insurance = insurance;
            SegmentIdentifier = "BPR"; //bpr 1
            TransactionHandlingCode = "I"; // bpr 2
            insurance.CheckAmount = MonetaryAmount; //bpr3 
            CreditOrDebtFlag = "C"; //bpr4
            insurance.PaymentType = PaymentMethodCode; //bpr5
            PaymentMethodCode = "CCP"; //bpr6
            SenderDfiIdNumberQualifier = "01"; //bpr7
            SenderDfiIdNumber = "043000096"; //bpr8
            SenderAccountNumberQualifier = "DA"; //bpr9
            SenderAccountNumber = "0"; //bpr10
            OriginatingCompanyId = "5135511997";//bpr11
            SenderDfiIdNumberQualifier = string.Empty; //bpr12
            SenderDfiIdNumber = string.Empty; //bpr13
            SenderAccountNumberQualifier = string.Empty; //bpr14
            SenderAccountNumber = string.Empty;//bpr15
            Date = insurance.CheckDate.ConvertedDate();

        }

        private InsuranceCompany Insurance { get; set; }
        private string TransactionHandlingCode { get; set; }
        private decimal MonetaryAmount { get; set; }
        private string CreditOrDebtFlag { get; set; }
        private string PaymentMethodCode { get; set; }
        private string PaymentFormatCode { get; set; }
        private string SenderDfiIdNumberQualifier { get; set; }
        private string SenderDfiIdNumber { get; set; }
        private string SenderAccountNumberQualifier { get; set; }
        private string SenderAccountNumber { get; set; }
        private string OriginatingCompanyId { get; set; }
        private string OriginatingCompanyCode { get; set; }
        private string ReceivingDfiIdNumberQualifier { get; set; }
        private string ReceivingDfiNumber { get; set; }
        private string ReceivingAccountNumberQualifier { get; set; }
        private string ReceivingAccountNumber { get; set; }
        private string Date { get; set; }

        public string BuildBpr()
        {
            var buildBpr = new StringBuilder();

            //Begin BPR     

            buildBpr.Append(SegmentIdentifier);
            buildBpr.Append(DataElementTerminator);
            buildBpr.Append(MonetaryAmount);
            buildBpr.Append(DataElementTerminator);
            buildBpr.Append(CreditOrDebtFlag);
            buildBpr.Append(DataElementTerminator);
            buildBpr.Append(PaymentMethodCode);
            buildBpr.Append(DataElementTerminator);
            buildBpr.Append(PaymentFormatCode);
            buildBpr.Append(DataElementTerminator);

            if(SenderDfiIdNumberQualifier !=null || SenderDfiIdNumber!=null)
            {
                buildBpr.Append(SenderDfiIdNumberQualifier);
                buildBpr.Append(DataElementTerminator);
                buildBpr.Append(SenderDfiIdNumber);
                buildBpr.Append(DataElementTerminator);
            }

            if(SenderAccountNumberQualifier !=null||SenderDfiIdNumber!=null)
            {
                buildBpr.Append(SenderAccountNumberQualifier);
                buildBpr.Append(DataElementTerminator);
                buildBpr.Append(SenderAccountNumber);
                buildBpr.Append(DataElementTerminator);
            }

            buildBpr.Append(OriginatingCompanyId);
            buildBpr.Append(DataElementTerminator);
            buildBpr.Append(OriginatingCompanyCode);

            if(!string.IsNullOrEmpty(ReceivingDfiIdNumberQualifier) ||!string.IsNullOrEmpty(ReceivingDfiNumber))
            {
                buildBpr.Append(ReceivingDfiIdNumberQualifier);
                buildBpr.Append(DataElementTerminator);
                buildBpr.Append(ReceivingDfiNumber);
                buildBpr.Append(DataElementTerminator);
            }

            if(!string.IsNullOrEmpty(ReceivingAccountNumberQualifier)||!string.IsNullOrEmpty(ReceivingAccountNumber))
            {
                buildBpr.Append(ReceivingAccountNumberQualifier);
                buildBpr.Append(DataElementTerminator);
                buildBpr.Append(ReceivingAccountNumber);
                buildBpr.Append(DataElementTerminator);
            }
            buildBpr.Append(Date);
            buildBpr.Append(SegmentTerminator);

            //buildBpr.Append("BPR" + "*");
            //buildBpr.Append("I" + "*"); // BPR01 Transaction Handling Code Remittance Information Only
            //buildBpr.Append(insurance.CheckAmount + "*"); // BPR02 Monetary Amount
            //buildBpr.Append("C" + "*"); //BPR03 Credit/Debit Flag
            //buildBpr.Append(insurance.PaymentType + "*"); //BPR04 Payment Method Code
            //buildBpr.Append("CCP" + "*"); //BPR05 Payment Format Code, Cash Concentration plus addenda
            //buildBpr.Append("01" + "*"); //BPR06 DFI ID Number Qualifier
            //buildBpr.Append("043000096" + "*"); //BPR07 ABA Sender Transit Routing Number
            //buildBpr.Append("DA" + "*"); //BPR08 Account Number Qualifier
            //buildBpr.Append("0" + "*"); //BPR09 Sender Bank Account NUmber
            //buildBpr.Append("5135511997" + "*"); //BPR10 Originating Company Identifier
            //buildBpr.Append("01" + "*"); //BPR12 ABA Receiver Transit Routing Number
            //buildBpr.Append("*");
            //buildBpr.Append("GAPFILL" + "*");
            //buildBpr.Append("DA" + "*"); //BPR013 Account Number Qualifier
            //buildBpr.Append("0" + "*"); //BPR015 Receiver or Provider Account Number

            //var dateConversion = new DateConversion();
            //buildBpr.Append(dateConversion.ConvertedDate(insurance.CheckDate)); //BPR016 Check Issue/Effective date
            //buildBpr.Append("~");

            return buildBpr.ToString();

        }
    }
}
