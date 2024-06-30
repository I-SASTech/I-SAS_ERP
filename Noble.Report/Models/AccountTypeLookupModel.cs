using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class AccountTypeLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string AccountName { get; set; }
        public string AccountNameArabic { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CostCenterLookUpModel> CostCenterLookUpModel { get; set; }

    }
}