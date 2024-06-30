using FluentValidation;

namespace Focus.Business.WareHouseTransfers.Commands.AddWareHouseTransfer
{
    public class AddWareHouseTransferCommandValidator:AbstractValidator<AddWareHouseTransferCommand>
    {
        public AddWareHouseTransferCommandValidator()
        {
            RuleFor(x => x.WareHouseTransfer.WareHouseTransferItems).NotEmpty();
            RuleFor(x => x.WareHouseTransfer.FromWareHouseId).NotEmpty();
            RuleFor(x => x.WareHouseTransfer.ToWareHouseId).NotEmpty();
            RuleFor(x => x.WareHouseTransfer.Date).NotEmpty();
            RuleFor(x => x.WareHouseTransfer.Code).NotEmpty();
        }
    }
}
