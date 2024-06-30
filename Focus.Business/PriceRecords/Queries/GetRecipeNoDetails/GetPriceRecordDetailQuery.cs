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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PriceRecords.Queries.GetRecipeNoDetails
{
    public class GetPriceRecordDetailQuery : IRequest<PriceRecordLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetPriceRecordDetailQuery, PriceRecordLookupModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetPriceRecordDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<PriceRecordLookupModel> Handle(GetPriceRecordDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var priceRecord =  _context.PriceRecords.AsNoTracking()
                        .FirstOrDefault(x=>x.Id==request.Id);


                    if (priceRecord != null)
                    {

                      

                        return new PriceRecordLookupModel
                        {
                            Id = priceRecord.Id,
                            IsActive = priceRecord.IsActive,
                            Price=priceRecord.Price,
                            SalePrice = priceRecord.SalePrice,
                            PurchasePrice = priceRecord.PurchasePrice,
                            CostPrice = priceRecord.CostPrice,
                            PriceLabelingId = priceRecord.PriceLabelingId,
                            ProductId = priceRecord.ProductId,
                           
                        };
                    }
                    throw new NotFoundException(nameof(priceRecord), request.Id);
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
