using Focus.Business.DayStarts.Queries.GetCounterInformation;
using Focus.Business.Interface;
using Focus.Domain.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.DayStarts.Queries.GetCounterDetails
{
    public class GetCounterDetailsQuery : IRequest<List<CounterInformationLookupModel>>
    {
        public List<CounterLookupModel> Counters { get; set; }
        public class Handler : IRequestHandler<GetCounterDetailsQuery, List<CounterInformationLookupModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IPrincipal _principal;
            private readonly IUserHttpContextProvider _contextProvider;
            public Handler(IApplicationDbContext context,
                ILogger<GetCounterDetailsQuery> logger,
                IPrincipal principal, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger;
                _principal = principal;
                _contextProvider = contextProvider;
            }

            public async Task<List<CounterInformationLookupModel>> Handle(GetCounterDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var ids = request.Counters.Select(x => x.Id).ToList();

                    var counterList = new List<CounterInformationLookupModel>();

                    foreach (var terminalId in ids)
                    {
                        var userTerminal = await _context.UserTerminals
                          .Include(x => x.Terminal)
                          .Where(x => x.TerminalId == terminalId)
                          .LastOrDefaultAsync(cancellationToken);

                        if (userTerminal == null)
                            throw new ApplicationException("User Terminal");

                        //var dayId = _contextProvider.GetDayId();

                        //if (dayId == Guid.Empty)
                        //{
                        //    continue;
                        //}

                        var dayStart = await _context.DayStarts
                               .LastOrDefaultAsync(x => x.CounterId == userTerminal.TerminalId, cancellationToken);

                        _context.DisableTenantFilter = true;

                        var cashInHand = _context.Transactions.AsNoTracking()
                            .Include(x => x.Account)
                            .Where(x => x.Account.Code == "10100001" &&
                                        EF.Property<Guid>(x, "DayId") == dayStart.Id &&
                                        EF.Property<Guid>(x, "CounterId") == userTerminal.TerminalId).Sum(x => x.Debit - x.Credit);

                        var bankAccount = _context.Transactions.AsNoTracking()
                            .Include(x => x.Account)
                            .Where(x => x.Account.Code == "10500001" &&
                                        EF.Property<Guid>(x, "DayId") == dayStart.Id &&
                                        EF.Property<Guid>(x, "CounterId") == userTerminal.TerminalId).Sum(x => x.Debit - x.Credit);

                        var expenseAccount = _context.DailyExpenses.AsNoTracking()
                            .Include(x => x.DailyExpenseDetails)
                            .Where(x => EF.Property<Guid>(x, "DayId") == dayStart.Id &&
                                        EF.Property<Guid>(x, "CounterId") == userTerminal.TerminalId)
                            .Sum(x => x.DailyExpenseDetails.Sum(y => y.Amount));

                        _context.DisableTenantFilter = false;

                        counterList.Add(new CounterInformationLookupModel
                        {
                            Bank = bankAccount,
                            CashInHand = cashInHand,
                            CounterCode = userTerminal.Terminal.Code,
                            CounterId = terminalId,
                            Expense = expenseAccount,
                            OpeningCash = dayStart.OpeningCash
                        });
                    }

                    return counterList;
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new ApplicationException("Day Start");
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}
