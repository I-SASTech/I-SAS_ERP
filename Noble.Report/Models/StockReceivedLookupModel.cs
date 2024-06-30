﻿using Noble.Report.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class StockReceivedLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public StockTransferStatus StockTransferStatus { get; set; }
        public DateTime Date { get; set; }
        public Guid? WarehouseId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? StockTransferId { get; set; }
        public Guid? StockRequestId { get; set; }
        public string VehicalNo { get; set; }
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public string DriverNationalId { get; set; }
        public string StockTransferBranch { get; set; }
        public string StockRecievedBranch { get; set; }
        public bool IsStockReceived { get; set; }
        public decimal TotalAmount { get; set; }

        public List<StockReceivedItemsLookupModel> WareHouseTransferItems { get; set; }
    }
}