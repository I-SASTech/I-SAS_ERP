using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Focus.Business.Common;
using Focus.Business.HR.Payroll.PayrollSchedule.Commands;
using Focus.Business.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Focus.Business.HR.Payroll.PayrollSchedule.Queries
{
    public class PayrollScheduleListQuery : PagedRequest, IRequest<PagedResult<List<PayrollScheduleLookUpModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public int PageNumber { get; set; }
        public string SalaryType { get; set; }

        public class Handler : IRequestHandler<PayrollScheduleListQuery, PagedResult<List<PayrollScheduleLookUpModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<PayrollScheduleListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<PayrollScheduleLookUpModel>>> Handle(PayrollScheduleListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = _context.PaySchedules
                        .AsNoTracking()
                        .Where(x=>!x.Proceed)
                        .AsQueryable();

                    if (request.IsDropDown)
                    {
                        if (!string.IsNullOrEmpty(request.SalaryType))
                        {
                            query = request.SalaryType== "Hourly Based" ? query.Where(x => x.IsHourlyPay) : query.Where(x => !x.IsHourlyPay);
                        }
                        var deduction = new List<PayrollScheduleLookUpModel>();
                        foreach (var item in query)
                        {
                            deduction.Add(new PayrollScheduleLookUpModel()
                            {
                                Id = item.Id,
                                PayPeriod = item.PayPeriod,
                                PayPeriodStartDate = item.PayPeriodStartDate,
                                Name = item.Name,
                                PayPeriodEndDate = item.PayPeriodEndDate,
                                IfPayDayFallOnHoliday = item.IfPayDayFallOnHoliday,
                                PayDate = item.PayDate,
                                Default = item.Default,
                                IsHourlyPay = item.IsHourlyPay
                            });
                        }

                        return new PagedResult<List<PayrollScheduleLookUpModel>>
                        {
                            Results = deduction
                        };
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;


                        query = query.Where(x =>
                            x.Name.Contains(searchTerm));

                    }

                    var count = await query.CountAsync(cancellationToken: cancellationToken);
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize);

                    var list = new List<PayrollScheduleLookUpModel>();
                    foreach (var item in query)
                    {
                        list.Add(new PayrollScheduleLookUpModel()
                        {
                            Id = item.Id,
                            PayPeriod = item.PayPeriod,
                            PayPeriodStartDate = item.PayPeriodStartDate,
                            Name = item.Name,
                            PayPeriodEndDate = item.PayPeriodEndDate,
                            IfPayDayFallOnHoliday = item.IfPayDayFallOnHoliday,
                            PayDate = item.PayDate,
                            Default = item.Default,
                            IsHourlyPay = item.IsHourlyPay
                        });
                    }

                    return new PagedResult<List<PayrollScheduleLookUpModel>>
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
