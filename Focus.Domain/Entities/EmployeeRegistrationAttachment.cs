using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
    public class EmployeeRegistrationAttachment : BaseEntity, ITenant
    {
        public string Photo { get; set; }
        public string CNIC { get; set; }
        public string Passport { get; set; }
        public string DrivingLicense { get; set; }
        public DateTime Date { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual EmployeeRegistration Employee { get; set; }
    }
}
