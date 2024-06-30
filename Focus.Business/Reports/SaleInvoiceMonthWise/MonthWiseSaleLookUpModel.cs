using Focus.Business.Reports.SaleInvoiceReport;
using System;
using System.Collections.Generic;


namespace Focus.Business.Reports.SaleInvoiceMonthWise
{
    public class MonthWiseSaleLookUpModel
    {
        public string Month { get; set; }
        public DateTime Date { get; set; }
        public Guid? BranchId { get; set; }
        public List<SaleInvoiceModel> Sales { get; set; }

    }
}
