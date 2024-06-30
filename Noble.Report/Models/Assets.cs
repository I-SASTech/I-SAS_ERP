using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class Assets
    { 
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string CompareWith { get; set; }
        public string CostCenter { get; set; }
        public string CostCenterArabic { get; set; }
        public string AccountType { get; set; }
        public string AccountTypeArabic { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
    }
}