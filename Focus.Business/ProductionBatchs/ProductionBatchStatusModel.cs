using Focus.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;

namespace Focus.Business.ProductionBatchs
{
    public class ProductionBatchStatusModel
    {
        public Guid? Id { get; set; }
        public string LateReason { get; set; }
        public string LateReasonCompletion { get; set; }
        public decimal DamageStock { get; set; }
        public decimal RemainingStock { get; set; }
        public decimal UnitPrice { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public Guid? RemainingWareHouse { get; set; }
        public Guid? DamageWareHouse { get; set; }
        public string CompleteBy { get; set; }
        public string ProcessBy { get; set; }
        public string TransferBy { get; set; }
    }
}
