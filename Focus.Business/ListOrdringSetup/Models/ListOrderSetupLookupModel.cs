using System;

namespace Focus.Business.ListOrdringSetup.Models
{
    public class ListOrderSetupLookupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Sequence { get; set; }
        public string DocumentName { get; set; }
    }
}
