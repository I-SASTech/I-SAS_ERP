using Focus.Domain.Enums;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
   public class ProductionBatch : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal DamageStock { get; set; }
        public decimal RemainingStock { get; set; }
        public string LateReason { get; set; }
        public string LateReasonCompletion { get; set; }
        public string LateReasonTransfer { get; set; }
        public string RegistrationNo { get; set; }
        public string NoOfBatches { get; set; }
        public string Note { get; set; }
        public Guid? SaleOrderId { get; set; }
        public virtual SaleOrder SaleOrder { get; set; }
        public Guid? EmployeeRegistrationId { get; set; }
        public virtual EmployeeRegistration EmployeeRegistration { get; set; }
        public Guid RecipeNoId { get; set; }
        public virtual RecipeNo RecipeNo { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }
        public string CompleteBy { get; set; }
        public string ProcessBy { get; set; }
        public string TransferBy { get; set; }
        public DateTime? CompleteDate { get; set; }
        public DateTime? ProcessDate { get; set; }
        public DateTime? TransferDate { get; set; }
        public decimal RecipeQuantity { get; set; }
        public virtual ICollection<ProductionBatchItem> ProductionBatchItems { get; set; }
        public virtual ICollection<BatchProcess> BatchProcesses { get; set; }
        public virtual ICollection<GatePass> GatePasses { get; set; }
        public Guid? BranchId { get; set; }
    }
}