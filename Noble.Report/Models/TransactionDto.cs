using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class TransactionDto
    {
        public string TransactionType { get; set; }
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public string DocumentNumber { get; set; }
        public string Date { get; set; }
        public string DocumentDate { get; set; }
        public string TransactionDate { get; set; }
        public string ApprovalDate { get; set; }
        public string Description { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string Year { get; set; }
        public Guid PeriodId { get; set; }
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }

    }
}