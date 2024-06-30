using Focus.Business.RecipeNos.Queries.NetAmount;
using System;
using System.Linq;

namespace Focus.Business.RecipeNos.Queries.GetRecipeNoList
{
    public class RecipeNoLookUpModel 
    {
        public Guid Id { get; set; }
        public string RecipeName { get; set; }
        public string RegistrationNumber { get; set; }
        public string ProductName { get; set; }
        public decimal NetAmount { get; set; }
        public bool IsInvoice { get; set; }
        public bool IsActive { get; set; }
        public string Date { get; set; }
        public int Quantity { get; set; }

        public static RecipeNoLookUpModel Create(Domain.Entities.RecipeNo recipeNo)
        {
            var netAmount = new TotalAmount();
            var lookUpModel = new RecipeNoLookUpModel
            {
                Id = recipeNo.Id,
                RecipeName = recipeNo.RecipeName,
                RegistrationNumber=recipeNo.RegistrationNo,
                Quantity = recipeNo.Quantity,
                IsActive = recipeNo.IsActive,
                ProductName = recipeNo.Product?.EnglishName,
                NetAmount = netAmount.CalculateTotalAmount(recipeNo.RecipeItems.ToList()),
                Date = recipeNo.Date.ToString("MM/dd/yyyy hh:mm tt")
            };
            return lookUpModel;
        }
    }
}
