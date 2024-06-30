using Focus.Domain.Enums;
using System;
using System.Collections.Generic;
using Focus.Domain.Enum;
using Focus.Domain.Entities;

namespace Focus.Business.PriceRecords
{
    public class PriceRecordLookupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal NewPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public bool IsActive { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string PriceLabelName { get; set; }
        public Guid? PriceLabelingId { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string DisplayName { get; set; }
    }
}
