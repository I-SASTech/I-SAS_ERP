using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class MediaType : BaseEntity, ITenant
    {
        public string Name { get; set; }
        public virtual ICollection<Inquiry> Inquiries { get; set; }
    }
}
