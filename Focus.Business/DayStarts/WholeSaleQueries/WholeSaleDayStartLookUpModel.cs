using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.DayStarts.WholeSaleQueries
{
    public class WholeSaleDayStartLookUpModel
    {
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public string CounterName { get; set; }
        public string FromTime { get; set; }
        public bool IsDayStart { get; set; }
        public Guid DayStartId { get; set; }
        public Guid CounterId { get; set; }
        public object Token { get; set; }

    }
}
