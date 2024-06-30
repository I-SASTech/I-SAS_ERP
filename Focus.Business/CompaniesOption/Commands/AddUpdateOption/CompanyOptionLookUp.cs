using System;

namespace Focus.Business.CompaniesOption.Commands.AddUpdateOption
{
    public class CompanyOptionLookUp
    {
        public Guid Id { get; set; }
        public Guid LocationId { get; set; }
        public string Label { get; set; }
        public bool Value { get; set; }
        public string OptionValue { get; set; }
    }
}
