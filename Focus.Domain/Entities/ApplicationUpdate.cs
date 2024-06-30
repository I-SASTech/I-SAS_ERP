using System;
using System.Collections.Generic;
using System.Text;
using Focus.Domain.Interface;

namespace Focus.Domain.Entities
{
    public class ApplicationUpdate:BaseEntity, ITenant
    {
        public string Status { get; set; }
        public string Version { get; set; }
        public string Detail { get; set; }
        public DateTime DateTime { get; set; }
        public Guid? CounterId { get; set; }
    }
}
