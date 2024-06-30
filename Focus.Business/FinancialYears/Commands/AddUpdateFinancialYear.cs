using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using Focus.Business.Models;
using Focus.Business.SyncRecords.Commands;
using Focus.Domain.Entities;
using Focus.Domain.Enum;
using Focus.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.FinancialYears.Commands
{
    public class AddUpdateFinancialYear : IRequest<Guid>
    {
        public string Year { get; set; }
        public int Month { get; set; }
        public string MonthType { get; set; }

        public class Handler : IRequestHandler<AddUpdateFinancialYear, Guid>
        {
            //Create variable of IApplicationDbContext for Add and update database
            private readonly IApplicationDbContext _context;

            //Create logger interface variale for log error
            private readonly ILogger _logger;
            private string _code;
            private readonly IMediator _mediator;
            //Constructor

            public Handler(IApplicationDbContext context, ILogger<AddUpdateFinancialYear> logger, IMediator mediator)
            {
                _context = context;
                _logger = logger;
                _mediator = mediator;
            }

            public async Task<Guid> Handle(AddUpdateFinancialYear request, CancellationToken cancellationToken)
            {
                try
                {
                    var lastIndex = request.Year.LastIndexOf('-');

                    Random rnd = new Random();
                    for (int i = 0; i < 11; i++)
                    {
                        _code += rnd.Next(0, 9).ToString();
                    }

                    if (lastIndex == 0) throw new ApplicationException("Invalid year passed");

                    var firstYear = request.Year.Substring(0, request.Year.IndexOf("-", StringComparison.Ordinal));
                    var companySubmissionPeriod = new List<CompanySubmissionPeriod>();
                    var yearlyPeriod = new YearlyPeriod
                    {
                        Name = firstYear + "-",
                    };
                    yearlyPeriod.MonthType = request.MonthType;
                    if (request.Month == 0)
                    {
                        yearlyPeriod.Name = yearlyPeriod.Name + firstYear;
                        //var yearlyPeriod = await _context.YearlyPeriods.AddAsync();
                    }
                    else
                    {
                        yearlyPeriod.Name = yearlyPeriod.Name + (Convert.ToInt32(firstYear)+1);
                        
                    }
                    await _context.YearlyPeriods.AddAsync(yearlyPeriod,cancellationToken);
                    for (var i = 1; i <= 12; i++)
                    {
                        var period = new CompanySubmissionPeriod()
                        {
                            Year = (i + request.Month) <= 12 ? firstYear:(Convert.ToInt32(firstYear)+1).ToString(),
                            PeriodName = (i + request.Month) <= 12 ? (PeriodName)i + request.Month : (PeriodName)((i + request.Month) - 12),
                            PeriodStart = (i + request.Month) <= 12 ?
                                DateTime.Parse($"{firstYear} {((PeriodName)i + request.Month).ToString()} 01") :
                                DateTime.Parse($"{(Convert.ToInt32(firstYear) + 1).ToString()} {((PeriodName)((i + request.Month) - 12)).ToString()} 01"),
                            PeriodEnd = (i + request.Month) <= 12 ?
                                DateTime.Parse($"{firstYear} {((PeriodName)i + request.Month).ToString()} 01").AddMonths(1).AddDays(-1) :
                                DateTime.Parse($"{(Convert.ToInt32(firstYear)+1).ToString()} {((PeriodName)((i + request.Month) - 12)).ToString()} 01").AddMonths(1).AddDays(-1),
                            YearlyPeriodId = yearlyPeriod.Id
                        };
                        companySubmissionPeriod.Add(period);
                    }
                    await _context.CompanySubmissionPeriods.AddRangeAsync(companySubmissionPeriod, cancellationToken);


                    var log = _context.SyncLog();
                    var branches = await _context.Branches.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
                    var list = new List<SyncRecordModel>();
                    if (branches.Any())
                    {
                        foreach (var branch in branches)
                        {
                            var syncRecords = log.Select(x => new SyncRecordModel
                            {
                                Table = x.Table,
                                ColumnId = x.ColumnId,
                                CompanyId = x.CompanyId,
                                ColumnType = x.ColumnType,
                                Push = x.Push,
                                Pull = x.Pull,
                                Action = x.Action,
                                ChangeDate = DateTime.Now,
                                PullDate = x.PullDate,
                                PushDate = x.PushDate,
                                ColumnName = x.ColumnName,
                                BranchId = branch.Id,
                                Code = _code,
                            }).ToList();

                            list.AddRange(syncRecords);
                        }
                    }
                    else
                    {
                        var syncRecords = log.Select(x => new SyncRecordModel
                        {
                            Table = x.Table,
                            ColumnId = x.ColumnId,
                            CompanyId = x.CompanyId,
                            ColumnType = x.ColumnType,
                            Push = x.Push,
                            Pull = x.Pull,
                            Action = x.Action,
                            ChangeDate = DateTime.Now,
                            PullDate = x.PullDate,
                            PushDate = x.PushDate,
                            ColumnName = x.ColumnName,
                            //          BranchId = branch.Id,
                            Code = _code,
                        }).ToList();

                        list.AddRange(syncRecords);
                    }

                    await _mediator.Send(new AddUpdateSyncRecordCommand()
                    {
                        SyncRecordModels = list,
                        IsServer = true
                    }, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                    return companySubmissionPeriod.Select(x=>x.Id).FirstOrDefault();
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


