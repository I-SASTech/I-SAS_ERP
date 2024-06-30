using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
   public class PriceRecord : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
   {
       public string Name { get; set; }
       public decimal Price { get; set; }
       public decimal PurchasePrice { get; set; }
       public decimal SalePrice { get; set; }
       public decimal CostPrice { get; set; }
       public bool IsActive { get; set; }
       public Guid ProductId { get; set; }
       public virtual Product Product { get; set; }
       public Guid? PriceLabelingId { get; set; }
       public virtual PriceLabeling PriceLabeling { get; set; }
       public Guid? CustomerId { get; set; }
       public bool IsCustomer { get; set; }
       public decimal NewPrice { get; set; }

    }
}
