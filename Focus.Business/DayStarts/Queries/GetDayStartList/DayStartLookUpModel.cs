using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Focus.Business.Common.Mappings;
using Focus.Domain.Entities;

namespace Focus.Business.DayStarts.Queries.GetDayStartList
{
    public class DayStartLookUpModel:IMapFrom<DayStart>
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public Guid SaleId { get; set; }
        public Guid CounterId { get; set; }
        public Guid LocationId { get; set; }
        public decimal OpeningCash { get; set; }
        public decimal VerifyCash { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public bool IsActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DayStart, DayStartLookUpModel>();
        }
    }
}
