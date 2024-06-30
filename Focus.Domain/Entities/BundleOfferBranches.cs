using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class BundleOfferBranches : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public Guid? BundleCategoriesId { get; set; }
        public Guid? PromotionOfferId { get; set; }
        public Guid? BranchId { get; set; }
        public virtual BundleCategory BundleCategories { get; set; }
        public virtual PromotionOffer PromotionOffer { get; set; }
    }
}
