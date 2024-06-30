using System;

namespace Focus.Business.HoldTypeSetup.Models
{
    public class HoldSetupLookupModel
    {
        public Guid Id { get; set; }
        public string HoldRecordType { get; set; }
        public bool IsActive { get; set; }
    }
}
