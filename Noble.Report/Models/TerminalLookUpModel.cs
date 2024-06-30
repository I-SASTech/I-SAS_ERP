using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.Models
{
    public class TerminalLookUpModel
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
        public ICollection<Guid> CategoryIdList { get; set; }
        public string CompanyNameEnglish { get; set; }
        public string BusinessNameEnglish { get; set; }
        public string BusinessTypeEnglish { get; set; }
        public string CompanyNameArabic { get; set; }
        public string BusinessNameArabic { get; set; }
        public string BusinessTypeArabic { get; set; }
        public string BusinessLogo { get; set; }
        public string TerminalUserType { get; set; }
    }
}