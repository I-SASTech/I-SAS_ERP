using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class DeliveryHoliday : BaseEntity
    {
        public Guid? DeliveryAddressId { get; set; }
        public virtual DeliveryAddress DeliveryAddress { get; set; }
        public Guid? WeekHolidayId { get; set; }
        public virtual WeekHoliday WeekHoliday { get; set; }
    }
}
