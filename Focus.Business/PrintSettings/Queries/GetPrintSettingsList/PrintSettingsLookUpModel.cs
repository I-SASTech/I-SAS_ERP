using System;
using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;

namespace Focus.Business.PrintSettings.Queries.GetPrintSettingsList
{
    public class PrintSettingLookUpModel:IMapFrom<PrintSetting>
    {
        public Guid Id { get; set; }
        public string PrintSize { get; set; }
        public decimal? ReturnDays { get; set; }
        public bool isActive { get; set; }
        public string TermsInAr { get; set; }
        public string TermsInEng { get; set; }
        public string PrinterName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PrintSetting, PrintSettingLookUpModel>();
        }
    }
}
