using System;

namespace Noble.Api.Models
{
    public class SizeVm
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
    }
}
