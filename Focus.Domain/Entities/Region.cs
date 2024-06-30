using Focus.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Focus.Domain.Entities
{
  public class Region : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string CountryId { get; set; }
        public string StateId { get; set; }
        public Guid CityId { get; set; }
        public virtual City City { get; set; }
        public string Area { get; set; }
        public bool isActive { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }


    }
}
