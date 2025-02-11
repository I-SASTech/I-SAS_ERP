﻿using Focus.Business.Dashboard.Queries.GetInventoryList;
using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Common
{
   public class PagedResult<T>
    {
        public T Results { get; set; }
        public T ResultsWOPagination { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }
        public int ActiveBundle { get; set; }
    }
}
