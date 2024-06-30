using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class PaymentVoucherItem : BaseEntity
    {
      
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal PayAmount { get; set; }
        public decimal Total { get; set; }
        public decimal ExtraAmount { get; set; }
        public bool IsAging { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        public PartiallyInvoices PartiallyInvoices { get; set; }

        public virtual Guid? PurchaseInvoiceId { get; set; }
        public virtual Guid? SaleInvoiceId { get; set; }
        public string DocumentType { get; set; }
        public string ChequeNumber { get; set; }
        public Guid? PaymentVoucherId { get; set; }
        public virtual PaymentVoucher PaymentVouchers { get; set; }
    }
}
