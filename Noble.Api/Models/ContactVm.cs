using Focus.Business.Attachments.Commands;
using Focus.Business.Contacts;
using Focus.Business.PriceRecordChanges.Model;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Noble.Api.Models
{
    public class ContactVm
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string WhatsAppNumber { get; set; }
        public DateTime? Date { get; set; }
        public string Prefix { get; set; }
        public string CustomerCode { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string CompanyNameArabic { get; set; }
        public string CustomerDisplayName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string RegistrationDate { get; set; }
        public string Category { get; set; }
        public string CustomerType { get; set; }
        public string CustomerGroup { get; set; }
        public string ContactNo1 { get; set; }
        
        public string CommercialRegistrationNo { get; set; }
        public string VatNo { get; set; }

        public Guid? RegionId { get; set; }
        public Guid? CurrencyId { get; set; }
        public Guid? TaxRateId { get; set; }
        public string Website { get; set; }

        public string BillingAttention { get; set; }
        public string BillingCountry { get; set; }
        public string BillingZipCode { get; set; }
        public string BillingPhone { get; set; }
        public string BillingArea { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingFax { get; set; }

        public string ShippingAttention { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingArea { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingFax { get; set; }
        public Guid? CustomerGroupId { get; set; }
        public string Remarks { get; set; }
        public bool IsCustomer { get; set; }
        public bool IsActive { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsAddressOnAll { get; set; }
        public bool IsCashCustomer { get; set; }

        public string PaymentTerms { get; set; }
        public string DeliveryTerm { get; set; }
        public string CreditDays { get; set; }
        public string CreditLimit { get; set; }
        public string CreditPeriod { get; set; }

        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public List<ContactPerson> ContactPersonList { get; set; }
        public List<ContactBankAccount> ContactBankAccountList { get; set; }
        public List<AddressLookUpModel> DeliveryAddressList { get; set; }
        //End

        
        public SupplierType? SupplierType { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsExpire { get; set; }
        public bool Status { get; set; }
        public bool MultipleAddress { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? CaptureDate { get; set; }
        public string Reason { get; set; }

        //Bank Account
        public string BankAccountTitle1 { get; set; }
        public string BankAccountNo1 { get; set; }
        public string NameOfBank1 { get; set; }
        public string RoutingCode1 { get; set; }
        public string City1 { get; set; }
        public string IBAN1 { get; set; }
        public string BranchName1 { get; set; }
        public string Country1 { get; set; }

        public string BankAccountTitle2 { get; set; }
        public string BankAccountNo2 { get; set; }
        public string NameOfBank2 { get; set; }
        public string RoutingCode2 { get; set; }
        public string City2 { get; set; }
        public string IBAN2 { get; set; }
        public string BranchName2 { get; set; }
        public string Country2 { get; set; }
        public Guid? AccountId { get; set; }
        
        public string Location { get; set; }
        public string Coordinator { get; set; }
        public string Currency1 { get; set; }
        public string Currency2 { get; set; }
        public bool IsRaw { get; set; }
        public string Comments { get; set; }
        public string DeliveryAddress { get; set; }
        public string ShippingOther { get; set; }
        public string DeliveryOther { get; set; }

        public List<PriceLabelsProuctsListLookupModel> ProductList { get;set; }

        public Guid? PriceLabelId { get; set; }
        public bool IsAllowEmail { get; set; }
        public bool IsAutoEmail { get; set; }
    }
}
