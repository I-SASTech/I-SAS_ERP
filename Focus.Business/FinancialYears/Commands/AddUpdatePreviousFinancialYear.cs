using Focus.Business.Interface;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Exceptions;
using Focus.Business.SyncRecords.Commands;

namespace Focus.Business.FinancialYears.Commands
{
    public class AddUpdatePreviousFinancialYear : IRequest<Guid>
    {
        public string Year { get; set; }
        public int Month { get; set; }

        public class Handler : IRequestHandler<AddUpdatePreviousFinancialYear, Guid>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            public Handler(IApplicationDbContext context, ILogger<AddUpdatePreviousFinancialYear> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdatePreviousFinancialYear request, CancellationToken cancellationToken)
            {
                try
                {
                    
                    for (int j = 0; j < 5; j++)
                    {
                        var latestYear = _context.YearlyPeriods.OrderBy(x => x.Name).FirstOrDefault()?.Name;
                        if (string.IsNullOrEmpty(latestYear))
                            throw new NotFoundException("Current year not found", "");



                        var month = _context.CompanySubmissionPeriods.OrderByDescending(p => p.PeriodEnd).LastOrDefault()
                            ?.PeriodEnd.ToString("MMMM");
                        var lastYearConvert = int.Parse(latestYear.Split("-")[0]);

                        request.Year = (lastYearConvert - 1) + "-" + (lastYearConvert - 1);
                        request.Month = month == "January"
                            ? 0
                            : Convert.ToInt32(_context.CompanySubmissionPeriods.OrderByDescending(p => p.PeriodEnd)
                                .LastOrDefault()?.PeriodName);





                        var lastIndex = request.Year.LastIndexOf('-');

                        if (lastIndex == 0) throw new ApplicationException("Invalid year passed");

                        var firstYear = request.Year.Substring(0, request.Year.IndexOf("-", StringComparison.Ordinal));
                        var companySubmissionPeriod = new List<CompanySubmissionPeriod>();
                        var yearlyPeriod = new YearlyPeriod
                        {
                            Name = firstYear + "-",
                        };
                        if (request.Month == 0)
                        {
                            yearlyPeriod.Name = yearlyPeriod.Name + firstYear;
                            //var yearlyPeriod = await _context.YearlyPeriods.AddAsync();
                        }
                        else
                        {
                            yearlyPeriod.Name = yearlyPeriod.Name + (Convert.ToInt32(firstYear) + 1);

                        }

                        await _context.YearlyPeriods.AddAsync(yearlyPeriod, cancellationToken);
                        for (var i = 1; i <= 12; i++)
                        {
                            var period = new CompanySubmissionPeriod()
                            {
                                Year = (i + request.Month) <= 12 ? firstYear : (Convert.ToInt32(firstYear) + 1).ToString(),
                                PeriodName = (i + request.Month) <= 12
                                    ? (PeriodName)i + request.Month
                                    : (PeriodName)((i + request.Month) - 12),
                                PeriodStart = (i + request.Month) <= 12
                                    ? DateTime.Parse($"{firstYear} {((PeriodName)i + request.Month).ToString()} 01")
                                    : DateTime.Parse(
                                        $"{(Convert.ToInt32(firstYear) + 1).ToString()} {((PeriodName)((i + request.Month) - 12)).ToString()} 01"),
                                PeriodEnd = (i + request.Month) <= 12
                                    ? DateTime.Parse($"{firstYear} {((PeriodName)i + request.Month).ToString()} 01")
                                        .AddMonths(1).AddDays(-1)
                                    : DateTime.Parse(
                                            $"{(Convert.ToInt32(firstYear) + 1).ToString()} {((PeriodName)((i + request.Month) - 12)).ToString()} 01")
                                        .AddMonths(1).AddDays(-1),
                                YearlyPeriodId = yearlyPeriod.Id
                            };
                            companySubmissionPeriod.Add(period);
                        }
                        Random rnd = new Random();
                        for (int i = 0; i < 11; i++)
                        {
                            _code += rnd.Next(0, 9).ToString();
                        }

                        await _context.CompanySubmissionPeriods.AddRangeAsync(companySubmissionPeriod, cancellationToken);

                        await _mediator.Send(new AddUpdateSyncRecordCommand()
                        {
                            SyncRecordModels = _context.SyncLog(),
                            Code = _code,
                        }, cancellationToken);

                        await _context.SaveChangesAsync(cancellationToken);

                    }

                    return Guid.NewGuid();
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException("Please Contact to support");
                }
            }
        }
    }
}
