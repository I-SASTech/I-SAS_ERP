using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Enums;

namespace Focus.Business.RecipeNos.Queries.GetRecipeNoDetails
{
    public class RecipeNoDetailLookUpModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string RegistrationNo { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public bool IsClose { get; set; }
        public string RecipeName { get; set; }
        public string EnglishName { get; set; }

        public virtual ICollection<RecipeNoItemLookupModel> RecipeNoItems { get; set; }
        public static Expression<Func<RecipeNo, RecipeNoDetailLookUpModel>> Projection
        {
            get
            {
                return department => new RecipeNoDetailLookUpModel
                {
                    Id = department.Id,
                    Date = department.Date,
                    RecipeName = department.RecipeName,
                    RegistrationNo = department.RegistrationNo,
                    ProductId = department.ProductId,
                    EnglishName = department.Product.EnglishName,
                    Note = department.Note,
                    Quantity = department.Quantity,
                    IsActive = department.IsActive,
                    IsClose = department.IsClose,
                   
             
                    //RecipeNoItems = department.RecipeNoItems,

                };
            }
        }


        public static RecipeNoDetailLookUpModel Create(RecipeNo recipeNo)
        {
            return Projection.Compile().Invoke(recipeNo);
        }
    }
}
