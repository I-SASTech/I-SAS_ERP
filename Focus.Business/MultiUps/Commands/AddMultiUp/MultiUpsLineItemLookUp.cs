using System;

namespace Focus.Business.MultiUps.Commands.AddMultiUp
{
    public class MultiUpsLineItemLookUp
    {
        public Guid? Id { get; set; }

        public Guid? WarrantyCategoryId { get; set; }
        public string WarrantyCategory { get; set; }
        public Guid? JobAssignId { get; set; }
        public Guid? UpsDescriptionId { get; set; }
        public string UpsDescription { get; set; }
        public Guid? ProblemId { get; set; }
        public string Problem { get; set; }
        public Guid? AcessoryIncludedId { get; set; }
        public string AcessoryIncluded { get; set; }
        public string Status { get; set; }
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
    }
}
