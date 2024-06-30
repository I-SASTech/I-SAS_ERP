using System;
using System.Collections.Generic;
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

namespace Focus.Business.MobileOrders.Queries.GetMobileOrderDetails
{
    public class GetMobileOrderDetailQuery:IRequest<MobileOrderLookupModel>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetMobileOrderDetailQuery, MobileOrderLookupModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetMobileOrderDetailQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<MobileOrderLookupModel> Handle(GetMobileOrderDetailQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var mobileOrder = await _context.MobileOrders
                               .AsNoTracking()
                                  .Include(x => x.MobileOrderItems)
                                  .ThenInclude(x => x.Product)
                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);


                    if (mobileOrder != null)
                    {
                        var poItems = new List<MobileOrderItemLookupModel>();
                        foreach (var item in mobileOrder.MobileOrderItems)
                        {
                            poItems.Add(new MobileOrderItemLookupModel
                            {
                                Id = item.Id,
                                MobileOrderId = item.MobileOrderId,
                                ProductId = item.ProductId,
                                Price = item.Price,
                                Quantity = item.Quantity,

                            });
                        }

                        return new MobileOrderLookupModel
                        {
                            Id = mobileOrder.Id,
                            OrderDate = mobileOrder.OrderDate,
                            OrderNo = mobileOrder.OrderNo,
                            CustomerId = mobileOrder.CustomerId,
                            Name = mobileOrder.Name,
                            MobileOrderItemLookupModels = poItems,
                        };
                    }
                    throw new NotFoundException(nameof(mobileOrder), request.Id);
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
