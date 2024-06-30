using Focus.Business.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Focus.Business.PurchasePosts.Queries.GetPurchasePostDetail
{
    public class PurchaseReturnHistoryCounter : IRequest<int>
    {

        public Guid Id { get; set; }
        public bool IsReturnView { get; set; }
        public bool IsMultiUnit { get; set; }

        public class Handler : IRequestHandler<PurchaseReturnHistoryCounter, int>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PurchaseReturnHistoryCounter> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<int> Handle(PurchaseReturnHistoryCounter request, CancellationToken cancellationToken)
            {
                try
                {
                    var purchase = _context.PurchasePosts
                        .AsNoTracking().Count(x => x.PurchaseInvoiceId == request.Id);
                   
                    return purchase;

                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException(e.Message);
                }
            }


        }
    }
}