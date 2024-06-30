using System;

namespace Focus.Domain.Entities
{
    public class NoblePermission
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public virtual NobleModule NobleModules { get; set; }
        public Guid NobleModuleId { get; set; }
    }
}
