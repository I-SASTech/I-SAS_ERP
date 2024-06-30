using Focus.Domain.Enums;
using System;
using System.Collections.Generic;
using Focus.Domain.Enum;

namespace Focus.Business.RecipeNos
{
    public class RecipeNoLookupModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string RecipeName { get; set; }
        public string RegistrationNo { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }
        public virtual ICollection<RecipeNoItemLookupModel> RecipeNoItems { get; set; }
     

    }
}
