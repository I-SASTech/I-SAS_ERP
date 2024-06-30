using Focus.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
   public class WeekHoliday : BaseEntity
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public Guid? HolidayId { get; set; }
        public virtual Holiday Holiday { get; set; }
        public virtual ICollection<DeliveryHoliday> DeliveryHolidays { get; set; }
        public Guid? BranchId { get; set; }


    }
}

