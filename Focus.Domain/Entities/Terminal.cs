using System;
using Focus.Domain.Interface;
using System.Collections;
using System.Collections.Generic;
using Focus.Domain.Enum;

namespace Focus.Domain.Entities
{
    public class Terminal : BaseEntity, IAuditedEntityBase, ITenantFilterableEntity, ITenant
    {
        public string Code { get; set; }
        public string PrinterName { get; set; }
        public string MACAddress { get; set; }
        public string IPAddress { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string BusinessNameEnglish { get; set; }
        public string BusinessTypeEnglish { get; set; }
        public string CompanyNameArabic { get; set; }
        public string BusinessNameArabic { get; set; }
        public string BusinessTypeArabic { get; set; }
        public string BusinessLogo { get; set; }
        public bool IsActive { get; set; }
        public bool OverWrite { get; set; }
        public Guid? PosTerminalId { get; set; }
        public virtual BankPosTerminal BankPosTerminal { get; set; }
        public Guid? AccountId { get; set; }
        public virtual Account Account { get; set; }
        public Guid? CashAccountId { get; set; }
        public virtual Account CashAccount { get; set; }
        public virtual ICollection<UserTerminal> UserTerminals { get; set; }
        public virtual ICollection<TerminalCategory> TerminalCategories { get; set; }
        public TerminalType TerminalType { get; set; }
        public bool OnPageLoadItem { get; set; }
        public TerminalUserType TerminalUserType { get; set; }
        public Guid? BranchId { get; set; }


    }
}
