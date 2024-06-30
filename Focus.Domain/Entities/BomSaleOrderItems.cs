using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class BomSaleOrderItems : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid BomId { get; set; }
        public Guid SaleOrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public virtual Bom Boms { get; set; }
        public virtual Product Products { get; set; }
        public virtual ICollection<BomRawProducts> BomRawProducts { get; set; }
        
    }
}
