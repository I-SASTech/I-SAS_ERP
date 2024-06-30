using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Noble.Report.NobleDefaultServices
{
    public class ModuleWiseClaimsLookupModel
    {
        public string TokenName { get; set; }
        public string Token { get; set; }
        public Guid CompanyId { get; set; }
    }
}