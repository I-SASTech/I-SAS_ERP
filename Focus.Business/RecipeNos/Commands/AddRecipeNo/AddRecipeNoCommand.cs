using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Domain.Entities;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.RecipeNos.Commands.AddRecipeNo
{
    public class AddRecipeNoCommand : IRequest<Guid>
    {

        public RecipeNoLookupModel recipeNo { get; set; }
        public class Handler : IRequestHandler<AddRecipeNoCommand, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variable for log error
            private readonly ILogger _logger;
            private readonly IMediator _mediator;
            private string _code;

            public Handler(IApplicationDbContext context, ILogger<AddRecipeNoCommand> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddRecipeNoCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (request.recipeNo.Id == Guid.Empty)
                    {
                        var recipeNo = new RecipeNo()
                        {
                            RecipeName = request.recipeNo.RecipeName,
                            Date = request.recipeNo.Date,
                            ExpireDate = request.recipeNo.ExpireDate,
                            RegistrationNo = request.recipeNo.RegistrationNo,
                            ProductId = request.recipeNo.ProductId,
                            Quantity = request.recipeNo.Quantity,
                            Note = request.recipeNo.Note,
                            ApprovalStatus = request.recipeNo.ApprovalStatus,
                            IsClose = request.recipeNo.IsClose,
                            IsActive = request.recipeNo.IsActive,
                            //BranchId = request.recipeNo.BranchId,
                            RecipeItems = request.recipeNo.RecipeNoItems.Select(x =>
                                new RecipeItem()
                                {
                                    ProductId = x.ProductId,
                                    WareHouseId = x.WareHouseId,
                                    Quantity = x.Quantity,
                                    UnitPerPack = x.UnitPerPack,
                                    BasicUnit = x.BasicUnit,
                                    LevelOneUnit = x.LevelOneUnit,
                                    HighQuantity = x.HighQuantity,
                                    Waste = x.Waste,

                                }).ToList()
                        };

                        await _context.RecipeNos.AddAsync(recipeNo, cancellationToken);
                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                            DocumentNo = recipeNo.RegistrationNo,
                            //BranchId = recipeNo.BranchId
                        }, cancellationToken);

                        //Save changes to database
                        await _context.SaveChangesAsync(cancellationToken);

                        // Return Product id after successfully updating data
                        return recipeNo.Id;
                    }
                    else
                    {
                        var purchase = await _context.RecipeNos.FindAsync(request.recipeNo.Id);
                        if (purchase==null)
                        {
                            throw new NotFoundException("RecipeNos", "");
                        }

                        purchase.RecipeName = request.recipeNo.RecipeName;
                        purchase.Date = request.recipeNo.Date;
                        purchase.ExpireDate = request.recipeNo.ExpireDate;
                        purchase.RegistrationNo = request.recipeNo.RegistrationNo;
                        purchase.Note = request.recipeNo.Note;
                        purchase.ProductId = request.recipeNo.ProductId;
                        purchase.Quantity = request.recipeNo.Quantity;
                        purchase.ApprovalStatus = request.recipeNo.ApprovalStatus;
                        purchase.IsClose = request.recipeNo.IsClose;
                        purchase.IsActive = request.recipeNo.IsActive;

                        _context.RecipeItems.RemoveRange(purchase.RecipeItems);
                        purchase.RecipeItems = request.recipeNo.RecipeNoItems.Select(x =>
                                                       new RecipeItem()
                                                       {
                                                           ProductId = x.ProductId,
                                                           Quantity = x.Quantity,
                                                           WareHouseId = x.WareHouseId,
                                                           HighQuantity = x.HighQuantity,
                                                           Waste = x.Waste,
                                                           UnitPerPack = x.UnitPerPack,
                                                           BasicUnit = x.BasicUnit,
                                                           LevelOneUnit = x.LevelOneUnit,

                                                       }).ToList();

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            DocumentNo = purchase.RegistrationNo,
                            //BranchId = recipeNo.BranchId
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);
                        return purchase.Id;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }
        }
    }
}
