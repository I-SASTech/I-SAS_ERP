using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.InquiryComments.Queries.GetInquiryCommentDetails
{
    public class InquiryCommentDetailsLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public Guid? ReplyCommentedId { get; set; }
        public Guid InquiryId { get; set; }
       
        public DateTime DateTime { get; set; }


    }
}
