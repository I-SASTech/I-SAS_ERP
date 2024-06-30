using System;
using System.Collections.Generic;
using System.Linq;
using Focus.Domain.Enum;

namespace Focus.Business.SaleOrders.Queries.GetSaleOrderList
{
   
    public class SaleOrderLookUpModel
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public string RegistrationNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerArabicName { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsInvoice { get; set; }
        public bool IsClose { get; set; }
        public bool IsQuotation { get; set; }
        public bool IsProcessed { get; set; }
        public string Date { get; set; }
        public string Version { get; set; }
        public Guid? CustomerAccountId { get; set; }
        public Guid? CustomerAdvanceAccountId { get; set; }
        public string InvoiceNote { get; set; }
        public string EmployeeName { get; set; }
        public PartiallyInvoices PartiallyInvoices { get; set; }
        public string CreatedBy {get; set;}
        public string Reason {get; set;}
        public Guid? QuotationId { get; set; }
        public Guid? SaleOrderId { get; set; }
        public Guid? EditQuotationId { get; set; }
        public Guid? EditSaleOrderId { get; set; }
        public Guid? ProformaId { get; set; }
        public Guid? DeliveryChallanId { get; set; }

        public virtual List<SaleOrderItemLookupModel> SaleOrderItem { get; set; }
        public string BranchCode { get; set; }
        public string CustomerEmail { get; set; }
        public bool IsDefault { get; set; }
    }
}
