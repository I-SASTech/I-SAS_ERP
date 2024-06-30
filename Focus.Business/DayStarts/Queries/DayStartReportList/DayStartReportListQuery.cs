using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.DayStarts.Queries.DayStartReportList
{
    public class DayStartReportListQuery : IRequest<List<DayStartReportListLookUp>>
    {
        public class Handler : IRequestHandler<DayStartReportListQuery, List<DayStartReportListLookUp>>
        {
            public readonly IApplicationDbContext _context;
            public readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<DayStartReportListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }

            public async Task<List<DayStartReportListLookUp>> Handle(DayStartReportListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query =  _context.DayStarts.AsNoTracking()
                        .Where(x=> x.DayStartId != null && x.ToTime != null && x.Date.Value.Date >= DateTime.UtcNow.AddDays(-30))
                        .AsQueryable();

                    var dayList = new List<DayStartReportListLookUp>();

                    foreach (var day in query)
                    {
                        dayList.Add(new DayStartReportListLookUp()
                        {
                            Id = day.Id,
                            FromTime = day.FromTime?.ToString("MM/dd/yyyy hh:mm tt"),
                            ToTime = day.ToTime?.ToString("MM/dd/yyyy hh:mm tt"),
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
