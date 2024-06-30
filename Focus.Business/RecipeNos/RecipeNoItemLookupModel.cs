using Focus.Business.Sales.Queries.GetSaleDetail;
using System;

namespace Focus.Business.RecipeNos
{
    public class RecipeNoItemLookupModel
    {
        public Guid Id { get; set; }
        public int HighQuantity { get; set; }
        public int Quantity { get; set; }
        public decimal Waste { get; set; }
        public int UnitPerPack { get; set; }
        public string BasicUnit { get; set; }
        public string LevelOneUnit { get; set; }
        public decimal TotalWithWaste { get; set; }
        public Guid? WareHouseId { get; set; }
        public Guid ProductId { get; set; }
        public Guid RecipeNoId { get; set; }
        public ProductDropdownLookUpModel Product { get; set; }
    }
}
