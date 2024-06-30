using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
   public class RecipeNo : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime Date { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string RegistrationNo { get; set; }
        public string RecipeName { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }
        public Guid? SampleRequestId { get; set; }
        public virtual SampleRequest SampleRequest { get; set; }
        public virtual ICollection<RecipeItem> RecipeItems { get; set; }
        public virtual ICollection<ProductionBatch> ProductionBatchs { get; set; }
        public virtual ICollection<BatchCosting> BatchCostings { get; set; }
        public virtual ICollection<RecipeAssortment> RecipeAssortments { get; set; }


    }
}