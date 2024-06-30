using System;
using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class ReparingOrderType : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public ReparingOrderTypeEnum ReparingOrderTypeEnums { get; set; }
        public virtual ICollection<ReparingOrder> WarrantyCategories { get; set; }
        public virtual ICollection<ReparingOrder> UpsDescriptions { get; set; }
        public virtual ICollection<ReparingOrder> Problems { get; set; }
        public virtual ICollection<ReparingOrder> AcessoryIncludes { get; set; }
        public virtual ICollection<MultiUPSLineItem> MultiWarrantyCategories { get; set; }
        public virtual ICollection<MultiUPSLineItem> MultiUpsDescriptions { get; set; }
        public virtual ICollection<MultiUPSLineItem> MultiProblems { get; set; }
        public virtual ICollection<MultiUPSLineItem> MultiAcessoryIncludes { get; set; }

        public bool isActive { get; set; }
        public Guid? BranchId { get; set; }
    }
}