using System;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class EmployeeDepartment : BaseEntity, ITenant
    {
        public Guid EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
