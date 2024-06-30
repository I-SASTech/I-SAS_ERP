using System;

namespace Focus.Business.WarrantyTypes.Commands
{
    public class WarrantyTypeLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}
