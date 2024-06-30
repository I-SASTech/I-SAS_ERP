using Focus.Domain.Enums;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
    public class WareHouseTransfer : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity, ISoftDelete
    {
        public string Code { get; set; }
        public Guid FromWareHouseId { get; set; }
        public Guid ToWareHouseId { get; set; }
        public Guid? FromBranchId { get; set; }
        public Guid? ToBranchId { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<WareHouseTransferItem> WareHouseTransferItems { get; set; }

        public Guid? BranchId { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}
