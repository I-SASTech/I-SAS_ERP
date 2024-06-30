using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class CategoryVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
        public Guid? PurchaseAccountId { get; set; }
        public Guid? COGSAccountId { get; set; }
        public Guid? InventoryAccountId { get; set; }
        public Guid? IncomeAccountId { get; set; }
        public Guid? SaleAccountId { get; set; }
        public bool IsReturn { get; set; }
        public int ReturnDays { get; set; }
        public bool IsService { get; set; }
    }
}
