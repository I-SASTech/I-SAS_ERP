using System;
using System.Collections.Generic;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class Employee : BaseEntity, ITenant, IAuditedEntityBase, ITenantFilterableEntity
    {
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }
        public string Nic { get; set; }
        public string JobType { get; set; }
        public string SearchingKey { get; set; }
        public Guid? ParentId { get; set; }
        public virtual Employee Parent { get; set; }
        public virtual ICollection<Employee> Children { get; set; }
        public Guid? DesignationId { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual Guid? ZoneId { get; set; }
        public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
        public virtual ICollection<EmployeeAttachment> EmployeeAttachments { get; set; }
    
      
    }
}
