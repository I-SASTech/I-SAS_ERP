using System;

namespace Focus.Business.TemporaryCashIssues.Commands
{
    public class TemporaryCashIssueItemModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid TemporaryCashRequestId { get; set; }
    }
}
