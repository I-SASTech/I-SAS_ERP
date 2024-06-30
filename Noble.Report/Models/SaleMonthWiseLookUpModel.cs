using Noble.Report.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class SaleMonthWiseLookUpModel
    {
        public string Month { get; set; }
        public DateTime Date { get; set; }
        public List<SaleInvoiceModel> Sales { get; set; }

    }
}