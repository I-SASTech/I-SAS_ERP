using System;
using System.Collections.Generic;
using Focus.Domain.Entities;

namespace Focus.Business.Colors.Queries.GetColorDetails
{
    public class ColorDetailLookUpModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; }
        public List<VariationInventory> VariationInventories { get; set; }
        public decimal Quantity { get; set; }
    }
}
