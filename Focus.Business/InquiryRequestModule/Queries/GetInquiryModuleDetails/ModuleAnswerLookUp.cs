using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleDetails
{
    public class ModuleAnswerLookUp
    {
        public int Id { get; set; }
        public string Answer { get; set; }
        public bool Selected { get; set; }
        public string CustomAnswer { get; set; }
    }
}
