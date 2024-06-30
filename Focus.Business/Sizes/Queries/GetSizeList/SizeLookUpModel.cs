using System;

namespace Focus.Business.Sizes.Queries.GetSizeList
{
    public class SizeLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
    }
}
