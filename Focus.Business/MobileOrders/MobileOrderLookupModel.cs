using Focus.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Focus.Business.MobileOrders
{
    public class MobileOrderLookupModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual ICollection<MobileOrderItemLookupModel> MobileOrderItemLookupModels { get; set; }
    }
}
