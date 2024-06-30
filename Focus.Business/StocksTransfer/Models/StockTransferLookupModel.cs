using Focus.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Focus.Business.StocksTransfer.Models
{
    public class StockTransferLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public StockTransferStatus StockTransferStatus { get; set; }
        public string StockStatus { get; set; }
        public string StockRequestedBranch { get; set; }
        public string StockTransferBranch { get; set; }
        public DateTime Date { get; set; }
        public Guid? WareHouseId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? StockRequestId { get; set; }
        public string VehicalNo { get; set; }
        public string DriverName { get; set; }
        public string DriverNumber { get; set; }
        public string DriverNationalId { get; set; }
        public bool MainBranch { get; set; }
        public Guid? StockRequesBranchtId { get; set; }
        public Guid? VoucherId { get; set; }
        public decimal TotalAmount { get; set; }
        public string BranchType { get; set; }
        public List<StockTransferItemsLookupModel> wareHouseTransferItems { get; set; }
    }
}
