using System;
using System.Collections.Generic;
using System.Linq;
using Focus.Business.Attachments.Commands;
using Focus.Business.StockAdjustments.Queries.GetStockAdjustmentDetails;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.StockAdjustments.Queries.GetStockAdjustmentList
{
    public class StockAdjustmentLookUpModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Code { get; set; }
        public string Narration { get; set; }
        public bool isDraft { get; set; }
        public Guid WarehouseId { get; set; }
        public StockAdjustmentType StockAdjustmentType { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public string WarehouseName { get; set; }
        //public ProductDropdownLookUpModel StockAdjustmentDetails { get; set; }

        public virtual List<StockAdjustmentLookDetailUpModel> StockAdjustmentDetails { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public string Reason { get; set; }
        public string TaxRateName { get; set; }
        public decimal Total { get; set; }
        public bool IsSerial { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public Guid? BankCashAccountId { get; set; }

        public static StockAdjustmentLookUpModel Create(Focus.Domain.Entities.StockAdjustment stockAdjustment)
        {
            var lookUpModel = new StockAdjustmentLookUpModel
            {
                Id = stockAdjustment.Id,
                Code = stockAdjustment.Code,
                Reason = stockAdjustment.Reason,
                Narration=stockAdjustment.Narration,
                WarehouseId=stockAdjustment.WarehouseId,
                WarehouseName = stockAdjustment.Warehouse?.Name,
                Date=stockAdjustment.Date,
                isDraft= stockAdjustment.isDraft,
                TaxRateName = stockAdjustment.TaxRate?.Name,
                Total = stockAdjustment.TaxRateId!=null? stockAdjustment.TaxMethod== "Exclusive"? (stockAdjustment.StockAdjustmentDetails.Sum(x=>x.Price*x.Quantity) * (stockAdjustment.TaxRate.Rate+100)/100): stockAdjustment.StockAdjustmentDetails.Sum(x => x.Price * x.Quantity)
                    : stockAdjustment.StockAdjustmentDetails.Sum(x => x.Price * x.Quantity)
            };
            return lookUpModel;
        }
    }
}
