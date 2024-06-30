using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Domain.Entities
{
    public class GuestedHoliday : BaseEntity
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Guid? HolidayId { get; set; }
        public virtual Holiday Holiday { get; set; }

    }
}

