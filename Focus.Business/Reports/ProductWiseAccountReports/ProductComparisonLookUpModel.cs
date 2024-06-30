using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Business.Reports.ProductWiseAccountReports
{
    public class ProductComparisonLookUpModel
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductArabicName { get; set; }
        public string Code { get; set; }
        public string IAccountType { get; set; }
        public string COGSAccountType { get; set; }
        public string SAccountType { get; set; }
        public decimal IOpeningBalance { get; set; }
        public decimal IClosingingBalance { get; set; }
        public decimal ITotal { get; set; }
        public decimal GSOpeningBalance { get; set; }
        public decimal GSClosingingBalance { get; set; }
        public decimal GSTotal { get; set; }
        public decimal SOpeningBalance { get; set; }
        public decimal SClosingingBalance { get; set; }
        public decimal STotal { get; set; }

        public Guid? BranchId { get; set; }

        //public ICollection<AccountLookUpModel> AccountLookUp { get; set; }
    }
}
