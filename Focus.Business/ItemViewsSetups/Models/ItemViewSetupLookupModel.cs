using System;

namespace Focus.Business.ItemViewsSetups.Models
{
    public class ItemViewSetupLookupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Sequence { get; set; }
    }
}
