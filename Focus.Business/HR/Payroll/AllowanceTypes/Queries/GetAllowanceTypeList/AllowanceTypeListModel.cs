using System.Collections.Generic;

namespace Focus.Business.HR.Payroll.AllowanceTypes.Queries.GetAllowanceTypeList
{
    public class AllowanceTypeListModel
    {
        public IList<AllowanceTypeLookUpModel> AllowanceTypes { get; set; }
    }
}
