using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Enum;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class Inquiry : BaseEntity, ITenant, ITenantFilterableEntity, IAuditedEntityBase
    {
        public string InquiryNumber { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime DueDateTime { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string TermAndCondition { get; set; }
        public string UserMessage { get; set; }
        public bool IsTerm { get; set; }
      
        public Guid? CustomerId { get; set; }
        public virtual Contact Contact { get; set; }
        public Guid? ReceiverId { get; set; }
        public Guid? HandlerId { get; set; }
        public Guid? ReferedBy { get; set; }
        public virtual EmployeeRegistration EmployeeRegistration { get; set; }
        public Guid? MediaTypeId { get; set; }
        public virtual MediaType MediaType { get; set; }
        public Guid? InquiryPriorityId { get; set; }
        public virtual InquiryPriority InquiryPriority { get; set; }
        public Guid? InquiryStatusDynamicId { get; set; }
        public virtual InquiryStatusDynamic InquiryStatusDynamic { get; set; }
        public Guid? InquiryProcessId { get; set; }
        public virtual InquiryProcess InquiryProcess { get; set; }
        public Guid? InquiryTypeId { get; set; }
        public virtual InquiryType InquiryType { get; set; }
        public virtual ICollection<InquiryItem> InquiryItems { get; set; }
        public virtual ICollection<InquiryModuleQuestion> InquiryModuleQuestions { get; set; }
        public virtual ICollection<InquiryComment> InquiryComments { get; set; }
        public virtual ICollection<InquiryEmail> InquiryEmails { get; set; }
        public virtual ICollection<InquiryMeeting> InquiryMeetings { get; set; }
        public virtual ICollection<InquiryStatus> InquiryStatus { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? BranchId { get; set; }
    }
}
