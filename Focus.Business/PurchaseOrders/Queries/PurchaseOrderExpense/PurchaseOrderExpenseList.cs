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

namespace Focus.Business.PurchaseOrders.Queries.PurchaseOrderExpense
{
    public class PurchaseOrderExpenseList : IRequest<List<Domain.Entities.PurchaseOrderExpense>>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<PurchaseOrderExpenseList, List<Domain.Entities.PurchaseOrderExpense>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<PurchaseOrderExpenseList> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<Domain.Entities.PurchaseOrderExpense>> Handle(PurchaseOrderExpenseList request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.PurchaseOrderExpenses
                        .AsNoTracking()
                        .Where(x => x.PurchaseOrderId == request.Id)
                        .AsQueryable();


                    query = query.OrderBy(x => x.Id);

                    var payment = await query.Select(x =>
                        new Domain.Entities.PurchaseOrderExpense()
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
            }
        }
    }
}
