using System;

namespace Focus.Business.TemporaryCashReturns.Commands
{
    public class TemporaryCashReturnLookUp
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public bool IsCashRequesterUser { get; set; }
        public Guid TemporaryCashIssueId { get; set; }
    }
}
