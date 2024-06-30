using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using Focus.Domain.Entities;

namespace Focus.Business.InventoryBlinds.Queries.GetInventoryBlindDetail
{
    public class InventoryBlindDetailModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int StockLevel { get; set; }
        public int PhysicalQuantity { get; set; }
        public string Remarks { get; set; }
        public Guid InventoryBlindId { get; set; }
        public string EnglishName { get; set; }
        public string ArabicName { get; set; }
        public string Shelf { get; set; }
        public Category Category { get; set; }
        public Inventory Inventory { get; set; }
    }

    public class Category
    {
        public string Name { get; set; }
        public string NameArabic { get; set; }
    }
    public class Inventory
    {
        public int CurrentQuantity { get; set; }
        public int PhysicalQuantity { get; set; }
    }
}
