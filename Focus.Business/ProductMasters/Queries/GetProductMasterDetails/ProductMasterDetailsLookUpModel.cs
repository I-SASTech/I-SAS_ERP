using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.ProductMasters.Queries.GetProductMasterDetails
{
    public class ProductMasterDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }

        public static Expression<Func<ProductMaster, ProductMasterDetailsLookUpModel>> Projection
        {
            get
            {
                return productMaster => new ProductMasterDetailsLookUpModel
                {
                    Id = productMaster.Id,
                    Name = productMaster.Name,
                    NameArabic = productMaster.NameArabic,
                    Description = productMaster.Description,
                    Code = productMaster.Code,
                    isActive = productMaster.isActive
                };
            }
        }

        public static ProductMasterDetailsLookUpModel Create(ProductMaster productMaster)
        {
            return Projection.Compile().Invoke(productMaster);
        }
    }
}
