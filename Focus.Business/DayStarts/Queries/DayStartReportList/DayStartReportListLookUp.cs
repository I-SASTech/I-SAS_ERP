using System;

namespace Focus.Business.DayStarts.Queries.DayStartReportList
{
    public class DayStartReportListLookUp
    {
        public Guid Id { get; set; }
        public Guid? CounterId { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
    }
}
