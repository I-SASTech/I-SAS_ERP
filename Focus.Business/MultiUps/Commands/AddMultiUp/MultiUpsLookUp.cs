using System;
using System.Collections.Generic;

namespace Focus.Business.MultiUps.Commands.AddMultiUp
{
    public class MultiUpLookUp
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Dates { get; set; }
        public string Registeredby { get; set; }
        public string RegistrationNo { get; set; }
        public Guid? CustomerId { get; set; }
        public string CustomerNameEn { get; set; }
        public string Address { get; set; }
        public string CustomerNameAr { get; set; }
        public string EmployeeNameEn { get; set; }
        public string EmployeeNameAr { get; set; }
        public string UpsDes { get; set; }
        public string Type { get; set; }
        public string Issue { get; set; }
        public string Accessor { get; set; }
        public string DoneBy { get; set; }
        public string PaymentType { get; set; }
        public string Mobile { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? WarrantyCategoryId { get; set; }
        public Guid? UpsDescriptionId { get; set; }
        public Guid? ProblemId { get; set; }
        public Guid? AcessoryIncludedId { get; set; }

        public string SerialNo { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public decimal EstimateAmount { get; set; }
        public decimal Payment { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public DateTime? CompleteDate { get; set; }
        public Guid? JobAssignId { get; set; }
        public Guid? RegisteredById { get; set; }
        public virtual ICollection<MultiUpsLineItemLookUp> MultiUpsLineItems { get; set; }
        public string CradNumber { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public string DealerRef { get; set; }
        public string FaultInfo { get; set; }
        public bool IsComplete { get; set; }
        public decimal RemaningPrice { get; set; }
        public decimal Discount { get; set; }
        public bool IsCollected { get; set; }
        public bool IsRepared { get; set; }
        public bool IsPrint { get; set; }
        public bool IsReturn { get; set; }
        public decimal AdvanceAmount { get; set; }
        public decimal CashAmount { get; set; }
        public bool IsCashed { get; set; }
        public Guid? BranchId { get; set; }
    }
}
