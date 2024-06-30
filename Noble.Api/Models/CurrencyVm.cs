using System;

namespace Noble.Api.Models
{
    public class CurrencyVm
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Sign { get; set; }
        public string ArabicSign { get; set; }
        public bool IsActive { get; set; }
        public bool Setup { get; set; }
        public string Image { get; set; }
    }
}
