using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Enums
{
    public enum DocumentType
    {
        SaleInvoice = 1,
        SaleReturn = 2,
        ProformaInvoice = 3,
        GlobalInvoice = 4,
        ReceiptInvoice = 5,
        SaleOrder = 6,
        SaleQuotation = 7,
        SaleInvoiceHold = 8,
        CreditNote = 9,
        ServiceQuotation = 10,
        ServiceSaleOrder = 11,
        ServiceProformaInvoice = 12,
    }
}