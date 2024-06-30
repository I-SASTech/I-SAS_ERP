using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.DayStarts.Queries.GetTotalInvoicesQuery
{
   public class TotalInvoicesLookUpModel
    {
        public string UserName { get; set; }
        public decimal TotalInvoices { get; set; }
        public decimal TotalCashInvoices { get; set; }
        public decimal TotalBankInvoices { get; set; }
        public decimal TotalCashAndBankInvoices { get; set; }
        public decimal TotalSaleReturn { get; set; }
        public decimal TotalSaleReturnValue { get; set; }
    }
}
