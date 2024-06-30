using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleDetails
{
    public class ModuleQuestionLookUp
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public ICollection<ModuleAnswerLookUp> AnswerLookUps { get; set; }
        public Guid InquiryModuleId { get; set; }
    }
}
