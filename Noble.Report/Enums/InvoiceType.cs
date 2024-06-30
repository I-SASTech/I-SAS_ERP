using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Enums
{
    public enum InvoiceType
    {
        Hold,
        Paid,
        Credit,
        SaleReturn,
        ProformaInvoice,
        GlobalInvoice,
        ReceiptInvoice,
        PurchaseOrder,
    }
}