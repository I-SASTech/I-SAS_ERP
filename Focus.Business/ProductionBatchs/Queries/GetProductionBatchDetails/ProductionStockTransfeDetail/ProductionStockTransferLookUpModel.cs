using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.ProductionBatchs.Queries.GetProductionBatchDetails.ProductionStockTransfeDetail
{
  public  class ProductionStockTransferLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public decimal DamageStock { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal RemainingStock { get; set; }
        public string Note { get; set; }
        public Guid RemainingWareHouse { get; set; }
        public string RemainingWareHouseName { get; set; }
        public Guid DamageWareHouse { get; set; }
        public string DamageWareHouseName { get; set; }
        public Guid ProductionBatchId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        //public static Expression<Func<ProductionStockTransfer, ProductionStockTransferLookUpModel>> Projection
        //{
        //    get
        //    {
        //        return designation => new ProductionStockTransferLookUpModel
        //        {
        //            Id = designation.Id,
        //            Date = designation.Date,
        //            Code = designation.Code,
        //            DamageStock = designation.DamageStock,
        //            UnitPrice = designation.UnitPrice,
        //            RemainingWareHouse = designation.RemainingWareHouse,
        //            DamageWareHouse = designation.DamageWareHouse,
        //            ProductionBatchId = designation.ProductionBatchId,
        //            ProductId = designation.ProductId,

        //        };
        //    }
        //}

        //public static ProductionStockTransferLookUpModel Create(ProductionStockTransfer designation)
        //{
        //    return Projection.Compile().Invoke(designation);
        //}
    }
}
