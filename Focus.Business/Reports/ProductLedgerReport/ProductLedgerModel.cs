using Focus.Domain.Entities;
using Focus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.Reports.ProductLedgerReport
{
    public class ProductLedgerModel
    {
        public Guid? Id { get; set; }
        public string DocumentNumber { get; set; }
        public string TransactionType { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string DocumentDate { get; set; }
        public string ApprovalDate { get; set; }

        public string Description { get; set; }
        public string Year { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal RunningBalance { get; set; }
        
        
        public ICollection<LedgerTransactionLookUpModel> LedgerTransactionLookUpModels { get; set; }
    }
}
