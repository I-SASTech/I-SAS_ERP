﻿using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class ItemViewSetup  : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Sequence { get; set; }
    }
}
