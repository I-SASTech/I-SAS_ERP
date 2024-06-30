
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class CustomerDiscount:BaseEntity,ITenant,IAuditedEntityBase,ITenantFilterableEntity
    {
        public string DiscountName { get; set; }
        public double Discount { get; set; }
        public double FreightDiscount { get; set; }
        public double ExtraDiscount { get; set; }
        public double OtherDiscount { get; set; }
        public double OpenDiscount { get; set; }
    }
}
