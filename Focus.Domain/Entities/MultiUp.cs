
using Focus.Domain.Interface;
using System.Collections.Generic;
using System;

namespace Focus.Domain.Entities
{
    public class MultiUp : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public DateTime Date { get; set; }
        public string RegistrationNo { get; set; }
        public Guid? CustomerId { get; set; }
        public virtual Contact Customer { get; set; }
        public Guid? EmployeeId { get; set; }
        public virtual EmployeeRegistration Employee { get; set; }
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
        public bool IsRepared { get; set; }
        public bool IsReturn { get; set; }
        public bool IsPrint { get; set; }
        public virtual ICollection<MultiUPSLineItem> MultiUPSLineItems { get; set; }
        public virtual ICollection<PaymentVoucher> PaymentVouchers { get; set; }
        public string CradNumber { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string DealerRef { get; set; }
        public string FaultInfo { get; set; }
        public decimal RemaningPrice { get; set; }
        public Guid? BranchId { get; set; }
    }
}
