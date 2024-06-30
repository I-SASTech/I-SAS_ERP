using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.NobleUserRole.Queries.GetNoblePermissionList
{
    public class NobleModulePermissionLookUp
    {
        public List<NobleModuleLookUp> NobleModuleLook { get; set; }
        public List<NoblePermissionLookUp> NoblePermissionLook { get; set; }
    }
}
