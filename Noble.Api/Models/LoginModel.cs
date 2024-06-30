using Focus.Business.DefaultSettingInvoice.DefaultSattingCommand;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Noble.Api.Models
{
    public class LoginModel
    {
        public string Id { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? CounterId { get; set; }
        public Guid? CashAccountId { get; set; }
        public Guid? BankAccountId { get; set; }
        public Guid? TerminalId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public string ImagePath { get; set; }
        public string PhoneNo { get; set; }
        public object Token { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsMultiUnit { get; set; }
        public bool MainBranch { get; set; }
        public bool IsProduction { get; set; }
        public bool InvoiceWoInventory { get; set; }
        public string UserRoleName { get; set; }
        public bool? IsTouchInvoice { get; set; }
        public bool IsDayStart { get; set; }
        public string PrintSizeA4 { get; set; }
        public string TermsInAr { get; set; }
        public string TermsInEng { get; set; }
        public bool IsArea { get; set; }
        public bool IsProceed { get; set; }
        public bool Arabic { get; set; }
        public bool English { get; set; }
        public bool? AllowAll { get;set; }
        public bool Terminal { get; set; }
        public bool DayStart { get; set; }
        public bool Step1 { get; set; }
        public bool Step2 { get; set; }
        public bool Step3 { get; set; }
        public bool Step4 { get; set; }
        public bool Step5 { get; set; }
        public bool? IsExpenseDay { get; set; }
        public bool TermsCondition { get; set; }
        public Guid? WarehouseId { get; set; }
        public string LoginUserName { get; set; }
        public bool CashVoucher { get; set; }
        public decimal? ReturnDays { get; set; }
        public bool IsOpenDay { get; set; }
        public bool IsTransferAllow { get; set; }
        public bool IsSupervisor { get; set; }
        public bool IsPermissionToStartDay { get; set; }
        public bool IsPermissionToCloseDay { get; set; }
        public string DayStartTime { get; set; }
        public bool TransferCounter { get; set; }
        public bool IsMasterProductPermission { get; set; }
        public string PrinterName { get; set; }
        public bool IsHeaderFooter { get; set; }
        public string NobleRole { get; set; }
        public string SaleOrder { get; set; }
        public string InvoicePrint { get; set; }
        public bool SimpleInvoice { get; set; }
        public bool SoInventoryReserve { get; set; }
        public bool IsBlindPrint { get; set; }
        public bool IsDeliveryNote { get; set; }
        public bool AutoPaymentVoucher { get; set; }
        public bool InternationalPurchase { get; set; }
        public bool PartiallyPurchase { get; set; }
        public bool VersionAllow { get; set; }
        public bool PurchaseOrder { get; set; }
        public bool ExpenseToGst { get; set; }
        public bool AutoPurchaseVoucher { get; set; }
        public string PrintTemplate { get; set; }
        public bool IsForMedical { get; set; }
        public bool IsExpenseAccount { get;  set; }
        public bool SuperAdminDayStart { get; set; }
        public bool BankDetail { get; set; }
        public bool OnPageLoadItem { get; set; }
        public List<CompanyOption> CompanyOptions { get; set; }
        public string TaxInvoiceLabel { get;  set; }
        public string BusinessLogo { get;  set; }
        public string BusinessNameArabic { get;  set; }
        public string BusinessNameEnglish { get;  set; }
        public string BusinessTypeArabic { get;  set; }
        public string BusinessTypeEnglish { get;  set; }
        public string CompanyNameArabic { get;  set; }
        public string CompanyNameEnglish { get;  set; }
        public string TaxInvoiceLabelAr { get;  set; }
        public string SimplifyTaxInvoiceLabel { get;  set; }
        public string SimplifyTaxInvoiceLabelAr { get;  set; }
        public bool OverWrite { get;  set; }
        public string TouchScreen { get;  set; }
        public string Expiration { get;  set; }
        public string TerminalUserType { get;  set; }
        public bool IsPayment { get;  set; }
        public bool IsUseMachine { get;  set; }
        public bool IsNotPayment { get;  set; }
        public bool TermAndConditionLength { get;  set; }
        public DefaultSettingModel DefaultSettingModel { get;  set; }
        public Guid? BranchId { get; set; }
        public string BranchName { get; set; }
    }
}
