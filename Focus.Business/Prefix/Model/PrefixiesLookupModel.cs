﻿using System;

namespace Focus.Business.Prefix.Model
{
    public class PrefixiesLookupModel
    {
        public Guid Id { get; set; }
        public string SInvoice { get; set; }
        public string SReturn { get; set; }
        public string SOrder { get; set; }
        public string SQutation { get; set; }
        public string PInvoice { get; set; }
        public string PReturn { get; set; }
        public string POrder { get; set; }
        public string Employee { get; set; }
        public string SOrdeTracking { get; set; }
        public string GlobalInvoice { get; set; }
        public string ProformaSaleInvoice { get; set; }
        public string SaleInvoiceHold { get; set; }
        public string PurchaseInvoiceDraft { get; set; }
        public string ReceiptInvoice { get; set; }
        public string CreditNote { get;  set; }
        public string DebitNote { get;  set; }
        public string CustomerPayReceipt { get; set; }
        public string AdvanceSaleReceipt { get; set; }
        public string SupplierPayReceipt { get; set; }
        public string AdvancePurchaseReceipt { get; set; }
    }
}
