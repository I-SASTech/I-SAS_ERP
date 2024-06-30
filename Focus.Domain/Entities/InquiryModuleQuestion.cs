using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class InquiryModuleQuestion:BaseEntity, ITenant
    {
        public Guid InquiryId { get; set; }
        public virtual Inquiry Inquiry { get; set; }
        public Guid InquiryModuleId { get; set; }
        public virtual InquiryModule InquiryModule { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string Answer5 { get; set; }
        public string Answer6 { get; set; }
        public string Answer7 { get; set; }
        public string Answer8 { get; set; }
        public string Answer9 { get; set; }
        public string Description { get; set; }
    }
}
