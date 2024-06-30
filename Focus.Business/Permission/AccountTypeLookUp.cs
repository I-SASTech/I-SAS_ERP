using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Permission
{
   public class AccountTypeLookUp
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public bool IsActive { get; set; }
        public List<CostCenterLookUp> CostCenters { get; set; }
    }
}
