using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Common
{
    public abstract class PagedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool NoPaging { get; set; }

    }
}