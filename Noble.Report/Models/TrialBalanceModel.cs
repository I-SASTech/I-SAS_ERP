using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class TrialBalanceModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TransactionType { get; set; }
        public string AccountName { get; set; }
        public string AccountNameArabic { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool VatDeductible { get; set; }
        public bool IsActive { get; set; }
        public bool check { get; set; }
        public decimal Debit { get; set; }
        public decimal Total { get; set; }

        public decimal Credit { get; set; }
        public DateTime Date { get; set; }
        public string TransactionDate { get; set; }
        public string DocumentDate { get; set; }

    }
}