using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
    public class ModulesName : BaseEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ModulesRights> ModulesRights { get; set; }
    }
}
