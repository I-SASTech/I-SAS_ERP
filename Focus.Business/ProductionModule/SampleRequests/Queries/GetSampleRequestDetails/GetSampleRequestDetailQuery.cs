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

namespace Focus.Business.ProductionModule.SampleRequests.Queries.GetSampleRequestDetails
{
    public class GetSampleRequestDetailQuery : IRequest<SampleRequestLookupModel>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<GetSampleRequestDetailQuery, SampleRequestLookupModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetSampleRequestDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<SampleRequestLookupModel> Handle(GetSampleRequestDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var sampleRequest = await _context.SampleRequests
                        .AsNoTracking()
                        .Include(x=>x.SampleRequestItems)
                        .ThenInclude(x=>x.Product)
                        .FirstOrDefaultAsync(x=>x.Id==request.Id,cancellationToken);
                    
                    return new SampleRequestLookupModel
                    {
                        Id = sampleRequest.Id,
                        Code = sampleRequest.Code,
                        NoOfSampleRequests = sampleRequest.NoOfSampleRequests,
                        CustomerId = sampleRequest.CustomerId,
                        ReferredBy = sampleRequest.ReferredBy,
                        RequestGenerated = sampleRequest.RequestGenerated,
                        Date = sampleRequest.Date,
                        DueDate = sampleRequest.DueDate,
                        ProductId = sampleRequest.ProductId,
                        ApprovalStatus = sampleRequest.ApprovalStatus,
                        SampleRequestItems = sampleRequest.SampleRequestItems.Select(x =>
                            new SampleRequestItemLookupModel()
                            {
                                Id = x.Id,
                                ProductId = x.ProductId,
                                Description = x.Description,
                                UnitPerPack = x.UnitPerPack,
                                Quantity = x.Quantity,
                                HighQuantity = x.HighQuantity,
                                Product = new ProductDropdownLookUpModel
                                {
                                    Id = x.Product.Id,
                                    BarCode = x.Product.BarCode,
                                    Code = x.Product.Code,
                                    EnglishName = x.Product.EnglishName,
                                    ArabicName = x.Product.ArabicName,
                                    Inventory = (x.Product.Inventories == null || x.Product.Inventories.Count == 0)
                                        ? null
                                        : new Inventory()
                                        {
                                            CurrentQuantity = x.Product.Inventories.OrderBy(x => x.ProductId == x.Product.Id).LastOrDefault().CurrentQuantity,
                                        },


                                }
                            }).ToList(),
                    };
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
