using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Exceptions;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.ProductionBatchs.Queries.GetProductionBatchDetails.ProductionStockTransfeDetail
{
   public class ProductionStockTransferDetailQuery : IRequest<ProductionStockTransferLookUpModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<ProductionStockTransferDetailQuery, ProductionStockTransferLookUpModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<ProductionStockTransferDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<ProductionStockTransferLookUpModel> Handle(ProductionStockTransferDetailQuery request, CancellationToken cancellationToken)
            {
                var designation = await _context.ProductionStockTransfers.AsNoTracking()
                          
                         .FirstOrDefaultAsync(x => x.ProductionBatchId == request.Id);
                var wareHouseList = _context.Warehouses.AsNoTracking().ToList();
                var productList = _context.Products.AsNoTracking().ToList();

                if (designation != null)
                {
                    return new ProductionStockTransferLookUpModel
                    {
                        Id=designation.Id,
                        Date = designation.Date,
                        Code = designation.Code,
                        DamageStock = designation.DamageStock,
                        UnitPrice = designation.UnitPrice,
                        RemainingWareHouse = designation.RemainingWareHouse,
                        RemainingWareHouseName=wareHouseList.FirstOrDefault(x=>x.Id==designation.RemainingWareHouse)?.Name,
                        DamageWareHouse = designation.DamageWareHouse,
                        DamageWareHouseName = wareHouseList.FirstOrDefault(x => x.Id == designation.DamageWareHouse)?.Name,
                        ProductName = productList.FirstOrDefault(x => x.Id == designation.ProductId)?.EnglishName,

                        ProductionBatchId = designation.ProductionBatchId,
                        ProductId = designation.ProductId,
                    }
                    ;
                }
                throw new NotFoundException(nameof(Designation), request.Id);
            }
        }
    }
}
