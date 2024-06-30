﻿using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
    public class SubCategory : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }

        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
        public virtual Category Category { get; set; }
        public Guid CategoryId { get; set; }
       
    }
}
