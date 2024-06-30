using System;
using System.Collections.Generic;

namespace Focus.Business.ManualAttendance.Queries
{
  public  class DateLookUpModel
    {
        public DateTime? Date { get; set; }
        public virtual List<ManualAttendenceLookUpModel> ManualAttendenceLookUpModel { get; set; }

    }
}
