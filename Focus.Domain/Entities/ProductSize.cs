using System;

namespace Focus.Domain.Entities
{
    public class ProductSize : BaseEntity
    {
        public Guid ProductId { get; set; }
        public virtual Product Products { get; set; }
        public Guid SizeId { get; set; }
        public virtual Size Size { get; set; }
    }
}
