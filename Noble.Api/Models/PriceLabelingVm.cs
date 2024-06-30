using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class PriceLabelingVm
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
        public string NameArabic { get; set; }
    }
}
