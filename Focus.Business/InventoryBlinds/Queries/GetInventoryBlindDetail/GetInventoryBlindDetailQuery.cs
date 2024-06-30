using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Categories.Queries.GetCategoryDetails;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.InventoryBlinds.Queries.GetInventoryBlindDetail
{
    public class GetInventoryBlindDetailQuery : IRequest<InventoryBlindModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetInventoryBlindDetailQuery, InventoryBlindModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetCategoryDetailsQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<InventoryBlindModel> Handle(GetInventoryBlindDetailQuery request, CancellationToken cancellationToken)
            {

                try
                {
                    var inventoryBlind = await _context.InventoryBlinds.FindAsync(request.Id);
                    if (inventoryBlind == null)
                        throw new NotFoundException("Inventory Blind details", "");

                    var inventoryBlindDetailList = new List<InventoryBlindDetailModel>();
                    foreach (var blindDetail in inventoryBlind.InventoryBlindDetails)
                    {
                        var category = new Category()
                        {
                            Name = blindDetail.Product.Category.Name,
                            NameArabic = blindDetail.Product.Category.NameArabic
                        };
                        var inventory = new Inventory()
                        {
                            CurrentQuantity = blindDetail.CurrentQuantity,
                            PhysicalQuantity = blindDetail.PhysicalQuantity
                        };
                        var inventoryBlindDetail = new InventoryBlindDetailModel()
                        {
                            Id = blindDetail.ProductId,
                            Inventory = inventory,
                            PhysicalQuantity = blindDetail.PhysicalQuantity,
                            Remarks = blindDetail.Remarks,
                            InventoryBlindId = inventoryBlind.Id,
                            EnglishName = blindDetail.Product.EnglishName,
                            ArabicName = blindDetail.Product.ArabicName,
                            Category = category,
                            Shelf = blindDetail.Product.Shelf
                        };
                        inventoryBlindDetailList.Add(inventoryBlindDetail);
                    }

                    return new InventoryBlindModel()
                    {
                        Id = inventoryBlind.Id,
                        DateTime = inventoryBlind.DateTime,
                        WarehouseId = inventoryBlind.WarehouseId,
                        DocumentId = inventoryBlind.DocumentId,
                        Note = inventoryBlind.Note,
                        IsCounted = inventoryBlind.IsCounted,
                        InventoryBlindDetailModels = inventoryBlindDetailList
                    };
                }
                catch (NotFoundException exception)
                {
                    _logger.LogError(exception.Message);
                    throw new NotFoundException(exception.Message, "");
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
