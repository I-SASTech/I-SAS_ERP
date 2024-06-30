using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InquiryComments.Queries.GetInquiryCommentList
{
    public class InquiryCommentLookUpModel 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public Guid? ReplyCommentedId { get; set; }
        public Guid InquiryId { get; set; }
        public List<InquiryCommentLookUpModel> CommentedData { get; set; }

        public DateTime DateTime { get; set; }
        public string TimeSpan { get; set; }
        public bool IsReply { get; set; }
        public bool IsViewReply { get; set; }
    }
}
