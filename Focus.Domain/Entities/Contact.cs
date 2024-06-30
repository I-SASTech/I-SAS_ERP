using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class Contact : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public Guid? CashCustomerId { get; set; }
        public string Code { get; set; }
        public string Prefix { get; set; }

        public string Category { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string CompanyNameArabic { get; set; }
        public string CustomerDisplayName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string RegistrationDate { get; set; }
        public string CustomerType { get; set; }
        public string CustomerGroup { get; set; }
        public string ContactNo1 { get; set; }

        public string CommercialRegistrationNo { get; set; }
        public string VatNo { get; set; }

        public Guid? CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public Guid? RegionId { get; set; }
        public virtual Region Region { get; set; }
        public Guid? TaxRateId { get; set; }
        public virtual TaxRate TaxRate { get; set; }
        public string Website { get; set; }

        public string BillingAttention { get; set; }
        public string Country { get; set; }
        public string BillingZipCode { get; set; }
        public string BillingPhone { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string BillingFax { get; set; }

        public string ShippingAttention { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingArea { get; set; }
        public string ShippingAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string ShippingOther { get; set; }
        public string DeliveryOther { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingFax { get; set; }

        public string Remarks { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsAddressOnAll { get; set; }
        public bool IsActive { get; set; }
        public bool IsCashCustomer { get; set; }

        public string PaymentTerms { get; set; }
        public string DeliveryTerm { get; set; }
        public string CreditDays { get; set; }
        public string CreditLimit { get; set; }
        public string CreditPeriod { get; set; }
        public string WhatsAppNumber { get; set; }
        public DateTime? Date { get; set; }
        //End

        public string ContactPerson1 { get; set; }
        public string ContactPerson2 { get; set; }
        public string ContactNo2 { get; set; }
        public SupplierType? SupplierType { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string ContactNumber { get; set; }
        public bool IsExpire { get; set; }
        public bool Status { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? CaptureDate { get; set; }
        public string Reason { get; set; }
        public Guid? AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid? AdvanceAccountId { get; set; }
        public virtual Account AdvanceAccount { get; set; }
        public Guid? SupplierCashAccountId { get; set; }
        public virtual Account SupplierCashAccount { get; set; }
        public DateTime? StatusDate { get; set; }
        public bool IsRaw { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Skype { get; set; }
        public string CustomerCode { get; set; }

        public virtual ICollection<ContactBankAccount> ContactBankAccounts { get; set; }
        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }
        public virtual ICollection<ContactAttachment> ContactAttachments { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual ICollection<SaleOrder> SaleOrders { get; set; }
        public virtual ICollection<PurchasePost> PurchasePosts { get; set; }
        public virtual ICollection<JournalVoucherItem> JournalVoucherItems { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<DispatchNote> DispatchNotes { get; set; }
        public virtual ICollection<AutoPurchaseTemplate> AutoPurchaseTemplates { get; set; }
        public virtual ICollection<SampleRequest> SampleRequests { get; set; }
        public virtual ICollection<Inquiry> Inquiries { get; set; }
        public virtual ICollection<GoodReceiveNote> GoodReceiveNotes { get; set; }
        public virtual ICollection<ReparingOrder> ReparingOrders { get; set; }
        public virtual ICollection<ContactPerson> ContactPersons { get; set; }
        public virtual ICollection<DeliveryChallan> DeliveryChallans { get; set; }

        public virtual ICollection<MultiUp> MultiUps { get; set; }
        public virtual ICollection<CreditNote> CreditNotes { get; set; }

        public Guid? CustomerGroupId { get; set; }
        public virtual CustomerGroup CustomerGroups { get; set; }
        public Guid? PriceLabelId { get; set; }
        public bool IsAllowEmail { get; set; }
        public bool IsAutoEmail { get; set; }
    }
}
