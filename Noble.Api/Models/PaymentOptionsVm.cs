using System;

namespace Noble.Api.Models
{
    public class PaymentOptionsVm
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; internal set; }
    }
}
