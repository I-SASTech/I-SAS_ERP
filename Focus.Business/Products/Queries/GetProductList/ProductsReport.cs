using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Entities;

namespace Focus.Business.Products.Queries.GetProductList
{
  public  class ProductsReport
    {
        public Product Product { get; set; }
        public Inventory Inventory { get; set; }
    }
}
