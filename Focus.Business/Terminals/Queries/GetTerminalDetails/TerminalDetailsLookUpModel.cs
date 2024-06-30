using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Focus.Domain.Enum;

namespace Focus.Business.Terminals.Queries.GetTerminalDetails
{
    public class TerminalDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? PosTerminalId { get; set; }
        public Guid? CashAccountId { get; set; }
        public string Code { get; set; }
        public string MACAddress { get; set; }
        public string IPAddress { get; set; }
        public bool isActive { get; set; }
        public bool OverWrite { get; set; }
        public bool OnPageLoadItem { get; set; }
        public string PrinterName { get; set; }
        public TerminalType TerminalType { get; set; }
        public ICollection<Guid> CategoryIdList { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string BusinessNameEnglish { get; set; }
        public string BusinessTypeEnglish { get; set; }
        public string CompanyNameArabic { get; set; }
        public string BusinessNameArabic { get; set; }
        public string BusinessTypeArabic { get; set; }
        public string BusinessLogo { get; set; }
        public string TerminalUserType { get; set; }


        public static Expression<Func<Terminal, TerminalDetailsLookUpModel>> Projection
        {
            get
            {
                return terminal => new TerminalDetailsLookUpModel
                {
                    Id = terminal.Id,
                    Code = terminal.Code,
                    MACAddress = terminal.MACAddress,
                    IPAddress = terminal.IPAddress,
                    isActive = terminal.IsActive,
                    OverWrite = terminal.OverWrite,
                    AccountId = terminal.AccountId,
                    PosTerminalId = terminal.PosTerminalId,
                    CashAccountId = terminal.CashAccountId,
                    PrinterName = terminal.PrinterName,
                    TerminalType = terminal.TerminalType,
                    OnPageLoadItem = terminal.OnPageLoadItem,
                    CompanyNameEnglish = terminal.CompanyNameEnglish,
                    BusinessNameEnglish = terminal.BusinessNameEnglish,
                    BusinessTypeEnglish = terminal.BusinessTypeEnglish,
                    CompanyNameArabic = terminal.CompanyNameArabic,
                    BusinessNameArabic = terminal.BusinessNameArabic,
                    BusinessTypeArabic = terminal.BusinessTypeArabic,
                    BusinessLogo = terminal.BusinessLogo,
                    CategoryIdList = terminal.TerminalCategories != null? terminal.TerminalCategories.Select(x => x.CategoryId).ToList(): new List<Guid>(),
                    TerminalUserType = terminal.TerminalUserType.ToString()
                };
            }
        }


        public static TerminalDetailsLookUpModel Create(Terminal terminal)
        {
            return Projection.Compile().Invoke(terminal);
        }
    }
}
