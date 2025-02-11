﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class ProductDropdownLookUpModel
    {
        public Guid Id { get; set; }
        public string BarCode { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public bool IsExpire { get; set; }
        public Guid TaxRateId { get; set; }
        public Inventory Inventory { get; set; }
        public string TaxMethod { get; set; }
        public string SaleReturnDays { get; set; }
        public bool Guarantee { get; set; }
        public bool Serial { get; set; }
        public string StyleNumber { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public decimal SalePrice { get; set; }
        public decimal? UnitPerPack { get; set; }
        public string LevelOneUnit { get; set; }
        public string BasicUnit { get; set; }
        public string StockLevel { get; set; }
        public bool ServiceItem { get; set; }
        public bool HighUnitPrice { get; set; }
        public decimal? WholesaleQuantity { get; set; }
        public decimal WholesalePrice { get; set; }
    }
}