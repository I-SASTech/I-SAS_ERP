using System;
using System.Collections.Generic;

namespace Focus.Business.ProductGroups.Model
{
    public class ProductGroupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public bool Status { get; set; }
        public List<ProductListForGroupModel> ProductList { get; set; }
    }
    
}
