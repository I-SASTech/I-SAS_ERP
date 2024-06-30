using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.FinancialYears.Commands;
using Focus.Business.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Focus.Business.FinancialYears.Queries.GetCurrentYear
{
   public class GetCurrentYearQuery : IRequest<PeriodModel>
    {

        public string Month { get; set; }
        public class Handler : IRequestHandler<GetCurrentYearQuery, PeriodModel>
        {
            //Create variable of IApplicationDbContext for Add and update database
            public readonly IApplicationDbContext _context;

            //Create logger interface variale for log error
            public readonly ILogger _logger;


            //Constructor

            public Handler(IApplicationDbContext context, ILogger<GetCurrentYearQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<PeriodModel> Handle(GetCurrentYearQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var month = string.Empty;
                    var yearToList = new List<string>();
                    var latestYear = _context.YearlyPeriods
                        .OrderBy(x=>x.Name)
                        .LastOrDefault()
                        ?.Name;
                    if (string.IsNullOrEmpty(latestYear))
                    {
                        if (request.Month == "January" || string.IsNullOrEmpty(request.Month))
                        {
                            var year = DateTime.Now.Year;
                            yearToList.Add(year + "-" + year);
                            yearToList.Add((year + 1) + "-" + (year + 1));
                        }
                        else
                        {
                            var year = DateTime.Now.Year;
                            yearToList.Add(year + "-" + (year + 1));
                            yearToList.Add((year + 1) + "-" + (year + 2));
                        }

                    }
                    else
                    {
                        month = _context.CompanySubmissionPeriods.OrderByDescending(p => p.PeriodEnd).LastOrDefault()?.PeriodEnd.ToString("MMMM");
                        var lastYearConvert = int.Parse(latestYear.Split("-")[0]);
                        yearToList.Add((lastYearConvert + 1) + "-" + (lastYearConvert + 2));
                    }
                    var periods = _context
                        .CompanySubmissionPeriods
                        .AsEnumerable()
                        .OrderBy(x => x.Year).ThenBy(x => x.PeriodStart)
                        .GroupBy(x=>x.YearlyPeriodId)
                        .ToList();
                    //var data = new PeriodModel();
                    var data = new List<FinancialYearGroupByModel>();
                    foreach ( var period in periods)
                    {
                        var financial = new FinancialYearGroupByModel();
                        financial.CompanySubmissionPeriod = new List<CompanySubmissionVmodel>();
                        financial.CompanySubmissionPeriod.AddRange(period.Select(x => new CompanySubmissionVmodel()
                        {
                            PeriodName = x.PeriodName,
                            IsPeriodClosed = x.IsPeriodClosed,
                            Year = x.Year,
                            Id = x.Id,
                        }).ToList());
                        financial.Year = period.First().Year;
                        data.Add(financial);
                        
                    }
                    
                    return new PeriodModel
                    {
                        YearToList = yearToList,
                        FinancialYearList = data,
                        MonthName = month
                    };
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
