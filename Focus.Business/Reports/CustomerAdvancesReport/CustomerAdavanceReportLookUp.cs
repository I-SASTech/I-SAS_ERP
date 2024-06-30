using Focus.Business.Transactions.Commands;
using System.Collections.Generic;

namespace Focus.Business.Reports.CustomerAdvancesReport
{
    public class CustomerAdavanceReportLookUp
    {
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public decimal RunningBalance { get; set; }
        public decimal TotalBalance { get; set; }
        public List<TransactionDto> Transactions { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string Date { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public string CustomerNameEn { get; set; }
        public string CustomerNameAr { get; set; }
        public string CustomerNo { get; set; }
        public string Address { get; set; }
        public string CustomerVat { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }

    }
}
