using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.InquiryRequestProcess.Queries.GetInquiryProcessDetails
{
    public class InquiryProcessDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }


    }
}
