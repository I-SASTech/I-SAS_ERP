using System;
using System.Collections.Generic;
using Focus.Business.InquiryRequest.Commands.AddUpdateInquiry;
using Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleList;

namespace Focus.Business.InquiryRequest.Queries.GetInquiryDetails
{
    public class InquiryDetailLookUpModel
    {
        public Guid Id { get; set; }
        public string InquiryNumber { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime DueDateTime { get; set; }
        public string Reference { get; set; }
        public string Description { get; set; }
        public string TermAndCondition { get; set; }
        public string UserMessage { get; set; }
        public bool IsTerm { get; set; }
        public string InquiryStatus { get; set; }
        public Guid? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string InquiryType { get; set; }
        public Guid? ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public Guid? HandlerId { get; set; }
        public Guid? ReferedBy { get; set; }
        public string HandlerName { get; set; }
        public Guid? MediaTypeId { get; set; }
        public string MediaType { get; set; }
        public Guid? InquiryProcessId { get; set; }
        public Guid? InquiryModuleId { get; set; }
        public Guid? InquiryTypeId { get; set; }
        public Guid? InquiryPriorityId { get; set; }
        public Guid? InquiryStatusDynamicId { get; set; }
        public int TotalInquiry { get; set; }
        public string TimeSpan { get; set; }
        public Guid? BranchId { get; set; }
        public ICollection<InquiryItemLookUpModel> InquiryItems { get; set; }
        public ICollection<InquiryAttachmentLookUpModel> AttachmentList { get; set; }
        public ICollection<InquiryModuleLookUpModel> InquiryModuleLookUp { get; set; }
        public ICollection<InquiryStatusLookUpModel> InquiryStatusLookUp { get; set; }


        //public static Expression<Func<Inquiry, InquiryDetailLookUpModel>> Projection
        //{
        //    get
        //    {
        //        return Inquiry => new InquiryDetailLookUpModel
        //        {
        //            Id = Inquiry.Id,
        //            Name = color.Name,
        //            NameArabic = color.NameArabic,
        //            Description = color.Description,
        //            Code = color.Code,
        //            isActive = color.isActive,

        //        };
        //    }
        //}

        //public static ColorDetailLookUpModel Create(Color color)
        //{
        //    return Projection.Compile().Invoke(color);
        //}
    }
}
