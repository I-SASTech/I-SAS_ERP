using Focus.Business.TemporaryCashIssues.Queries;
using System;
using System.Collections.Generic;

namespace Focus.Business.TemporaryCashRequests.Queries
{
    public class TemporaryCashRequestListLookUp
    {
        public Guid Id { get; set; }
        public string RegistrationNo { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public decimal ReturnAmount { get; set; }
        public bool IsClose { get; set; }
        public bool IsCashRequesterUser { get; set; }
        public Guid UserId { get; set; }
        public Guid? TemporaryCashAccountId { get; set; }
        public List<TemporaryCashIssueExpensesLookUp> TemporaryCashIssueExpenses { get; set; }
        public decimal VoucherAmount { get; set; }
    }
}
