using System;

namespace Focus.Business.BranchSetups.Model
{
    public class BranchSetupLookupModel
    {
        public Guid Id { get; set; }
        public string Prefix { get; set; }
        public string Code { get; set; }
        public string StartingNumber { get; set; }
        public string EndNumber { get; set; }
        public Guid? LocationId { get; set; }
        public Guid? BusinessId { get; set; }

    }
}
