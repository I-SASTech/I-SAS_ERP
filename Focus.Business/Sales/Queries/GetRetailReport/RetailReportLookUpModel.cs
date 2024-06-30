using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Focus.Business.Sales.Queries.GetRetailReport
{
    public class RetailReportLookUpModel
    {
        public DateTime Date { get; set; }
        public decimal TotalCash { get; set; }
        public decimal TotalBank { get; set; }
        public decimal TotalSale { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalExpence { get; set; }
        public decimal TotalCredit { get; set; }
    }
}
