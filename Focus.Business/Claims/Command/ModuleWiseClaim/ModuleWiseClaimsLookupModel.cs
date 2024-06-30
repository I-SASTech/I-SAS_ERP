using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Claims.Command.ModuleWiseClaim
{
    public class ModuleWiseClaimsLookupModel
    {
        public string TokenName { get; set; }
        public string Token { get; set; }
        public Guid CompanyId { get; set; }
    }
}
