using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.Holiday.Model;
using Focus.Business.LeaveManagement.LeaveType.Quries;
using Focus.Business.LeaveManagement.WorkWeek.Model;
using Focus.Business.LeaveManagement.WorkWeek.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.Holiday.Queries
{
    public class GetLeaveHolidayListQuery : PagedRequest, IRequest<PagedResult<List<LeaveHolidayLookupModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public class Handler : IRequestHandler<GetLeaveHolidayListQuery, PagedResult<List<LeaveHolidayLookupModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetLeaveHolidayListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<LeaveHolidayLookupModel>>> Handle(GetLeaveHolidayListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.LeaveHolidays.ToListAsync();

                    if (request.IsDropDown)
                    {
                        var leave = new List<LeaveHolidayLookupModel>();
                        foreach (var item in query)
                        {
                            leave.Add(new LeaveHolidayLookupModel
                            {
                                Id = item.Id,
                                Name = item.Name,
                                Status = item.Status,
                                Country = item.Country,
                                Date = item.Date
                            });
                        }

                        return new PagedResult<List<LeaveHolidayLookupModel>>
                        {
                            Results = leave
                        };
                    }

                    if (!string.IsNullOrEmpty(request.SearchTerm))
                    {
                        var searchTerm = request.SearchTerm;
                        query = query.Where(x => x.Name.Contains(searchTerm)).ToList();
                    }

                    var count = query.Count();
                    query = query.Skip(((request.PageNumber) - 1) * request.PageSize).Take(request.PageSize).ToList();

                    var list = new List<LeaveHolidayLookupModel>();
                    foreach (var item in query)
                    {
                        list.Add(new LeaveHolidayLookupModel
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Status = item.Status,
                            Country = item.Country,
                            Date = item.Date
                        });
                    }

                    return new PagedResult<List<LeaveHolidayLookupModel>>
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
