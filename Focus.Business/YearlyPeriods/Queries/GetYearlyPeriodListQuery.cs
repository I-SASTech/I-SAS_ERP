using Focus.Business.Interface;
using Focus.Business.YearlyPeriods.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Focus.Business.YearlyPeriods.Queries
{
    public class GetYearlyPeriodListQuery : IRequest<List<YearlyPeriodLookupModel>>
    {
        public class Handler : IRequestHandler<GetYearlyPeriodListQuery, List<YearlyPeriodLookupModel>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context,ILogger<GetYearlyPeriodListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<List<YearlyPeriodLookupModel>> Handle(GetYearlyPeriodListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var yearlyPeriods = await _context.YearlyPeriods.AsNoTracking().Select(x => new
                    {
                        Name = x.Name,
                    }).ToListAsync(cancellationToken: cancellationToken);

                    var yearsOfPeriods = new List<YearlyPeriodLookupModel>();

                    foreach (var item in yearlyPeriods)
                    {
                        yearsOfPeriods.Add(new YearlyPeriodLookupModel()
                        {
                            Name = item.Name,
                        });
                    }

                    return yearsOfPeriods;
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.Message);
                    throw new ApplicationException(exception.Message);
                }
            }
        }
    }
}
