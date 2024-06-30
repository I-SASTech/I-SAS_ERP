using Focus.Domain.Enum;
using System;

namespace Focus.Business.CompHoliday.Models
{
    public class CompanyHolidaysLookupModel
    {
        public Guid Id { get; set; }
        public HolidayType HolidayType { get; set; }
        public DateTime Date { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool PaidStatus { get; set; }
    }
}
