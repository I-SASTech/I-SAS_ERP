using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class InvoiceDefault : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public bool IsSalePrice { get; set; }
        public bool IsCustomerPrice { get; set; }
        public bool IsSalePriceLabel { get; set; }
        public bool IsCustomerPriceLabel { get; set; }
    }
}
