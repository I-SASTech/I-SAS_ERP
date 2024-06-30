using System;

namespace Focus.Domain.Entities
{
    public class RecipeAssortment:BaseEntity
    {
        public Guid RecipeNoId { get; set; }
        public virtual RecipeNo RecipeNo { get; set; }
        public string Assortment { get; set; }
        public string Value { get; set; }
    }
}
