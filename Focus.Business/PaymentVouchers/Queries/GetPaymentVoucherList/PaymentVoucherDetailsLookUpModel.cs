using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Focus.Business.JournalVouchers.Queries.NetDrCr;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.PaymentVouchers.Queries.GetPaymentVoucherList
{
    public class PaymentVoucherDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public Guid ContactAccountId { get; set; }
        public string ContactAccountName { get; set; }
        public string AccountName { get; set; }
        public virtual Account Account { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public DateTime Date { get; set; }
        public string ChequeNumber { get; set; }
        public Guid PaymentVoucherId { get; set; }
        public virtual Guid? PurchaseInvoice { get; set; }
        public virtual Guid? SaleInvoice { get; set; }
        public virtual PaymentVoucher PaymentVouchers { get; set; }
    }
}
