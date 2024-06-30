using Focus.Domain.Interface;
using System;

namespace Focus.Domain.Entities
{
  public  class RecipeItem : BaseEntity, ISoftDelete, ITenant
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int HighQuantity { get; set; }
        public int UnitPerPack { get; set; }
        public string BasicUnit { get; set; }
        public string LevelOneUnit { get; set; }
        public int Quantity { get; set; }
        public decimal Waste { get; set; }
        public Guid RecipeNoId { get; set; }
        public virtual RecipeNo RecipeNo { get; set; }
        public Guid? WareHouseId { get; set; }
        public virtual Warehouse WareHouse { get; set; }

    }
}
