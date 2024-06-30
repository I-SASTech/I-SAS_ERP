using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus.Domain.Entities
{
    public class DeliveryAddress : BaseEntity
    {
        public Guid? ContactId { get; set; }
        public virtual Contact Contact { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }

        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string Langitutue { get; set; }
        public string Latitude { get; set; }
        public string GoogleLocation { get; set; }
        public string NearBy { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }

        public string Country { get; set; }
        public string BillingZipCode { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string BillingFax { get; set; }
        public bool IsOffice { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public bool AllDaySelection { get; set; }
        public bool AllHour { get; set; }
        public virtual ICollection<DeliveryHoliday> DeliveryHolidays { get; set; }

    }
}
