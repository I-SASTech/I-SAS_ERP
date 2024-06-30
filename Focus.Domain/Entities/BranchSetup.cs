using System;


namespace Focus.Domain.Entities
{
    public class BranchSetup : BaseEntity
    {
        public string Prefix { get; set; }
        public string Code { get; set; }
        public string StartingNumber { get; set; }
        public string EndNumber { get; set; }
        public Guid? LocationId { get; set; }
    }
}
