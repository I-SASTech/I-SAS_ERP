using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
    public class Category : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
        public bool IsReturn { get; set; }
        public int ReturnDays { get; set; }
        public virtual Account PurchaseAccount { get; set; }
        public Guid? PurchaseAccountId { get; set; }
        public virtual Account COGSAccount { get; set; }
        public Guid? COGSAccountId { get; set; }
        public virtual Account InventoryAccount { get; set; }
        public Guid? InventoryAccountId { get; set; }
        public virtual Account IncomeAccount { get; set; }
        public Guid? IncomeAccountId { get; set; }
        public virtual Account SaleAccount { get; set; }
        public Guid? SaleAccountId { get; set; }
        public bool IsService { get; set; }
        public virtual ICollection<SubCategory> SubCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<TerminalCategory> TerminalCategories { get; set; }
    }
}
