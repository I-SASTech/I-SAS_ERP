using Focus.Domain.Enum;
using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.ExcelAc;


namespace Focus.Business.PaymentVouchers.Commands
{
    public class PaymentVoucherItemLookUp
    {
        public Guid? Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal PayAmount { get; set; }
        public decimal Total { get; set; }
        public decimal ExtraAmount { get; set; }
        public bool IsAging { get; set; }
        public bool IsActive { get; set; }
        public DateTime Date { get; set; }
        public virtual Guid? PurchaseInvoiceId { get; set; }
        public virtual Guid? SaleInvoiceId { get; set; }
        public string ChequeNumber { get; set; }
        public Guid? PaymentVoucherId { get; set; }
        public PartiallyInvoices PartiallyInvoices { get; set; }

    }
}
