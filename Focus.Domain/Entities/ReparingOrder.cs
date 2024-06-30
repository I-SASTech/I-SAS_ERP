
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
   public class ReparingOrder : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public Guid? CustomerId { get; set; }
        public virtual Contact Customer { get; set; }
        public Guid? EmployeeId { get; set; }
        public virtual EmployeeRegistration Employee { get; set; }

        public Guid? WarrantyCategoryId { get; set; }
        public virtual ReparingOrderType WarrantyCategory { get; set; }
        public Guid? UpsDescriptionId { get; set; }
        public virtual ReparingOrderType UpsDescription { get; set; }
        public Guid? ProblemId { get; set; }
        public virtual ReparingOrderType Problem { get; set; }
        public Guid? AcessoryIncludedId { get; set; }
        public virtual ReparingOrderType AcessoryInclude { get; set; }
        public string SerialNo { get; set; }
        public decimal RemaningPrice { get; set; }
        public decimal Discount { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public decimal EstimateAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal CashAmount { get; set; }
        public bool IsCashed { get; set; }
        public string PaymentType { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public Guid? JobAssignId { get; set; }
        public Guid? RegisteredById { get; set; }
        public bool IsComplete { get; set; }
        public bool IsCollected { get; set; }
        public bool IsRepared{ get; set; }
        public bool IsReturn{ get; set; }
        public bool IsPrint { get; set; }
        public virtual ICollection<ReparingItem> ReparingItems { get; set; }
        public virtual ICollection<PaymentVoucher> PaymentVouchers { get; set; }
        public string CradNumber { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string DealerRef { get; set; }
        public string FaultInfo { get; set; }
        public Guid? BranchId { get; set; }

        public string Year { get; set; }
        public Guid? PeriodId { get; set; }
    }
}