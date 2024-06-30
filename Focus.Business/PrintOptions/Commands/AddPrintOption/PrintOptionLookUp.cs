using System;

namespace Focus.Business.PrintOptions.Commands.AddPrintOption
{
    public class PrintOptionLookUp
    {
        public Guid Id { get; set; }
        public Guid? PrintSettingId { get; set; }
        public string Label { get; set; }
        public string LabelNameArabic { get; set; }
        public bool Value { get; set; }
        public string Type { get; set; }
        public Guid? BranchId { get; set; }
    }
}
