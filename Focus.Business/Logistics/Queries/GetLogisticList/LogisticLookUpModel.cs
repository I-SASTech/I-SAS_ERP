using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Logistics.Queries.GetLogisticList
{
    public class LogisticLookUpModel : IMapFrom<Logistic>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string LicenseNo { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string TermsAndCondition { get; set; }
        public string Xcoordinates { get; set; }
        public string Ycoordinates { get; set; }
        public SeaPort Ports { get; set; }
        public string ServiceFor { get; set; }
        public Guid? ClearanceAgent { get; set; }
        public virtual Account Account { get; set; }
        public Domain.Enum.Logistics LogisticsForm { get; set; }
        public bool IsActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Logistic, LogisticLookUpModel>();
        }
    }
}
