using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class DispatchNoteItem : BaseEntity, ISoftDelete, ITenant
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public Guid DispatchNoteId { get; set; }
        public virtual DispatchNote DispatchNote { get; set; }
    }
}
