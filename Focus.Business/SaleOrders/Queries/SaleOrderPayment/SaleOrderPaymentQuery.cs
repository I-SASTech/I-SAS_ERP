using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.SaleOrders.Queries.SaleOrderPayment
{
    public class SaleOrderPaymentQuery : IRequest<List<Domain.Entities.PurchaseOrderPayment>>
    {
        public Guid Id { get; set; }
        public bool IsEmail { get; set; }

        public class Handler : IRequestHandler<SaleOrderPaymentQuery, List<Domain.Entities.PurchaseOrderPayment>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<SaleOrderPaymentQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<Domain.Entities.PurchaseOrderPayment>> Handle(SaleOrderPaymentQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.IsEmail)
                        _context.DisableTenantFilter = true;
                    var query = _context.SaleOrderPayments
                        .AsNoTracking()
                        .Where(x => x.SaleOrderId == request.Id)
                        .AsQueryable();

                    query = query.OrderBy(x => x.Id);
                    var payment = await query.Select(x =>
                        new Domain.Entities.PurchaseOrderPayment()
                        {
                            Id = x.Id,
                            VoucherNumber = x.VoucherNumber,
                            PaymentVoucherId = x.PaymentVoucherId,
                            Date = x.Date,
                            Narration = x.Narration,
                            PaymentMode = x.PaymentMode,
                            Amount = x.Amount,
                        }).ToListAsync(cancellationToken: cancellationToken);

                    return payment;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("List Error");
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }

    }
}
