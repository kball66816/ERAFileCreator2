using System.Text;
using Common.Common.Extensions;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class Bpr : SegmentBase
    {
        public Bpr(Payment payment)
        {
            SegmentIdentifier = "BPR"; //bpr 1
            TransactionHandlingCode = "I"; // bpr 2
            MonetaryAmount = payment.Amount;
            CreditOrDebtFlag = "C"; //bpr4
            PaymentMethodCode = payment.Type;
            PaymentFormatCode = "CCP"; //bpr6
            SenderDfiIdNumberQualifier = "01"; //bpr7
            SenderDfiIdNumber = "043000096"; //bpr8
            SenderAccountNumberQualifier = "DA"; //bpr9
            SenderAccountNumber = "0"; //bpr10
            OriginatingCompanyId = "5135511997"; //bpr11
            OriginatingCompanyCode = string.Empty;
            ReceivingDfiIdNumberQualifier = "01"; //bpr12
            ReceivingDfiNumber = "GAPFILL"; //bpr13
            ReceivingAccountNumberQualifier = "DA"; //bpr14
            ReceivingAccountNumber = "0"; //bpr15
            Date = payment.Date.DateToYearFirstShortString();
        }

        private string TransactionHandlingCode { get; }
        private decimal MonetaryAmount { get; }
        private string CreditOrDebtFlag { get; }
        private string PaymentMethodCode { get; }
        private string PaymentFormatCode { get; }
        private string SenderDfiIdNumberQualifier { get; }
        private string SenderDfiIdNumber { get; }
        private string SenderAccountNumberQualifier { get; }
        private string SenderAccountNumber { get; }
        private string OriginatingCompanyId { get; }
        private string OriginatingCompanyCode { get; }
        private string ReceivingDfiIdNumberQualifier { get; }
        private string ReceivingDfiNumber { get; }
        private string ReceivingAccountNumberQualifier { get; }
        private string ReceivingAccountNumber { get; }
        private string Date { get; }

        public string BuildBpr()
        {
            var buildBpr = new StringBuilder();

            buildBpr.Append(SegmentIdentifier);
            buildBpr.Append(DataElementTerminator)
                .Append(TransactionHandlingCode)
                .Append(DataElementTerminator);
            buildBpr.Append(MonetaryAmount);
            buildBpr.Append(DataElementTerminator);
            buildBpr.Append(CreditOrDebtFlag);
            buildBpr.Append(DataElementTerminator);
            buildBpr.Append(PaymentMethodCode);
            buildBpr.Append(DataElementTerminator);
            buildBpr.Append(PaymentFormatCode);
            buildBpr.Append(DataElementTerminator);

            if (SenderDfiIdNumberQualifier != null || SenderDfiIdNumber != null)
            {
                buildBpr.Append(SenderDfiIdNumberQualifier);
                buildBpr.Append(DataElementTerminator);
                buildBpr.Append(SenderDfiIdNumber);
                buildBpr.Append(DataElementTerminator);
            }


            if (SenderAccountNumberQualifier != null || SenderDfiIdNumber != null)
            {
                buildBpr.Append(SenderAccountNumberQualifier);
                buildBpr.Append(DataElementTerminator);
                buildBpr.Append(SenderAccountNumber);
                buildBpr.Append(DataElementTerminator);
            }

            buildBpr.Append(OriginatingCompanyId);
            buildBpr.Append(DataElementTerminator);
            buildBpr.Append(OriginatingCompanyCode);
            buildBpr.Append(DataElementTerminator);

            if (!string.IsNullOrEmpty(ReceivingDfiIdNumberQualifier) || !string.IsNullOrEmpty(ReceivingDfiNumber))
            {
                buildBpr.Append(ReceivingDfiIdNumberQualifier);
                buildBpr.Append(DataElementTerminator);
                buildBpr.Append(ReceivingDfiNumber);
                buildBpr.Append(DataElementTerminator);
            }

            if (!string.IsNullOrEmpty(ReceivingAccountNumberQualifier) || !string.IsNullOrEmpty(ReceivingAccountNumber))
            {
                buildBpr.Append(ReceivingAccountNumberQualifier);
                buildBpr.Append(DataElementTerminator);
                buildBpr.Append(ReceivingAccountNumber);
                buildBpr.Append(DataElementTerminator);
            }

            buildBpr.Append(Date);
            buildBpr.Append(SegmentTerminator);

            return buildBpr.ToString();
        }
    }
}