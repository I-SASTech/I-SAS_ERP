using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class LedgerTransactionLookUpModel
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public string DocumentNumber { get; set; }
        public string TransactionType { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string DocumentDate { get; set; }
        public string ApprovalDate { get; set; }

        public string Description { get; set; }
        public string Year { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal OpeningBalance { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? PeriodId { get; set; }
        public bool IsArchived { get; set; }
    }
}