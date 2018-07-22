using System.Text;
using Common.Common.Extensions;
using PatientManagement.DAL;

namespace Edi835._835Segments
{
    public class Bpr : SegmentBase
    {
        public Bpr(Payment payment)
        {
            this.SegmentIdentifier = "BPR"; //bpr 1
            this.TransactionHandlingCode = "I"; // bpr 2
            this.MonetaryAmount = payment.Amount;
            this.CreditOrDebtFlag = "C"; //bpr4
            this.PaymentMethodCode = payment.Type;
            this.PaymentFormatCode = "CCP"; //bpr6
            this.SenderDfiIdNumberQualifier = "01"; //bpr7
            this.SenderDfiIdNumber = "043000096"; //bpr8
            this.SenderAccountNumberQualifier = "DA"; //bpr9
            this.SenderAccountNumber = "0"; //bpr10
            this.OriginatingCompanyId = "5135511997"; //bpr11
            this.OriginatingCompanyCode = string.Empty;
            this.ReceivingDfiIdNumberQualifier = "01"; //bpr12
            this.ReceivingDfiNumber = "GAPFILL"; //bpr13
            this.ReceivingAccountNumberQualifier = "DA"; //bpr14
            this.ReceivingAccountNumber = "0"; //bpr15
            this.Date = payment.Date.DateToYearFirstShortString();
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

            buildBpr.Append(this.SegmentIdentifier);
            buildBpr.Append(this.DataElementTerminator)
                .Append(this.TransactionHandlingCode)
                .Append(this.DataElementTerminator);
            buildBpr.Append(this.MonetaryAmount);
            buildBpr.Append(this.DataElementTerminator);
            buildBpr.Append(this.CreditOrDebtFlag);
            buildBpr.Append(this.DataElementTerminator);
            buildBpr.Append(this.PaymentMethodCode);
            buildBpr.Append(this.DataElementTerminator);
            buildBpr.Append(this.PaymentFormatCode);
            buildBpr.Append(this.DataElementTerminator);

            if (this.SenderDfiIdNumberQualifier != null || this.SenderDfiIdNumber != null)
            {
                buildBpr.Append(this.SenderDfiIdNumberQualifier);
                buildBpr.Append(this.DataElementTerminator);
                buildBpr.Append(this.SenderDfiIdNumber);
                buildBpr.Append(this.DataElementTerminator);
            }


            if (this.SenderAccountNumberQualifier != null || this.SenderDfiIdNumber != null)
            {
                buildBpr.Append(this.SenderAccountNumberQualifier);
                buildBpr.Append(this.DataElementTerminator);
                buildBpr.Append(this.SenderAccountNumber);
                buildBpr.Append(this.DataElementTerminator);
            }

            buildBpr.Append(this.OriginatingCompanyId);
            buildBpr.Append(this.DataElementTerminator);
            buildBpr.Append(this.OriginatingCompanyCode);
            buildBpr.Append(this.DataElementTerminator);

            if (!string.IsNullOrEmpty(this.ReceivingDfiIdNumberQualifier) ||
                !string.IsNullOrEmpty(this.ReceivingDfiNumber))
            {
                buildBpr.Append(this.ReceivingDfiIdNumberQualifier);
                buildBpr.Append(this.DataElementTerminator);
                buildBpr.Append(this.ReceivingDfiNumber);
                buildBpr.Append(this.DataElementTerminator);
            }

            if (!string.IsNullOrEmpty(this.ReceivingAccountNumberQualifier) ||
                !string.IsNullOrEmpty(this.ReceivingAccountNumber))
            {
                buildBpr.Append(this.ReceivingAccountNumberQualifier);
                buildBpr.Append(this.DataElementTerminator);
                buildBpr.Append(this.ReceivingAccountNumber);
                buildBpr.Append(this.DataElementTerminator);
            }

            buildBpr.Append(this.Date);
            buildBpr.Append(this.SegmentTerminator);

            return buildBpr.ToString();
        }
    }
}