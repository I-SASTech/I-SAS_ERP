using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeavePeriod.Model;
using Focus.Business.LeaveManagement.LeavePeriod.Queries;
using Focus.Business.LeaveManagement.LeaveType.Quries;
using Focus.Business.LeaveManagement.WorkWeek.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.WorkWeek.Queries
{
    public class WorkWeekListQuery : PagedRequest, IRequest<PagedResult<List<WorkWeekLookupModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public class Handler : IRequestHandler<WorkWeekListQuery, PagedResult<List<WorkWeekLookupModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetLeaveTypeListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<WorkWeekLookupModel>>> Handle(WorkWeekListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.WorkWeeks.ToListAsync();

                    if (request.IsDropDown)
                    {
                        var leave = new List<WorkWeekLookupModel>();
                        foreach (var item in query)
                        {
                            leave.Add(new WorkWeekLookupModel
                            {
                                Id = item.Id,
                                Day = item.Day,
                                Status = item.Status,
                                Country = item.Country,
                            });
                        }

                        return new PagedResult<List<WorkWeekLookupModel>>
                        {
                            Results = leave
                        };
                    }
                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.Country.Contains(searchTerm)).ToList();
                    }

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                    var list = new List<WorkWeekLookupModel>();
                    foreach (var item in query)
                    {
                        list.Add(new WorkWeekLookupModel
                        {
                            Id = item.Id,
                            Day = item.Day,
                            Status = item.Status,
                            Country = item.Country,
                        });
                    }

                    return new PagedResult<List<WorkWeekLookupModel>>
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
