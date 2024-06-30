using FluentValidation;

namespace Focus.Business.ProductionBatchs.Commands.AddProductionBatch
{
    public class AddProductionBatchCommandValidator:AbstractValidator<AddProductionBatchCommand>
    {
        public AddProductionBatchCommandValidator()
        {
            RuleFor(x => x.ProductionBatch.ProductionBatchItems).NotEmpty();
            RuleFor(x => x.ProductionBatch.Date).NotEmpty();
            RuleFor(x => x.ProductionBatch.RecipeNoId).NotEmpty();
            RuleFor(x => x.ProductionBatch.RegistrationNo).NotEmpty();
        }
    }
}
