using System;

namespace Focus.Business.TransactionApplicationLogs.Model
{
    public class TransactionApplicationLogsLookupModel
    {
        public Guid? Id { get; set; }
        public int FreshLogMovingDays { get; set; }
        public int DeleteFromHistory { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public Guid? LocationId { get; set; }
    }
}
