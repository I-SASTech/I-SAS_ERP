using System;
using System.Collections.Generic;
using Focus.Business.Attachments.Commands;
using Focus.Business.PriceRecordChanges.Model;
using Focus.Business.Sales.Queries.GetSaleList;
using Focus.Domain.Entities;
using Focus.Domain.Enum;

namespace Focus.Business.Contacts.Queries.GetContactDetails
{
    public class ContactDetailLookUpModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public string CustomerCode { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string CommercialRegistrationNo { get; set; }
        public string VatNo { get; set; }
        public string ContactNo1 { get; set; }
        public string PaymentTerms { get; set; }
        public string Remarks { get; set; }
        public string RegistrationDate { get; set; }
        public SupplierType? SupplierType { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Email { get; set; }
        public string DeliveryTerm { get; set; }
        public bool IsActive { get; set; }
        public bool IsCustomer { get; set; }
        public bool Status { get; set; }
        public bool IsCashCustomer { get; set; }
        public bool IsAddressOnAll { get; set; }

        public string CustomerType { get; set; }
        
        public Guid? AccountId { get; set; }
        public Guid? RegionId { get; set; }
        public string CreditPeriod { get; set; }
        public string CreditLimit { get; set; }
        public Guid? PriceLabelId { get; set; }

        //Attachment
        public string Telephone { get; set; }
        public string Website { get; set; }
        public bool IsRaw { get; set; }
        public string CustomerGroup { get; set; }
        public List<AttachmentLookUpModel> AttachmentList { get; set; }
        public List<ContactBankAccount> ContactBankAccountList { get; set; }
        public List<ContactPerson> ContactPersonList { get; set; }
        public string Prefix { get;  set; }
        public string CompanyNameEnglish { get;  set; }
        public string CustomerDisplayName { get;  set; }
        public string CompanyNameArabic { get;  set; }
        public Guid? CurrencyId { get;  set; }
        public Guid? TaxRateId { get;  set; }
        public string CreditDays { get;  set; }
        public string BillingFax { get;  set; }
        public string BillingAttention { get;  set; }
        public string BillingPhone { get;  set; }
        public string BillingZipCode { get;  set; }
        public string BillingCountry { get;  set; }
        public string BillingArea { get;  set; }
        public string BillingAddress { get;  set; }
        public string BillingCity { get;  set; }

        public string ShippingAttention { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingPhone { get; set; }
        public string ShippingArea { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingFax { get; set; }
        public string CurrencyName { get; set; }
        public string TaxRateName { get; set; }
        public string RunningBalance { get; set; }
        public List<SaleListLookupModel> InvoiceList { get; set; }
        public List<AddressLookUpModel> DeliveryAddressList { get; set; }
        public string DeliveryAddress { get; set; }
        public string ShippingOther { get; set; }
        public string DeliveryOther { get; set; }
        public List<PriceLabelsProuctsListLookupModel> ProductList { get; set; }

        public string WhatsAppNumber { get; set; }
        public DateTime? Date { get; set; }
        public Guid? CustomerGroupId { get; set; }
        public bool IsAllowEmail { get; set; }
        public bool IsAutoEmail { get; set; }
    }
}
