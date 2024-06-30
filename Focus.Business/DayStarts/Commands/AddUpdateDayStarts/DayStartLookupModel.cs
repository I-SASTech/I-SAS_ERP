using System;

namespace Focus.Business.DayStarts.Commands.AddUpdateDayStarts
{
    public class DayStartLookupModel
    {
        public Guid DayId { get; set; }
        public object Token { get; set; }
        public bool IsLoginFail { get; set; }
        public bool HasPermission { get; set; } = true;
        public bool IsTerminal { get; set; } = true;
        public bool IsExpenseDay { get; set; }
        public string PrinterName { get; set; }

    }
}
