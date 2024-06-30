using AutoMapper;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.PaymentVouchers.Queries.GetAdvanceBalance
{
    public class GetAdvanceBalanceQuery : IRequest<AdvanceBalanceLookupModel>
    {
        public Guid? BranchId { get; set; }
        public Guid? ContactId { get; set; }
        public string FormName { get; set; }
        public class Handler : IRequestHandler<GetAdvanceBalanceQuery, AdvanceBalanceLookupModel>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMapper _mapper;

            public Handler(IApplicationDbContext context,
                ILogger<GetAdvanceBalanceQuery> logger,
                IMapper mapper)
            {
                _context = context;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<AdvanceBalanceLookupModel> Handle(GetAdvanceBalanceQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    if (request.FormName == "BankPay")
                    {
                        var accountId = await _context.Accounts.AsNoTracking().Include(x => x.Contacts).FirstOrDefaultAsync(x => x.Name == "Supplier Advance");

                        if (request.BranchId == null)
                        {
                            var transactions = _context.Transactions.Where(x => x.AccountId == accountId.Id && x.ContactId == request.ContactId).Sum(x => x.Debit - x.Credit);
                            return new AdvanceBalanceLookupModel
                            {
                                AccountId = accountId.Id,
                                AdvanceBalance = transactions,
                            };
                        }
                        else
                        {
                            var transactions = _context.Transactions.Where(x => x.AccountId == accountId.Id && x.ContactId == request.ContactId && x.BranchId == request.BranchId).Sum(x => x.Debit - x.Credit);

                            return new AdvanceBalanceLookupModel
                            {
                                AccountId = accountId.Id,
                                AdvanceBalance = transactions,
                            };
                        }
                    }
                    else
                    {
                        var accountId = await _context.Accounts.AsNoTracking().Include(x => x.Contacts).FirstOrDefaultAsync(x => x.Name == "Customers Advance");

                        if (request.BranchId == null)
                        {
                            var transactions = _context.Transactions.Where(x => x.AccountId == accountId.Id && x.ContactId == request.ContactId).Sum(x => x.Debit - x.Credit);
                            return new AdvanceBalanceLookupModel
                            {
                                AccountId = accountId.Id,
                                AdvanceBalance = transactions * -1,
                            };
                        }
                        else
                        {
                            var transactions = _context.Transactions.Where(x => x.AccountId == accountId.Id && x.ContactId == request.ContactId && x.BranchId == request.BranchId).Sum(x => x.Debit - x.Credit);

                            return new AdvanceBalanceLookupModel
                            {
                                AccountId = accountId.Id,
                                AdvanceBalance = transactions * -1,
                            };
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);

                    throw new ApplicationException("Unable to process your request please contact support");
                }

            }
        }
    }
}
