using Focus.Business.Permission.Commands;
using System.Collections.Generic;

namespace Focus.Business.Permission
{
   public class CostCenterLookUp
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool VatDeductible { get; set; }
        public bool IsActive { get; set; }
        public List<AccountLookUp> Accounts { get; set; }
    }
}
