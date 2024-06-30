using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.Categories.Queries.GetCategoryDetails
{
    public class CategoryDetailLookUpModel
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
        public bool IsService { get; set; }
        public int ReturnDays { get; set; }

        public static Expression<Func<Category, CategoryDetailLookUpModel>> Projection
        {
            get
            {
                return request => new CategoryDetailLookUpModel
                {
                    Id=request.Id,
                    IsReturn = request.IsReturn,
                    ReturnDays = request.ReturnDays,
                    Name = request.Name,
                    NameArabic = request.NameArabic,
                    Description = request.Description,
                    Code = request.Code,
                    PurchaseAccountId = request.PurchaseAccountId,
                    COGSAccountId = request.COGSAccountId,
                    InventoryAccountId = request.InventoryAccountId,
                    IncomeAccountId = request.IncomeAccountId,
                    SaleAccountId = request.SaleAccountId,
                    isActive = request.isActive,
                    IsService = request.IsService,
                };
            }
        }

        public static CategoryDetailLookUpModel Create(Category category)
        {
            return Projection.Compile().Invoke(category);
        }
    }
}
