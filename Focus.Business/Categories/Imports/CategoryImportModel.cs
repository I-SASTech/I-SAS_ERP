using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Categories.Imports
{
   public class CategoryImportModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
        public Guid? PurchaseAccountId { get; set; }
        public Guid? COGSAccountId { get; set; }
        public Guid? InventoryAccountId { get; set; }
        public Guid? IncomeAccountId { get; set; }
        public Guid? SaleAccountId { get; set; }
    }
}
