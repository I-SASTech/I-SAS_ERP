using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class BatchCosting : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {

        public string Code { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid RecipeNoId { get; set; }
        public virtual RecipeNo RecipeNo { get; set; }
        public DateTime Date { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public virtual ICollection<BatchCostingItem> BatchCostingItems { get; set; }
    }
}
