using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
    public class MultiUPSLineItem : BaseEntity
    {
        public Guid? WarrantyCategoryId { get; set; }
        public virtual ReparingOrderType WarrantyCategory { get; set; }
        public Guid? UpsDescriptionId { get; set; }
        public virtual ReparingOrderType UpsDescription { get; set; }
        public Guid? ProblemId { get; set; }
        public virtual ReparingOrderType Problem { get; set; }
        public Guid? AcessoryIncludedId { get; set; }
        public virtual ReparingOrderType AcessoryInclude { get; set; }
        public string Status { get; set; }
        public Guid? JobAssignId { get; set; }
        public decimal EstimateAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal CashAmount { get; set; }
        public bool IsCashed { get; set; }
        public string PaymentType { get; set; }
        public string SerialNo { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public bool IsComplete { get; set; }
        public bool IsCollected { get; set; }
        public bool IsRepared { get; set; }
        public bool IsReturn { get; set; }
        public bool IsPrint { get; set; }
        public Guid? MultiUpId { get; set; }
        public virtual MultiUp MultiUp { get; set; }

    }
}
