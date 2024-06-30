using System;

namespace Focus.Domain.Entities
{
    public class GsmSmsSetup:BaseEntity
    {
        public string ComPort { get; set; }
        public string DefaultMessage { get; set; }
        public Guid? BranchId { get; set; }
    }
}
