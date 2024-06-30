using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Focus.Business.StockRequests.Models
{
    public class StockRequestLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public StockRequestStatus StockRequestStatus { get; set; }
        public Guid? WareHouseId{ get; set; }
        public Guid? FromWareHouseId{ get; set; }
        public Guid? ToWareHouseId { get; set; }
        public string ToBranchRequestName { get; set; }
        public Guid? FromBranchId { get; set; }
        public string FromBranchName { get; set; }
        public Guid? ToBranchId { get; set; }
        public string ToBranchName { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime Date { get; set; }
        public Guid? BranchId { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public List<StockRequestItemsLookupModel> WareHouseTransferItems { get; set; }
    }
}
