using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.RunPayrolls.Queries
{
    public class GetRunPayrollListQuery : PagedRequest, IRequest<PagedResult<List<RunPayrollListLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public int PageNumber { get; set; }

        public class Handler : IRequestHandler<GetRunPayrollListQuery, PagedResult<List<RunPayrollListLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetRunPayrollListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<RunPayrollListLookUpModel>>> Handle(GetRunPayrollListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.RunPayrolls
                        .AsNoTracking()
                        .Include(x => x.PaySchedule)
                        .Include(x => x.RunPayrollDetails)
                        .AsQueryable();


                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;


                        query = query.Where(x =>
                            x.PaySchedule.Name.Contains(searchTerm));

                    }

                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var list = new List<RunPayrollListLookUpModel>();
                    foreach (var item in query)
                    {
                        var total = (item.RunPayrollDetails.Sum(x => x.BaseSalary) + item.RunPayrollDetails.Sum(x => x.AllowanceAmount))
                                    - (item.RunPayrollDetails.Sum(x => x.DeductionAmount) + item.RunPayrollDetails.Sum(x => x.ContributionAmount) + item.RunPayrollDetails.Sum(x => x.TaxAmount) + item.RunPayrollDetails.Sum(x => x.LoanAmount));

                        decimal hourlyAmount = 0;
                        if (item.Hourly)
                        {
                            hourlyAmount = item.RunPayrollDetails.Sum(x => x.HourAmount);
                        }
                        list.Add(new RunPayrollListLookUpModel()
                        {
                            Id = item.Id,
                            PayPlan = item.PaySchedule.Name,
                            Status = item.Status,
                            PayPeriod = item.PaySchedule.PayPeriodStartDate.ToString("d") + " - " + item.PaySchedule.PayPeriodEndDate.ToString("d"),
                            PayDate = item.PaySchedule.PayDate.ToString("d"),
                            TotalEmployees = item.RunPayrollDetails.Count,
                            TaxAmount = item.RunPayrollDetails.Sum(x => x.TaxAmount),
                            NetSalary = total + hourlyAmount,
                        });
                    }

                    return new PagedResult<List<RunPayrollListLookUpModel>>
                    {
                        Results = list,
                        RowCount = count,
                        PageSize = request.PageSize,
                        CurrentPage = request.PageNumber,
                        PageCount = list.Count / request.PageSize
                    };





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
