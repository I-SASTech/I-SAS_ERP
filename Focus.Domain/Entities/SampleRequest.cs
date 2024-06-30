using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class SampleRequest : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string NoOfSampleRequests { get; set; }
         public Guid CustomerId { get; set; }
         public virtual Contact Customer { get; set; }
        public string ReferredBy { get; set; }
        public string RequestGenerated { get; set; }
        public DateTime Date { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public virtual ICollection<RecipeNo> RecipeNos { get; set; }
        public virtual ICollection<SampleRequestItem> SampleRequestItems { get; set; }
        public Guid? BranchId { get; set; }



    }
}
