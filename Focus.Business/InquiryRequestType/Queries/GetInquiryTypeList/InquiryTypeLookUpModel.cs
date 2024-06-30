using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.InquiryRequestType.Queries.GetInquiryTypeList
{
    public class InquiryTypeLookUpModel : IMapFrom<InquiryType>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<InquiryType, InquiryTypeLookUpModel>();
        }
    }
}
