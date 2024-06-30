using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
    public class Company: BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public bool Blocked { get; set; }
        public string LogoPath { get; set; }
        public string HouseNumber { get; set; }
        public string CompanyRegNo { get; set; }
        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public string VatRegistrationNo { get; set; }
        public string CompanyEmail { get; set; }
        public string CityEnglish { get; set; }
        public string CityArabic { get; set; }
        public string CountryEnglish { get; set; }
        public string CountryArabic { get; set; }
        public string CategoryInEnglish { get; set; }
        public string CategoryInArabic { get; set; }
        public string AddressEnglish { get; set; }
        public string AddressArabic { get; set; }
        public string PhoneNo { get; set; }
        public string Landline { get; set; }
        public string Website { get; set; }
        public string Postcode { get; set; }
        public string Town { get; set; }
        public string TaxInvoiceLabel { get; set; }
        //Auto Generate ClintNo 
        public string ClientNo { get; set; }
        public Guid ParentId { get; set; }
        public Guid? ClientParentId { get; set; }
        public Guid? BusinessParentId { get; set; }
        public virtual CompanyInformation CompanyInformation { get; set; }
        public virtual ICollection<CompanyLicence> CompanyLicences { get; set; }
        public virtual ICollection<CompanyAttachment> CompanyAttachments { get; set; }
        public virtual ICollection<TransactionApplicationLog> TransactionApplicationLogs { get; set; }
        public bool IsMultiUnit { get; set; }
        public bool IsProduction { get; set; }
        public bool InvoiceWoInventory { get; set; }
        public bool IsArea { get; set; }
        public bool IsProceed { get; set; }
        public bool English { get; set; }
        public bool Arabic { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string CompanyNameArabic { get; set; }
        public bool Terminal { get; set; }
        public bool DayStart { get; set; }
        public bool Step1 { get; set; }
        public bool Step2 { get; set; }
        public bool Step3 { get; set; }
        public bool Step4 { get; set; }
        public bool Step5 { get; set; }
        public bool TermsCondition { get; set; }
        public bool CashVoucher { get; set; }
        public bool IsOpenDay { get; set; }
        public bool IsTransferAllow { get; set; }
        public bool MasterProduct { get; set; }
        public bool SimpleInvoice { get; set; }
        public bool SoInventoryReserve { get; set; }
        public bool InternationalPurchase { get; set; }
        public bool PartiallyPurchase { get; set; }
        public bool VersionAllow { get; set; }
        public bool PurchaseOrder { get; set; }
        public bool ExpenseToGst { get; set; }
        public string SaleOrder { get; set; }
        public bool AutoPurchaseVoucher { get; set; }
        public bool IsForMedical { get; set; }
        public bool ExpenseAccount { get; set; }
        public bool SuperAdminDayStart { get; set; }
        public bool BankDetail { get; set; }
        public string TaxInvoiceLabelAr { get; set; }
        public string SimplifyTaxInvoiceLabel { get; set; }
        public string SimplifyTaxInvoiceLabelAr { get; set; }
    }
}
