using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Focus.Business.Interface;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.PurchasePosts.Queries.PurchasePostExpenseList
{
    public class PurchasePostExpenseList : IRequest<List<PurchasePostExpense>>
    {
        public Guid Id { get; set; }
        public class Handler : IRequestHandler<PurchasePostExpenseList, List<PurchasePostExpense>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<PurchasePostExpenseList> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<List<PurchasePostExpense>> Handle(PurchasePostExpenseList request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.PurchasePostExpenses
                        .AsNoTracking()
                        .Where(x => x.PurchasePostId == request.Id)
                        .AsQueryable();


                    query = query.OrderBy(x => x.Id);

                    var payment = await query.Select(x =>
                        new PurchasePostExpense()
                        {
                            Id = x.Id,
                            Date = x.Date,
                            BankCashAccountId = x.BankCashAccountId,
                            ContactAccountId = x.ContactAccountId,
                            VatAccountId = x.VatAccountId,
                            TaxRateId = x.TaxRateId,
                            TaxMethod = x.TaxMethod,
                            VoucherNumber = x.VoucherNumber,
                            Narration = x.Narration,
                            ChequeNumber = x.ChequeNumber,
                            Amount = x.Amount,
                            PaymentMode = x.PaymentMode,
                            PaymentMethod = x.PaymentMethod
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
