using System;
using System.Collections.Generic;
using Focus.Business.Accounting.Queries;
using Focus.Business.Models;
using Focus.Business.Permission.Commands.AddUpdateNoblePermission;

namespace Focus.Business.NoblePermission.Commands.AddUpdateNoblePermission
{
    public class GetAllPermissionModuleAndGroup
    {
        public Guid LocationId { get; set; }
        public GroupLookUp GroupLookUp { get; set; }
        public WhiteLabelLookUp WhiteLabelLookUp { get; set; }
        public List<ModuleLookUp> ModuleLookUps { get; set; }
        public List<PermissionsLookUp> PermissionsLookUps { get; set; }
        public List<CompanyLicenseLookUp> CompanyLicenseLookUps { get; set; }
        public AccountTemplateDto TemplateLists { get; set; }
    }
}
