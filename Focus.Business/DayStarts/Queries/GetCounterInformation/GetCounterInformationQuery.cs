using Focus.Business.Exceptions;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Extensions;
using Focus.Domain.Interface;

namespace Focus.Business.DayStarts.Queries.GetCounterInformation
{
    public class GetCounterInformationQuery : IRequest<CounterInformationLookupModel>
    {
        public Guid? UserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public bool IsDayStart { get; set; }
        public bool IsDayEnd { get; set; }

        public class Handler : IRequestHandler<GetCounterInformationQuery, CounterInformationLookupModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private readonly IPrincipal _principal;
            private readonly IUserHttpContextProvider _contextProvider;
            public Handler(IApplicationDbContext context,
                ILogger<GetCounterInformationQuery> logger,
                IPrincipal principal, IUserHttpContextProvider contextProvider)
            {
                _context = context;
                _logger = logger;
                _principal = principal;
                _contextProvider = contextProvider;
            }
            public async Task<CounterInformationLookupModel> Handle(GetCounterInformationQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var userTerminal = await _context.UserTerminals
                            .Include(x => x.Terminal)
                            .Where(x => x.UserId == request.UserId)
                            .LastOrDefaultAsync(cancellationToken);

                    if (userTerminal == null)
                        throw new NotFoundException("User Terminal", request.UserId);

                    if (_principal.Identity.UserId() == request.UserId.ToString())
                    {
                        if (request.IsDayStart)
                        {
                            var userEmail = _principal.Identity.Email();
                            var userId = _principal.Identity.UserId();

                            var userDetail = await _context.UserTerminals
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.UserId == Guid.Parse(userId), cancellationToken);

                            var query = _context.DayStarts
                                .Where(x => x.DayStartUser == userEmail &&
                                                         x.CounterId == userDetail.TerminalId).AsQueryable();

                            if (!query.Any())
                            {
                                return new CounterInformationLookupModel
                                {
                                    CounterCode = userTerminal.Terminal.Code,
                                    CounterId = userTerminal.Terminal.Id,
                                    OpeningCash = 0M
                                };
                            }

                            var dayStart = query.LastOrDefault();

                            if ((dayStart != null && dayStart.ToTime is { }))
                            {
                                _context.DisableTenantFilter = true;

                                var cashInHandAccount = _context.Transactions.AsNoTracking()
                                    .Include(x => x.Account)
                                    .Where(x => x.Account.Code == "10100001" &&
                                                EF.Property<Guid>(x, "DayId") == dayStart.Id &&
                                                EF.Property<Guid>(x, "CounterId") == dayStart.CounterId).Sum(x => x.Debit - x.Credit);

                                var expenseAccount = _context.DailyExpenses.AsNoTracking()
                                .Include(x => x.DailyExpenseDetails)
                                .Where(x => EF.Property<Guid>(x, "DayId") == dayStart.Id &&
                                            EF.Property<Guid>(x, "CounterId") == dayStart.CounterId)
                                .Sum(x => x.DailyExpenseDetails.Sum(y => y.Amount));

                                var cashInHand = (cashInHandAccount + dayStart.OpeningCash) - expenseAccount;

                                _context.DisableTenantFilter = false;

                                return new CounterInformationLookupModel
                                {
                                    CounterCode = userTerminal.Terminal.Code,
                                    CounterId = userTerminal.Terminal.Id,
                                    OpeningCash = cashInHand > 0 ? cashInHand : cashInHand * -1
                                };
                            }
                        }

                        if (request.IsDayEnd)
                        {
                            var dayId = _contextProvider.GetDayId();

                            if(dayId == Guid.Empty)
                            {
                                return new CounterInformationLookupModel
                                {
                                    CounterCode = userTerminal.Terminal.Code,
                                    CounterId = userTerminal.Terminal.Id,
                                };
                            }

                            var dayStart = await _context.DayStarts
                                .OrderBy(x => x.Id == dayId).LastOrDefaultAsync(cancellationToken);

                            _context.DisableTenantFilter = true;

                            var cashInHand = _context.Transactions.AsNoTracking()
                                .Include(x => x.Account)
                                .Where(x => x.Account.Code == "10100001" &&
                                            EF.Property<Guid>(x, "DayId") == dayId &&
                                            EF.Property<Guid>(x, "CounterId") == dayStart.CounterId).Sum(x => x.Debit - x.Credit);

                            var bankAccount = _context.Transactions.AsNoTracking()
                                .Include(x => x.Account)
                                .Where(x => x.Account.Code == "10500001" &&
                                            EF.Property<Guid>(x, "DayId") == dayId &&
                                            EF.Property<Guid>(x, "CounterId") == dayStart.CounterId).Sum(x => x.Debit - x.Credit);

                            var expenseAccount = _context.DailyExpenses.AsNoTracking()
                                .Include(x => x.DailyExpenseDetails)
                                .Where(x => EF.Property<Guid>(x, "DayId") == dayId &&
                                            EF.Property<Guid>(x, "CounterId") == dayStart.CounterId)
                                .Sum(x => x.DailyExpenseDetails.Sum(y => y.Amount));

                            _context.DisableTenantFilter = false;

                            return new CounterInformationLookupModel
                            {
                                CounterCode = userTerminal.Terminal.Code,
                                CounterId = userTerminal.Terminal.Id,
                                CashInHand = cashInHand > 0 ? cashInHand : cashInHand * -1,
                                Bank = bankAccount > 0 ? bankAccount : bankAccount * -1,
                                OpeningCash = dayStart.OpeningCash > 0 ? dayStart.OpeningCash : dayStart.OpeningCash * -1,
                                Expense = expenseAccount > 0 ? expenseAccount : expenseAccount * -1
                            };
                        }
                    }

                    return new CounterInformationLookupModel();
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                    throw new NotFoundException("Day Start", request.UserId);
                }
                finally
                {
                    _context.DisableTenantFilter = false;
                }
            }
        }
    }
}
