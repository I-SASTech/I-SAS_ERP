using System;

namespace Focus.Business.EmployeeRegistrations.Queries.GetEmployeeAttachments
{
    public class EmployeeAttachmentLookupModel
    {
        public string Photo { get; set; }
        public string CNIC { get; set; }
        public string Passport { get; set; }
        public string DrivingLicense { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime? Date { get; set; }
    }
}
