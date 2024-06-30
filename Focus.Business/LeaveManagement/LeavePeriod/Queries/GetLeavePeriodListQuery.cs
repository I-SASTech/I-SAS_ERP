using Focus.Business.Common;
using Focus.Business.Interface;
using Focus.Business.LeaveManagement.LeavePeriod.Model;
using Focus.Business.LeaveManagement.LeaveType.Model;
using Focus.Business.LeaveManagement.LeaveType.Quries;
using Focus.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Focus.Business.LeaveManagement.LeavePeriod.Queries
{
    public class GetLeavePeriodListQuery : PagedRequest, IRequest<PagedResult<List<LeavePeriodLookupModel>>>
    {
        public string SearchTerm { get; set; }
        public bool IsDropDown { get; set; }
        public class Handler : IRequestHandler<GetLeavePeriodListQuery, PagedResult<List<LeavePeriodLookupModel>>>
        {
            private readonly IApplicationDbContext _context;
            private readonly ILogger _logger;

            public Handler(IApplicationDbContext context, ILogger<GetLeaveTypeListQuery> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<PagedResult<List<LeavePeriodLookupModel>>> Handle(GetLeavePeriodListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var query = await _context.LeavePeriods.ToListAsync();

                    if (request.IsDropDown)
                    {
                        var leave = new List<LeavePeriodLookupModel>();
                        foreach (var item in query)
                        {
                            leave.Add(new LeavePeriodLookupModel
                            {
                                Id = item.Id,
                                Name = item.Name,
                                PeriodEnd = item.PeriodEnd,
                                PeriodStart = item.PeriodStart,
                            });
                        }

                        return new PagedResult<List<LeavePeriodLookupModel>>
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

                    var list = new List<LeavePeriodLookupModel>();
                    foreach (var item in query)
                    {
                        list.Add(new LeavePeriodLookupModel
                        {
                            Id = item.Id,
                            Name = item.Name,
                            PeriodEnd = item.PeriodEnd,
                            PeriodStart = item.PeriodStart,
                        });
                    }

                    return new PagedResult<List<LeavePeriodLookupModel>>
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
