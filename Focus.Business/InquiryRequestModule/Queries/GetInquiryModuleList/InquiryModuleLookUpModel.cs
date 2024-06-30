using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleDetails;

namespace Focus.Business.InquiryRequestModule.Queries.GetInquiryModuleList
{
    public class InquiryModuleLookUpModel 
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool Compulsory { get; set; }
        public int RowNumber { get; set; }
        public IList<ModuleQuestionLookUp> ModuleQuestionLookUps { get; set; }
        public IList<Attachment> Attachments { get; set; }

        public bool AttachmentCompulsory { get; set; }

        public bool IsEnable { get; set; }
        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<InquiryModule, InquiryModuleLookUpModel>();
        //}
    }
}
