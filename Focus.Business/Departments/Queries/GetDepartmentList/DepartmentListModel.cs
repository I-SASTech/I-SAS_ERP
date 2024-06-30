using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Departments.Queries.GetDepartmentList
{
    public class DepartmentListModel
    {
        public IList<DepartmentLookUpModel> Departments { get; set; }
    }
}
