﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class StockReceivedItemsLookupModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal TransferQuantity { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public decimal Amount { get; set; }
        public decimal TransferAmount { get; set; }
        public Guid? BranchId { get; set; }
        public Guid StockReceivedId { get; set; }
        public decimal RemainingQuantity { get; set; }
        public decimal lineTotal { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
    }
}