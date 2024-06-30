using System;

namespace Focus.Business.HR.Payroll.SalaryTemplates.Queries
{
    public class SalaryTemplateListLookUpModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string StructureName { get; set; }
    }
}
