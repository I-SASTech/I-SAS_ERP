using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
  public  class Holiday : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string Name { get; set; }
        public DateTime? InTime { get; set; }
        public DateTime? OutTime { get; set; }
        public string NameArabic { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Description { get; set; }
        public virtual ICollection<WeekHoliday> WeekHolidays { get; set; }
        public virtual ICollection<GuestedHoliday> GuestedHolidays { get; set; }



    }
}

