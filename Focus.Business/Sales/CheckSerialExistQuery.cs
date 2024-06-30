using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.Sales
{
    public class CheckSerialExistQuery : IRequest<bool>
    {

        public string Serial { get; set; }
        public Guid ProductId { get; set; }
        public class Handler : IRequestHandler<CheckSerialExistQuery, bool>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<CheckSerialExistQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<bool> Handle(CheckSerialExistQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var inventories = _context.Inventories
                        .AsNoTracking()
                        .Where(x => x.Serial == request.Serial && x.ProductId == request.ProductId)
                        .AsQueryable();

                    var purchase = await inventories
                        .AnyAsync(x => x.TransactionType == TransactionType.PurchasePost || x.TransactionType == TransactionType.StockIn, cancellationToken: cancellationToken);
                    if (!purchase)
                    {
                        return false;
                    }
                    else
                    {
                        var purchaseReturn = await inventories
                            .AnyAsync(x => x.TransactionType == TransactionType.PurchaseReturn, cancellationToken: cancellationToken);
                        if (purchaseReturn)
                        {
                            return false;
                        }
                        else
                        {
                            var sale = await inventories
                                .AnyAsync(x => x.TransactionType == TransactionType.SaleInvoice || x.TransactionType == TransactionType.StockOut, cancellationToken: cancellationToken);
                            if (!sale)
                            {
                                return true;
                            }
                            else
                            {
                                var saleReturn = await inventories
                                    .AnyAsync(x => x.TransactionType == TransactionType.SaleReturn, cancellationToken: cancellationToken);
                                return saleReturn;
                            }
                        }
                    }

                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
