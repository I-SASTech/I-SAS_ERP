using Focus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Focus.Business.DayStarts.Queries.GetDayStartDetails
{
    public class DayStartDetailsLookUpModel
    {
        public string CounterCode { get; set; }
        public decimal OpeningCash { get; set; }
        public Guid CounterId { get; set; }
    }
}
