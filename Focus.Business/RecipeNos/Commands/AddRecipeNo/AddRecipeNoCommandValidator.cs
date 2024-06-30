using FluentValidation;

namespace Focus.Business.RecipeNos.Commands.AddRecipeNo
{
    public class AddRecipeNoCommandValidator:AbstractValidator<AddRecipeNoCommand>
    {
        public AddRecipeNoCommandValidator()
        {
            RuleFor(x => x.recipeNo.RecipeNoItems).NotEmpty();
            RuleFor(x => x.recipeNo.Date).NotEmpty();
            RuleFor(x => x.recipeNo.ProductId).NotEmpty();
            RuleFor(x => x.recipeNo.Quantity).NotEmpty();
            RuleFor(x => x.recipeNo.RegistrationNo).NotEmpty();
        }
    }
}
