using System;

namespace Focus.Business.TemporaryCashRequests.Commands
{
    public class TemporaryCashRequestItemModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public Guid TemporaryCashRequestId { get; set; }
    }
}
