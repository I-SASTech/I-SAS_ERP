using Focus.Domain.Enum;
using System;
using System.Collections.Generic;

namespace Noble.Api.Models
{
    public class LoginVm
    {
        public string Id { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? TerminalId { get; set; }
        public Guid? OnlineTerminalId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public Guid UserId { get; set; }
        public string LastName { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string ClientNo { get; set; }
        public bool ChangePriceDuringSale { get; set; }
        public bool GiveDicountDuringSale { get; set; }
        public bool ViewCounterDetails { get; set; }
        public bool TransferCounter { get; set; }
        public bool CloseCounter { get; set; }
        public bool HoldCounter { get; set; }
        public bool CloseDay { get; set; }
        public bool ProcessSaleReturn { get; set; }
        public bool DailyExpenseList { get; set; }
        public bool StartDay { get; set; }
        public bool InvoiceWoInventory { get; set; }
        public bool IsTouchInvoice { get; set; }
        public bool IsActive { get; set; }
        public bool AllowAll { get; set; }
        public bool PermissionToStartExpenseDay { get; set; }
        public bool IsSupervisor { get;  set; }
        public string TouchScreen { get; set; }
        public bool TemporaryCashReceiver { get; set; }
        public bool TemporaryCashIssuer { get; set; }
        public bool TemporaryCashRequester { get; set; }
        public decimal Days { get; set; }
        public decimal Limit { get; set; }
        public bool IsExpenseAccount { get;  set; }
        public bool AllowViewAllData { get; set; }
        public string TerminalUserType { get; set; }
        public string DocumentType { get; set; }
        public bool IsOverAllAccess { get; set; }
        public Guid? BankId { get;  set; }
    }
}
