using Focus.Business.Attachments.Commands;
using Focus.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Focus.Business.CreditNotes.Model
{
    public class CreditNotesModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public Guid ContactId { get; set; }
        public string InvoiceNote { get; set; }
        public Guid? SaleId { get; set; }
        public Guid? PurchasePostId { get; set; }
        public bool IsInventoryTransaction { get; set; }
        public bool IsItemDescription { get; set; }
        public bool IsService { get; set; }
        public string Narration { get; set; }
        public decimal VatAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal GrossAmount { get; set; }
        public bool IsCreditNote { get; set; }
        public Guid? TerminalId { get; set; }
        public Guid? CreatedBy { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public IList<CreditNotesItemModel> SaleOrderItems { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }

        public Guid? WareHouseId { get; set; }
        public string TaxMethod { get; set; }
        public Guid? TaxRateId { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string CustomerNameAr { get; set; }
        public string CustomerVat { get; set; }
        public string CustomerNameEn { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerAddress2 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Mobile { get; set; }
        public string WareHouseName { get; set; }
        public string QrCode { get; set; }
        public Guid? BranchId { get; set; }
        public string BranchCode { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerDisplayName { get; set; }
        public string DeliveryAddress { get; set; }
        public string CustomerCRN { get; set; }
        public string SaleNo { get; set; }
        public string TaxRateName { get; set; }
        public string CustomerEmail { get; set; }
        public bool IsDefault { get; set; }
        public string BillingZipCode { get;  set; }
    }
}
