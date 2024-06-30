using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
    public class ModulesRights : BaseEntity
    {
        public Guid ModuleId { get; set; }
        public virtual ModulesName ModulesNames { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
