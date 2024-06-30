using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Business.Sales.Queries.GetSaleDetail;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.RecipeNos.Queries.GetRecipeNoDetails
{
    public class GetRecipeNoDetailQuery : IRequest<RecipeNoDetailLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetRecipeNoDetailQuery, RecipeNoDetailLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetRecipeNoDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<RecipeNoDetailLookUpModel> Handle(GetRecipeNoDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var recipeNo = await _context.RecipeNos.FindAsync(request.Id);


                    if (recipeNo != null)
                    {

                        var poItems = new List<RecipeNoItemLookupModel>();
                        foreach (var item in recipeNo.RecipeItems)
                        {
                            poItems.Add(new RecipeNoItemLookupModel
                            {
                                Id = item.Id,
                                ProductId = item.ProductId,
                                Quantity = item.Quantity,
                                HighQuantity = item.HighQuantity,
                                Waste = item.Waste,
                                WareHouseId = item.WareHouseId,
                                UnitPerPack = item.UnitPerPack,
                                BasicUnit = item.BasicUnit,
                                LevelOneUnit = item.LevelOneUnit,
                                Product = new ProductDropdownLookUpModel
                                {
                                    Id = item.Product.Id,
                                    BarCode = item.Product.BarCode,
                                    Code = item.Product.Code,
                                    EnglishName = item.Product.EnglishName,
                                    ArabicName = item.Product.ArabicName,
                                    Inventory = (item.Product.Inventories == null || item.Product.Inventories.Count == 0)
                                        ? null
                                        : new Inventory()
                                        {
                                            CurrentQuantity = item.Product.Inventories.OrderBy(x => x.ProductId == item.Product.Id).LastOrDefault().CurrentQuantity,
                                        },


                                }
                            });
                        }

                        return new RecipeNoDetailLookUpModel
                        {
                            Id = recipeNo.Id,
                            RecipeName = recipeNo.RecipeName,
                            Date = recipeNo.Date,
                            ExpireDate = recipeNo.ExpireDate,
                            IsClose = recipeNo.IsClose,
                            IsActive = recipeNo.IsActive,
                            ApprovalStatus = recipeNo.ApprovalStatus,
                            RegistrationNo = recipeNo.RegistrationNo,
                            ProductId = recipeNo.ProductId,
                            EnglishName = recipeNo.Product.EnglishName,
                            Quantity = recipeNo.Quantity,
                            Note = recipeNo.Note,


                            RecipeNoItems = poItems,
                        };
                    }
                    throw new NotFoundException(nameof(recipeNo), request.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw new ApplicationException("Unable to process your request please contact support");
                }
            }
        }
    }
}
