using Focus.Domain.Enum;
using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class CompanyHolidays : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public HolidayType HolidayType { get; set; }
        public DateTime Date { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool PaidStatus { get; set; }
    }
}
