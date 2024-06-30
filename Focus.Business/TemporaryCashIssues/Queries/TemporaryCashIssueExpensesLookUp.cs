using System;

namespace Focus.Business.TemporaryCashIssues.Queries
{
    public class TemporaryCashIssueExpensesLookUp
    {
        public Guid Id { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentType { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
    }
}
