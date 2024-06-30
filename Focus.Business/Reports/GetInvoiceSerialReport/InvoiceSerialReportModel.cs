using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Reports.GetInvoiceSerialReport
{
    public  class InvoiceSerialReportModel
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNameArabic { get; set; }
        public string InvoiceNo { get; set; }
        public decimal Total { get; set; }
    }
}
