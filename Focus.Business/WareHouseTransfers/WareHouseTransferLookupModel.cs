using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;

namespace Focus.Business.WareHouseTransfers
{
    public class WareHouseTransferLookupModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public Guid FromWareHouseId { get; set; }
        public Guid ToWareHouseId { get; set; }
        public Guid? FromBranchId { get; set; }
        public Guid? ToBranchId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime Date { get; set; }
        public ICollection<WareHouseTransferItemLookupModel> WareHouseTransferItems { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public Guid? BranchId { get; set; }
    }
}
