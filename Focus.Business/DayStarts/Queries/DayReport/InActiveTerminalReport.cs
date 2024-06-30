using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.DayStarts.Queries.DayStartReportList;
using Focus.Business.DayStarts.Queries.GetOpeninigBalance;
using Focus.Business.DayStarts.Queries.IsDayStart;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DayStarts.Queries.DayReport
{
    public class InActiveTerminalReport : IRequest<List<DayStartLookUpModel>>
    {
        public DateTime? FromDay { get; set; }
        public DateTime? ToDay { get; set; }

        public class Handler : IRequestHandler<InActiveTerminalReport, List<DayStartLookUpModel>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;
            public readonly IMediator _Mediator;

            public Handler(IApplicationDbContext context, ILogger<InActiveTerminalReport> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _Mediator = mediator;
            }

            public async Task<List<DayStartLookUpModel>> Handle(InActiveTerminalReport request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = (request.FromDay == null && request.ToDay == null)?_context.DayStarts.AsNoTracking()
                        .Where(x => x.DayStartId != null && x.ToTime != null && x.Date.Value.Date >= DateTime.UtcNow.AddDays(-15))
                        .AsQueryable():
                        _context.DayStarts.AsNoTracking()
                            .Where(x => x.DayStartId != null && x.FromTime >= request.FromDay && x.FromTime <= request.ToDay)
                            .AsQueryable();

                    var dayList = new List<DayStartLookUpModel>();
                    var userTerminal = _context.Terminals.AsNoTracking().ToList();
                    foreach (var day in query)
                    {
                        var calculation = await _Mediator.Send(new GetOpeningBalanceQuery()
                        {
                            Id = day.CounterId,
                            DayId = day.Id,
                            IsReport = true
                        });

                        dayList.Add(new DayStartLookUpModel
                        {
                            Id = day.Id,
                            OpeningCash = day.OpeningCash,
                            CounterName = userTerminal.FirstOrDefault(x => x.Id == day.CounterId)?.Code,
                            Date = day.Date?.ToString("MM/dd/yyyy"),
                            ToTime = day.ToTime?.ToString("MM/dd/yyyy hh:mm tt"),
                            FromTime = day.FromTime?.ToString("MM/dd/yyyy hh:mm tt"),
                            CashInHand = day.CashInHand - day.OpeningCash,
                            ExpenseCash = calculation.DraftExpense + calculation.PostExpense,
                            DraftExpense = calculation.DraftExpense,
                            PostExpense = calculation.PostExpense,
                            BankExpense = calculation.BankExpense,
                            DayEndUser = day.DayEndUser,
                            CarryCash = day.CarryCash,
                            SuperVisorName = day.SuperVisorName,
                            StartTerminalBy = day.StartTerminalBy,
                            StartTerminalFor = day.StartTerminalFor,
                            TotalCash = (day.CashInHand - day.OpeningCash) + day.OpeningCash - calculation.DraftExpense,
                            IsReceived = day.IsReceived,
                            ReceivingAmount = day.ReceivingAmount,
                            BankDetails = calculation.BankDetails,
                            EndTerminalBy = day.EndTerminalBy,
                            EndTerminalFor = day.EndTerminalFor,
                            CounterId = day.CounterId
                        });
                    }


                    return dayList;
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
